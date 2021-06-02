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
    /// Interaction logic for TrenerWindow.xaml
    /// </summary>
    public partial class TrenerWindow : Window
    {

        int idMenjam = -1;

      

       

        public static BindingList<Trener> Treneri { get; set; }

        public TrenerWindow()
        {
            InitializeComponent();

            Treneri = new BindingList<Trener>();

            using (var db = new TKSistemModelContainer())
            {
                var sviTreneri = from t in db.Treners
                                 select t;

                foreach (var y in sviTreneri)
                {
                    Treneri.Add(y);
                }


                //ucitaj dostupne klubove u cmb
                List<string> Klubovi = new List<string>();

                var sviKlubovi = from k in db.Klubovi
                                 select k.NazKl;

                foreach (var k in sviKlubovi)
                {
                    Klubovi.Add(k);
                }

                cmbKlubovi.ItemsSource = Klubovi;

            }

            DataGridTreneri.ItemsSource = Treneri;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Trener deleteTrener = new Trener();
            deleteTrener = DataGridTreneri.SelectedItem as Trener;

            using (var db = new TKSistemModelContainer())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Treners where Idtr=" + deleteTrener.IdTr);

                //reset liste za prikaz
                Treneri.Clear();

                var sviTreneri = from tre in db.Treners
                                 select tre;

                foreach (var y in sviTreneri)
                {
                    Treneri.Add(y);
                }

            }


        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            Trener updateTrener = new Trener();
            updateTrener = DataGridTreneri.SelectedItem as Trener;

            tbImeTrenera.Text = updateTrener.ImeTr;
            tbPrezimeTrenera.Text = updateTrener.PrzTr;
            idMenjam = updateTrener.IdTr;

            if (updateTrener.KlubIdKl != null)
            {
                using (var db = new TKSistemModelContainer())
                {
                    var trazenoImeKluba = from klub in db.Klubovi
                                          where klub.IdKl == updateTrener.KlubIdKl
                                          select klub.NazKl;

                    foreach (var ime in trazenoImeKluba)
                    {
                        cmbKlubovi.SelectedItem = ime;
                    }
                }
            }
            

            btnDodajTrenera.Content = "Izmeni";
            
            
        }

        private void btnDodajTrenera_Click(object sender, RoutedEventArgs e)
        {

            if (idMenjam == -1)
            {
                if (ValidateTrener())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        Trener t = new Trener();
                        t.ImeTr = tbImeTrenera.Text;
                        t.PrzTr = tbPrezimeTrenera.Text;

                        if (cmbKlubovi.Text.Trim() != "")
                        {
                            
                                var trazeniKlub = from k in db.Klubovi
                                                  where k.NazKl == cmbKlubovi.Text
                                                  select k;

                                foreach (var klub in trazeniKlub)
                                {
                                    t.Klub = klub;
                                }

                            
                        }

                        db.Treners.Add(t);
                        db.SaveChanges();


                        //reset liste za prikaz
                        Treneri.Clear();

                        var sviTreneri = from tre in db.Treners
                                         select tre;

                        foreach (var y in sviTreneri)
                        {
                            Treneri.Add(y);
                        }

                       

                    }

                    tbImeTrenera.Text = "";
                    tbPrezimeTrenera.Text = "";
                    btnDodajTrenera.Content = "Dodaj";
                    idMenjam = -1;
                }
                
            }
            else
            {
                if (ValidateTrener())
                {
                    using (var db = new TKSistemModelContainer())
                    {

                        var trazeniTrener = from tr in db.Treners
                                            where tr.IdTr == idMenjam
                                            select tr;

                        foreach (var t in trazeniTrener)
                        {
                            t.ImeTr = tbImeTrenera.Text;
                            t.PrzTr = tbPrezimeTrenera.Text;

                            if (cmbKlubovi.Text.Trim() != "")
                            {
                                
                                    var trazeniKlub = from k in db.Klubovi
                                                      where k.NazKl == cmbKlubovi.Text
                                                      select k;

                                    foreach (var klub in trazeniKlub)
                                    {
                                        t.Klub = klub;
                                    }

                            }
                            else
                            {
                                t.Klub = null;
                            }

                        }

                        db.SaveChanges();

                        //reset liste za prikaz
                        Treneri.Clear();

                        var sviTreneri = from tre in db.Treners
                                         select tre;

                        foreach (var y in sviTreneri)
                        {
                            Treneri.Add(y);
                        }


                    }


                    tbImeTrenera.Text = "";
                    tbPrezimeTrenera.Text = "";
                    btnDodajTrenera.Content = "Dodaj";
                    idMenjam = -1;
                }
                
            }
            

            


        }

        private bool ValidateTrener()
        {

            bool rez = true;

            
                if (tbImeTrenera.Text.Trim() == "")
                {
                    rez = false;
                    errImeTrenera.Visibility = Visibility.Visible;
                }
                else
                {
                    errImeTrenera.Visibility = Visibility.Hidden;
                }
            
            



            if (tbPrezimeTrenera.Text.Trim() == "")
            {
                rez = false;
                errPrezimeTrenera.Visibility = Visibility.Visible;
            }
            else
            {
                errPrezimeTrenera.Visibility = Visibility.Hidden;
            }

            return rez;

        }
    }
}
