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
                db.Database.ExecuteSqlCommand("DELETE FROM ima_ugovors where FabrikaReketaIdFab=" + deleteUgovor.FabrikaReketaIdFab+" and KlubIdKl="+deleteUgovor.KlubIdKl);

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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ima_ugovor editUgovor = new ima_ugovor();
            editUgovor = DataGridUgovori.SelectedItem as ima_ugovor;

            using (var db = new TKSistemModelContainer())
            {
                //pronadji klub
                var trazeniKlub = from kl in db.Klubovi
                                  where kl.IdKl == editUgovor.KlubIdKl
                                  select kl;


                foreach (var klub in trazeniKlub)
                {
                    cmbKlubovi.Text = klub.NazKl;
                    idKlubaMenjam = klub.IdKl;
                    
                }


                //pronadji fabriku
                var trazenaFabrika = from f in db.FabrikaReketas
                                     where f.IdFab == editUgovor.FabrikaReketaIdFab
                                     select f;

                foreach (var fab in trazenaFabrika)
                {
                    cmbFabrike.Text = fab.NazFab;
                    idFabrikeMenjam = fab.IdFab;
                    
                }

            }


            errKlub.Visibility = Visibility.Hidden;
            errFabrika.Visibility = Visibility.Hidden;
            errPostojiUgovor.Visibility = Visibility.Hidden;



            btnDodajUgovor.Content = "Izmeni";


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
                            ugg.KlubIdKl = klub.IdKl;
                            
                        }


                        //pronadji fabriku
                        var trazenaFabrika = from fab in db.FabrikaReketas
                                             where fab.NazFab == cmbFabrike.Text
                                             select fab;


                        foreach (var fabrika in trazenaFabrika)
                        {
                            
                                ugg.FabrikaReketa = fabrika;
                            ugg.FabrikaReketaIdFab = fabrika.IdFab;
                            
                        }

                        db.Database.ExecuteSqlCommand("DELETE FROM ima_ugovors where FabrikaReketaIdFab=" + idFabrikeMenjam + " and KlubIdKl=" + idKlubaMenjam);


                        db.ima_ugovors.Add(ugg);
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
