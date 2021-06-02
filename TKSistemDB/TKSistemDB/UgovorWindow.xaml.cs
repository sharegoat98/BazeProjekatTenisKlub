using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TKSistemDB
{
    /// <summary>
    /// Interaction logic for UgovorWindow.xaml
    /// </summary>
    public partial class UgovorWindow : Window
    {
        int idKlubaMenjam = -1;
        int idFabrikeMenjam = -1;
        

        public static BindingList<ima_ugovor> Ugovori { get; set; }

        public UgovorWindow()
        {
            InitializeComponent();

            Ugovori = new BindingList<ima_ugovor>();

            using (var db = new TKSistemModelContainer())
            {

                var sviUgovori = from u in db.ima_ugovors
                                 select u;

                foreach (var ug in sviUgovori)
                {
                    Ugovori.Add(ug);
                }

                DataGridUgovori.ItemsSource = Ugovori;


                //dodaj u cmb klubove u fabrike
                List<string> Klubovi = new List<string>();
                List<string> Fabrike = new List<string>();


                var sviklubovi = from k in db.Klubovi
                                 select k.NazKl;

                var sveFabrike = from f in db.FabrikaReketas
                                 select f.NazFab;

                foreach (var kl in sviklubovi)
                {
                    Klubovi.Add(kl);
                }

                foreach (var fab in sveFabrike)
                {
                    Fabrike.Add(fab);
                }


                cmbKlubovi.ItemsSource = Klubovi;
                cmbFabrike.ItemsSource = Fabrike;

            }



        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ima_ugovor deleteUgovor = new ima_ugovor();
            deleteUgovor = DataGridUgovori.SelectedItem as ima_ugovor;

            using (var db = new TKSistemModelContainer())
            {
                //nadji da obrises i turnir sa ovim ugovorom

                var turnir = from tr in db.Turnirs
                             where tr.ima_ugovorFabrikaReketaIdFab == deleteUgovor.FabrikaReketaIdFab && tr.ima_ugovorKlubIdKl == deleteUgovor.KlubIdKl
                             select tr;

                int idTur = -1;
                foreach (var tur in turnir)
                {
                    idTur = tur.IdTur;
                }

                db.Database.ExecuteSqlCommand("DELETE FROM Turnirs where IdTur=" + idTur);



                db.Database.ExecuteSqlCommand("DELETE FROM ima_ugovors where FabrikaReketaIdFab=" + deleteUgovor.FabrikaReketaIdFab + " and KlubIdKl=" + deleteUgovor.KlubIdKl);

                //resetuj listu za prikaz
                Ugovori.Clear();

                var sviUgovori = from ugo in db.ima_ugovors
                                 select ugo;

                foreach (var ug in sviUgovori)
                {
                    Ugovori.Add(ug);
                }
            }




        }

        

        private void btnDodajUgovor_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new TKSistemModelContainer())
            {
                if(idKlubaMenjam  == -1 && idFabrikeMenjam==-1)
                {
                    if (ValidateUgovor())
                    {
                        ima_ugovor u = new ima_ugovor();

                        //nadji izabrani klub
                        var trazeniKlub = from k in db.Klubovi
                                          where k.NazKl == cmbKlubovi.Text
                                          select k;

                        foreach (var kl in trazeniKlub)
                        {
                            u.Klub = kl;
                        }


                        //nadji izabranu fabriku
                        var trazenaFabrika = from fab in db.FabrikaReketas
                                             where fab.NazFab == cmbFabrike.Text
                                             select fab;

                        foreach (var fa in trazenaFabrika)
                        {
                            u.FabrikaReketa = fa;
                        }

                        db.ima_ugovors.Add(u);
                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Ugovori.Clear();

                        var sviUgovori = from ugo in db.ima_ugovors
                                         select ugo;

                        foreach (var ug in sviUgovori)
                        {
                            Ugovori.Add(ug);
                        }

                        idKlubaMenjam = -1;
                        idFabrikeMenjam = -1;
                        btnDodajUgovor.Content = "Dodaj";
                        cmbFabrike.Text = "";
                        cmbKlubovi.Text = "";


                    }

                    

                }
                else
                {
                    if (ValidateUgovor())
                    {

                        ima_ugovor ugg = new ima_ugovor();

                        

                        //pronadji klub
                        var trazeniKlub = from kl in db.Klubovi
                                          where kl.NazKl == cmbKlubovi.Text
                                          select kl;

                        foreach (var klub in trazeniKlub)
                        {
                            
                                ugg.Klub=klub;
                            
                            
                        }


                        //pronadji fabriku
                        var trazenaFabrika = from fab in db.FabrikaReketas
                                             where fab.NazFab == cmbFabrike.Text
                                             select fab;


                        foreach (var fabrika in trazenaFabrika)
                        {
                            
                                ugg.FabrikaReketa = fabrika;
                            
                            
                        }


                        //updateuj turnir sa ovim ugovorom
                        var turnir = from tr in db.Turnirs
                                     where tr.ima_ugovorFabrikaReketaIdFab == idFabrikeMenjam && tr.ima_ugovorKlubIdKl == idKlubaMenjam
                                     select tr;

                        //int idKl = -1;

                        foreach (var tur in turnir)
                        {
                            tur.ima_ugovor = ugg;
                        }

                        db.SaveChanges();


                        var istiUgovor = from ug in db.ima_ugovors
                                         where ug.FabrikaReketaIdFab == ugg.FabrikaReketaIdFab && ug.KlubIdKl == ugg.KlubIdKl
                                         select ug;



                        db.Database.ExecuteSqlCommand("DELETE FROM ima_ugovors where FabrikaReketaIdFab=" + idFabrikeMenjam + " and KlubIdKl=" + idKlubaMenjam);



                        List<ima_ugovor> Ugovori = new List<ima_ugovor>();
                        foreach (var u in istiUgovor)
                        {
                            Ugovori.Add(u);
                        }
                        

                        if (Ugovori.Count == 0)
                        {
                            db.ima_ugovors.Add(ugg);
                            db.SaveChanges();
                        }
                        
                        

                        //resetuj listu za prikaz
                        Ugovori.Clear();

                        var sviUgovori = from ugo in db.ima_ugovors
                                         select ugo;

                        foreach (var ug in sviUgovori)
                        {
                            Ugovori.Add(ug);
                        }

                        idKlubaMenjam = -1;
                        idFabrikeMenjam = -1;
                        btnDodajUgovor.Content = "Dodaj";
                        cmbFabrike.Text = "";
                        cmbKlubovi.Text = "";


                    }
                    



                }


            }


        }

        private bool ValidateUgovor()
        {
            bool rez = true;

            if (cmbKlubovi.Text.Trim() == "")
            {
                rez = false;
                errKlub.Visibility = Visibility.Visible;
            }
            else
            {
                errKlub.Visibility = Visibility.Hidden;
            }


            if (cmbFabrike.Text.Trim() == "")
            {
                rez = false;
                errFabrika.Visibility = Visibility.Visible;
            }
            else
            {
                errFabrika.Visibility = Visibility.Hidden;
            }

            if (idKlubaMenjam!=-1 && idFabrikeMenjam!=-1)
            {
                //specificna provera da li postoji
               
                bool nasaoUgovor = false;

                int idKluba=-1;
                int idFabrike = -1;

                using (var db = new TKSistemModelContainer())
                {

                    var trazeniKlub = from kl in db.Klubovi
                                      where kl.NazKl == cmbKlubovi.Text
                                      select kl.IdKl;

                    var trazenaFabrika = from fab in db.FabrikaReketas
                                          where fab.NazFab == cmbFabrike.Text
                                          select fab.IdFab;


                    foreach (var k in trazeniKlub)
                    {
                        idKluba = k;
                    }

                    foreach (var f in trazenaFabrika)
                    {
                        idFabrike = f;
                    }

                }

                foreach (ima_ugovor ugovor in Ugovori)
                {
                    if ((ugovor.KlubIdKl == idKluba && idKluba!=idKlubaMenjam) && (ugovor.FabrikaReketaIdFab == idFabrike && idFabrike!=idFabrikeMenjam))
                    {
                        nasaoUgovor = true;   
                    }
                }

                if (nasaoUgovor)
                {
                    rez = false;
                    errPostojiUgovor.Visibility = Visibility.Visible;
                }
                else
                {
                    errPostojiUgovor.Visibility = Visibility.Hidden;
                }



            }
            else
            {
                //klasicna provera da li postoji

                bool nasaoUgovor = false;

                int idKluba = -1;
                int idFabrike = -1;

                using (var db = new TKSistemModelContainer())
                {

                    var trazeniKlub = from kl in db.Klubovi
                                      where kl.NazKl == cmbKlubovi.Text
                                      select kl.IdKl;

                    var trazenaFabrika = from fab in db.FabrikaReketas
                                         where fab.NazFab == cmbFabrike.Text
                                         select fab.IdFab;


                    foreach (var k in trazeniKlub)
                    {
                        idKluba = k;
                    }

                    foreach (var f in trazenaFabrika)
                    {
                        idFabrike = f;
                    }

                }

                foreach (ima_ugovor ugovor in Ugovori)
                {
                    if ((ugovor.KlubIdKl == idKluba ) && (ugovor.FabrikaReketaIdFab == idFabrike ))
                    {
                        nasaoUgovor = true;
                    }
                }

                if (nasaoUgovor)
                {
                    rez = false;
                    errPostojiUgovor.Visibility = Visibility.Visible;
                }
                else
                {
                    errPostojiUgovor.Visibility = Visibility.Hidden;
                }

            }

            return rez;


        }


    }
}
