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
    /// Interaction logic for ReketWindow.xaml
    /// </summary>
    public partial class ReketWindow : Window
    {
        int idMenjam = -1;

        public static BindingList<Reket> Reketi { get; set; }

        public ReketWindow()
        {
            InitializeComponent();


            //popunjavanje liste reketa iz baze
            Reketi = new BindingList<Reket>();

            using (var db = new TKSistemModelContainer())
            {
                var sviReketi = from r in db.Rekets
                                select r;

                foreach (var rek in sviReketi)
                {
                    Reketi.Add(rek);
                }
            }

            DatGridReketi.ItemsSource = Reketi;

            //popunjavanje combo boxa za dostupne fabrike

            List<string> dostupneFabrike = new List<string>();

            using (var db = new TKSistemModelContainer())
            {
                var sveFabrike = from fr in db.FabrikaReketas
                                      select fr;

                foreach (var fab in sveFabrike)
                {
                    dostupneFabrike.Add(fab.NazFab);
                }

            }

            cmbImeFabrike.ItemsSource = dostupneFabrike;


        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Reket r = new Reket();
            r = DatGridReketi.SelectedItem as Reket;

            using (var db = new TKSistemModelContainer())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Rekets where SifRek=" + r.SifRek);


                //resetujem tabelu
                Reketi.Clear();


                var sviReketi = from re in db.Rekets
                                select re;

                foreach (var rek in sviReketi)
                {
                    Reketi.Add(rek);
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Reket r = new Reket();
            r = DatGridReketi.SelectedItem as Reket;

            tbModRek.Text = r.ModRek;
            tbMarRek.Text = r.MarRek;
            tbBojaRek.Text = r.BojaRek;

            using (var db = new TKSistemModelContainer())
            {
                var imeFabrike = from fr1 in db.FabrikaReketas
                                 where fr1.IdFab == r.FabrikaReketaIdFab
                                 select fr1.NazFab;

                foreach (var fab in imeFabrike)
                {
                    cmbImeFabrike.SelectedItem = fab;
                }
            }

            idMenjam = r.SifRek;
            dodajReket.Content = "Izmeni";

        }

        private void dodajReket_Click(object sender, RoutedEventArgs e)
        {
            if (idMenjam == -1)
            {
                if (ValidateReket())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        Reket r = new Reket();
                        r.MarRek = tbMarRek.Text;
                        r.ModRek = tbModRek.Text;
                        r.BojaRek = tbBojaRek.Text;

                        var trazenaFabrika = from fr1 in db.FabrikaReketas
                                             where fr1.NazFab == cmbImeFabrike.SelectedItem.ToString()
                                             select fr1;

                       

                        foreach (var fab in trazenaFabrika)
                        {
                            
                                r.FabrikaReketa = fab;
                            
                        }


                        db.Rekets.Add(r);
                        db.SaveChanges();



                        //resetovanje tabele
                        Reketi.Clear();

                        var sviReketi = from re in db.Rekets
                                        select re;

                        foreach (var rek in sviReketi)
                        {
                            Reketi.Add(rek);
                        }


                    }
                }
                
            }
            else
            {
                if (ValidateReket())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        var trazeniReket = from rrr in db.Rekets
                                           where rrr.SifRek == idMenjam
                                           select rrr;


                        //nadji id fabrike reketa
                        var trazenaFabrika = from fff in db.FabrikaReketas
                                               where fff.NazFab == cmbImeFabrike.Text
                                               select fff;


                        foreach (Reket rek in trazeniReket)
                        {
                            rek.MarRek = tbMarRek.Text;
                            rek.ModRek = tbModRek.Text;
                            rek.BojaRek = tbBojaRek.Text;

                            foreach (FabrikaReketa f in trazenaFabrika)
                            {
                                rek.FabrikaReketa = f;
                            }

                           
                        }

                        db.SaveChanges();

                        //resetovanje tabele
                        Reketi.Clear();


                        var sviReketi = from re in db.Rekets
                                        select re;

                        foreach (var rek in sviReketi)
                        {
                            Reketi.Add(rek);
                        }

                        

                    }

                    idMenjam = -1;
                    dodajReket.Content = "Dodaj";
                    tbBojaRek.Text = "";
                    tbMarRek.Text = "";
                    tbModRek.Text = "";
                }
                
            }
            
        }


        private bool ValidateReket()
        {
            bool rez = true;

            if (tbMarRek.Text.Trim() == "")
            {
                errMarka.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errMarka.Visibility = Visibility.Hidden;
            }

            if (tbModRek.Text.Trim() == "")
            {
                errModel.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errModel.Visibility = Visibility.Hidden;
            }

            if (tbBojaRek.Text.Trim() == "")
            {
                errBoja.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errBoja.Visibility = Visibility.Hidden;
            }


            if (cmbImeFabrike.Text.Trim() == "")
            {
                errFabrika.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errFabrika.Visibility = Visibility.Hidden;
            }

            return rez;


        }


    }
}
