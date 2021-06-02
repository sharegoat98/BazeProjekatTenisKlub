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
    /// Interaction logic for RekreativacWindow.xaml
    /// </summary>
    public partial class RekreativacWindow : Window
    {
        int idMenjam = -1;

        public static BindingList<Rekreativac> Rekreativci {get; set;}

        public RekreativacWindow()
        {
            InitializeComponent();


            Rekreativci = new BindingList<Rekreativac>();

            using (var db = new TKSistemModelContainer())
            {
                var sviRekreativci = from igr in db.Igraci
                                     where igr.TipIgr == "Rekreativac"
                                     select igr;

                foreach (var r in sviRekreativci)
                {
                    Rekreativci.Add(r as Rekreativac);
                }
            }

            DataGridRekreativci.ItemsSource = Rekreativci;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Rekreativac r = new Rekreativac();
            r = DataGridRekreativci.SelectedItem as Rekreativac;

            tbIme.Text = r.ImeIgr;
            tbPrezime.Text = r.PrzIgr;
            tbBrojSparinga.Text = r.BrojSparingaNedeljno.ToString();

            idMenjam = r.IdIgr;

            btnDodajRekreativca.Content = "Izmeni";

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Rekreativac r = new Rekreativac();
            r = DataGridRekreativci.SelectedItem as Rekreativac;

            using (var db = new TKSistemModelContainer())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Igraci where IdIgr=" + r.IdIgr);


                //resetuj listu za prikaz
                Rekreativci.Clear();

                var sviRekreativci = from igr in db.Igraci
                                     where igr.TipIgr == "Rekreativac"
                                     select igr;

                foreach (var rr in sviRekreativci)
                {
                    Rekreativci.Add(rr as Rekreativac);
                }

            }

        }

        private void btnDodajRekreativca_Click(object sender, RoutedEventArgs e)
        {

            if (idMenjam == -1)
            {
                if (ValidateRekreativac())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        Rekreativac r = new Rekreativac();
                        r.ImeIgr = tbIme.Text;
                        r.PrzIgr = tbPrezime.Text;
                        r.BrojSparingaNedeljno = Int32.Parse(tbBrojSparinga.Text);
                        r.TipIgr = "Rekreativac";

                        db.Igraci.Add(r);
                        db.SaveChanges();


                        //resetuj listu za prikaz
                        Rekreativci.Clear();

                        var sviRekreativci = from rek in db.Igraci
                                             where rek.TipIgr == "Rekreativac"
                                             select rek;

                        foreach (var ri in sviRekreativci)
                        {
                            Rekreativci.Add(ri as Rekreativac);
                        }


                    }

                    idMenjam = -1;
                    btnDodajRekreativca.Content = "Dodaj";
                    tbBrojSparinga.Text = "";
                    tbIme.Text = "";
                    tbPrezime.Text = "";
                }
                

            }
            else
            {
                if (ValidateRekreativac())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        var trazeniIgrac = from igr in db.Igraci
                                           where igr.TipIgr == "Rekreativac" && igr.IdIgr == idMenjam
                                           select igr;

                        foreach (Rekreativac r in trazeniIgrac)
                        {

                            r.ImeIgr = tbIme.Text;
                            r.PrzIgr = tbPrezime.Text;
                            r.BrojSparingaNedeljno = Int32.Parse(tbBrojSparinga.Text);

                        }


                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Rekreativci.Clear();

                        var sviRekreativci = from rek in db.Igraci
                                             where rek.TipIgr == "Rekreativac"
                                             select rek;

                        foreach (var ri in sviRekreativci)
                        {
                            Rekreativci.Add(ri as Rekreativac);
                        }

                    }

                    idMenjam = -1;
                    btnDodajRekreativca.Content = "Dodaj";
                    tbBrojSparinga.Text = "";
                    tbIme.Text = "";
                    tbPrezime.Text = "";
                }
                
            }
            
        }


        private bool ValidateRekreativac()
        {
            bool rez = true;

            if (tbIme.Text.Trim() == "")
            {
                errIme.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errIme.Visibility = Visibility.Hidden;
            }

            if (tbPrezime.Text.Trim() == "")
            {
                errPrezime.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errPrezime.Visibility = Visibility.Hidden;
            }

            if (tbBrojSparinga.Text.Trim() == "")
            {
                errSparing.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errSparing.Visibility = Visibility.Hidden;
            }

            bool imaGreska = false;

            try
            {
                Int32.Parse(tbBrojSparinga.Text);
            }
            catch
            {
                if (tbBrojSparinga.Text.Trim() != "")
                {
                    imaGreska = true;
                }
                
            }

            if (imaGreska)
            {
                errSparingBroj.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errSparingBroj.Visibility = Visibility.Hidden;
            }


            return rez;

        }
    }
}
