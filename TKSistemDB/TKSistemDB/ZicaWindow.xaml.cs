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
    /// Interaction logic for ZicaWindow.xaml
    /// </summary>
    public partial class ZicaWindow : Window
    {

        int idMenjam = -1;
        public static BindingList<Zica> Zice { get; set; }

        public static List<string> Reketi { get; set; }

        public ZicaWindow()
        {
            InitializeComponent();

            Zice = new BindingList<Zica>();

            using (var db = new TKSistemModelContainer())
            {

                var sveZice = from z in db.Zice
                              select z;

                foreach (var z in sveZice)
                {
                    Zice.Add(z);
                }

                DataGridZice.ItemsSource = Zice;




                var sviReketi = from r in db.Rekets
                                     select r;

                Reketi = new List<string>();

                foreach (var rek in sviReketi)
                {
                    string rekZaPrikaz = "";
                    rekZaPrikaz= rekZaPrikaz+rek.SifRek+" "+rek.MarRek+" "+rek.ModRek+" "+rek.BojaRek;
                    Reketi.Add(rekZaPrikaz);
                }

                cmbreketi.ItemsSource = Reketi;


            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Zica updateZica = new Zica();
            updateZica = DataGridZice.SelectedItem as Zica;

            tbBojaZice.Text = updateZica.BojaZic;
            tbMaterijalZice.Text = updateZica.MatZic;

            foreach (string rek in Reketi)
            {
                if (updateZica.Reket != null)
                {
                    string a = rek.Split(' ')[0];
                    if (Int32.Parse(a) == updateZica.Reket.SifRek)
                    {
                        cmbreketi.Text = rek;
                    }
                }
                else
                {
                    cmbreketi.Text = "";
                }
                
            }

            idMenjam = updateZica.SifZic;
            btnDodajZicu.Content = "Izmeni";


            errBoja.Visibility = Visibility.Hidden;
            errMat.Visibility = Visibility.Hidden;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Zica deleteZica = new Zica();
            deleteZica = DataGridZice.SelectedItem as Zica;

            using (var db = new TKSistemModelContainer())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Zice where SifZic=" + deleteZica.SifZic);

                //resetuj listu za prikaz
                Zice.Clear();

                var sveZice = from zz in db.Zice
                              select zz;

                foreach (var zic in sveZice)
                {
                    Zice.Add(zic);
                }

            }

        }

        private void btnDodajZicu_Click(object sender, RoutedEventArgs e)
        {
            
            if (idMenjam == -1)
            {
                if (ValidateZica())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        Zica z = new Zica();

                        z.BojaZic = tbBojaZice.Text;
                        z.MatZic = tbMaterijalZice.Text;

                        if (cmbreketi.Text.Trim() != "")
                        {
                            string idKojiTrazimReket = cmbreketi.Text.Split(' ')[0];

                            int idddd = Int32.Parse(idKojiTrazimReket);

                            var trazeniReket = from rek in db.Rekets
                                               where rek.SifRek == idddd
                                               select rek;

                            foreach (var reket in trazeniReket)
                            {
                                z.Reket = reket;
                            }


                        }

                        db.Zice.Add(z);
                        db.SaveChanges();


                        //resetuj listu za prikaz
                        Zice.Clear();

                        var sveZice = from zz in db.Zice
                                      select zz;

                        foreach (var zic in sveZice)
                        {
                            Zice.Add(zic);
                        }

                    }

                    btnDodajZicu.Content = "Dodaj";
                    tbBojaZice.Text = "";
                    tbMaterijalZice.Text = "";
                    cmbreketi.Text = "";
                    idMenjam = -1;
                }
                
            }
            else
            {
                if (ValidateZica())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        var trazenaZica = from zi in db.Zice
                                          where zi.SifZic == idMenjam
                                          select zi;

                        foreach (var zicaa in trazenaZica)
                        {
                            zicaa.BojaZic = tbBojaZice.Text;
                            zicaa.MatZic = tbMaterijalZice.Text;

                            if (cmbreketi.Text.Trim() != "")
                            {
                                string a = cmbreketi.Text.Split(' ')[0];
                                int b = Int32.Parse(a);

                                var trazeniReket = from r in db.Rekets
                                                   where r.SifRek == b
                                                   select r;

                                foreach (var rekkkk in trazeniReket)
                                {
                                    zicaa.Reket = rekkkk;
                                }
                            }
                        }

                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Zice.Clear();

                        var sveZice = from zz in db.Zice
                                      select zz;

                        foreach (var zic in sveZice)
                        {
                            Zice.Add(zic);
                        }


                    }

                    btnDodajZicu.Content = "Dodaj";
                    tbBojaZice.Text = "";
                    tbMaterijalZice.Text = "";
                    cmbreketi.Text = "";
                    idMenjam = -1;
                }
                
            }
            




        }

        private bool ValidateZica()
        {
            bool rez = true;

            if (tbBojaZice.Text.Trim() == "")
            {
                errBoja.Visibility = Visibility.Visible;
                rez = false;

            }
            else
            {
                errBoja.Visibility = Visibility.Hidden;
            }

            if (tbMaterijalZice.Text.Trim()=="")
            {
                errMat.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errMat.Visibility = Visibility.Hidden;
            }

            return rez;

        }
    }
}
