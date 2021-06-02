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
    /// Interaction logic for KlubWindow.xaml
    /// </summary>
    public partial class KlubWindow : Window
    {

        int idMenjam = -1;

        string imeKojeMenjam = "";

        public static BindingList<Klub> Klubovi { get; set; }


        public KlubWindow()
        {
            InitializeComponent();

            Klubovi = new BindingList<Klub>();

            using (var db = new TKSistemModelContainer())
            {
                var sviKlubovi = from k in db.Klubovi
                                 select k;

                foreach (var kl in sviKlubovi)
                {
                    Klubovi.Add(kl);
                }
            }

            DataGridKlubovi.ItemsSource = Klubovi;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Klub deleteKlub = new Klub();
            deleteKlub = DataGridKlubovi.SelectedItem as Klub;

            using (var db = new TKSistemModelContainer())
            {

                ObrisiProfesionalceKojiSUUTomKlubu(deleteKlub.IdKl);

                ObrisiTrenereKojiRadeUTomKlubu(deleteKlub.IdKl);

                ObrisiUgovoreKojeTajKlubIma(deleteKlub.IdKl);

                db.Database.ExecuteSqlCommand("DELETE FROM Klubovi where IdKl=" + deleteKlub.IdKl);

                //resetuj listu za prikaz
                Klubovi.Clear();

                var sviKlubovi = from klu in db.Klubovi
                                 select klu;

                foreach (var kl in sviKlubovi)
                {
                    Klubovi.Add(kl);
                }

            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Klub updateKlub = new Klub();
            updateKlub = DataGridKlubovi.SelectedItem as Klub;

            tbNazivKluba.Text = updateKlub.NazKl;
            tbAdresaKluba.Text = updateKlub.AdrKl;
            idMenjam = updateKlub.IdKl;

            btnDodaj.Content = "Izmeni";
            imeKojeMenjam = tbNazivKluba.Text;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (idMenjam == -1)
            {
                if (ValidateKlub())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        Klub k = new Klub();

                        k.NazKl = tbNazivKluba.Text;
                        k.AdrKl = tbAdresaKluba.Text;

                        db.Klubovi.Add(k);
                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Klubovi.Clear();

                        var sviKlubovi = from klu in db.Klubovi
                                         select klu;

                        foreach (var kl in sviKlubovi)
                        {
                            Klubovi.Add(kl);
                        }
                    }

                    btnDodaj.Content = "Dodaj";
                    tbAdresaKluba.Text = "";
                    tbNazivKluba.Text = "";
                    idMenjam = -1;
                    imeKojeMenjam = "";
                }
                
            }
            else
            {
                if (ValidateKlub())
                {
                    using (var db = new TKSistemModelContainer())
                    {

                        var trazeniKlub = from k in db.Klubovi
                                          where k.IdKl == idMenjam
                                          select k;

                        foreach (var kl in trazeniKlub)
                        {
                            kl.NazKl = tbNazivKluba.Text;
                            kl.AdrKl = tbAdresaKluba.Text;
                        }

                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Klubovi.Clear();

                        var sviKlubovi = from klu in db.Klubovi
                                         select klu;

                        foreach (var kl in sviKlubovi)
                        {
                            Klubovi.Add(kl);
                        }

                    }

                    btnDodaj.Content = "Dodaj";
                    tbAdresaKluba.Text = "";
                    tbNazivKluba.Text = "";
                    idMenjam = -1;
                    imeKojeMenjam = "";
                }
                
            }
            
        }


        public bool ValidateKlub()
        {
            bool rez = true;

            if (tbNazivKluba.Text.Trim()=="")
            {
                rez = false;
                errNazivKluba.Visibility = Visibility.Visible;
            }
            else
            {
                errNazivKluba.Visibility = Visibility.Hidden;
            }


            if (tbAdresaKluba.Text.Trim() == "")
            {
                rez = false;
                errAdresaKluba.Visibility = Visibility.Visible;
            }
            else
            {
                errAdresaKluba.Visibility = Visibility.Hidden;
            }


            bool nasao = false;

            if (imeKojeMenjam == "")
            {
                foreach (Klub k in Klubovi)
                {
                    if (k.NazKl == tbNazivKluba.Text)
                    {
                        nasao = true;
                    }
                }

                if (nasao)
                {
                    rez = false;
                    errNazivPostoji.Visibility = Visibility.Visible;
                }
                else
                {
                    errNazivPostoji.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                foreach (Klub k in Klubovi)
                {
                    if (k.NazKl == tbNazivKluba.Text && tbNazivKluba.Text!=imeKojeMenjam)
                    {
                        nasao = true;
                    }
                }

                if (nasao)
                {
                    rez = false;
                    errNazivPostoji.Visibility = Visibility.Visible;
                }
                else
                {
                    errNazivPostoji.Visibility = Visibility.Hidden;
                }

                
            }
            

            return rez;
        }


        private void ObrisiProfesionalceKojiSUUTomKlubu(int idKluba)
        {
            using (var db = new TKSistemModelContainer())
            {
                var sviPro = from igr in db.Igraci
                             where igr.TipIgr == "Profesionalac"
                             select igr;

                List<int> idPro = new List<int>();

                foreach (Profesionalac pro in sviPro)
                {
                    if (pro.KlubIdKl == idKluba)
                    {
                        idPro.Add(pro.IdIgr);
                    }
                }

                foreach (int id in idPro)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Igraci where IdIgr=" + id);
                }

            }
        }


        private void ObrisiTrenereKojiRadeUTomKlubu(int idKluba)
        {
            using (var db = new TKSistemModelContainer())
            {
                var svitreneri = from tr in db.Treners
                                 where tr.KlubIdKl == idKluba
                                 select tr;

                List<int> idTren = new List<int>();

                foreach (Trener tr in svitreneri)
                {
                    idTren.Add(tr.IdTr);
                }

                foreach (int id in idTren)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Treners where IdTr=" +id);
                }

            }
        }

        private void ObrisiUgovoreKojeTajKlubIma(int idKluba)
        {
            using (var db = new TKSistemModelContainer())
            {

                var sviUgovori = from ug in db.ima_ugovors
                                 where ug.KlubIdKl == idKluba
                                 select ug;

                List<int> ugovoriZaBrisanje1 = new List<int>();
                List<int> ugovoriZaBrisanje2 = new List<int>();
                foreach (var item in sviUgovori)
                {
                    ugovoriZaBrisanje1.Add(item.FabrikaReketaIdFab);
                    ugovoriZaBrisanje2.Add(item.KlubIdKl);
                }


                for (int i = 0; i < ugovoriZaBrisanje1.Count; i++)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM ima_ugovors where FabrikaReketaIdFab=" + ugovoriZaBrisanje1[i] + " and KlubIdKl=" + ugovoriZaBrisanje2[i]);
                }

            }
        }


    }
}
