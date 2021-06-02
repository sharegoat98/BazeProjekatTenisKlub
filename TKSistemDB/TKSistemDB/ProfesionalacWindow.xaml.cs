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
    /// Interaction logic for ProfesionalacWindow.xaml
    /// </summary>
    public partial class ProfesionalacWindow : Window
    {

        int idMenjam = -1;

        public static BindingList<Profesionalac> Profesionalci {get; set;}

        public ProfesionalacWindow()
        {
            InitializeComponent();

            Profesionalci = new BindingList<Profesionalac>();

            using (var db = new TKSistemModelContainer())
            {

                var sviProfesionalci = from pro in db.Igraci
                                       where pro.TipIgr == "Profesionalac"
                                       select pro;

                foreach (var pro in sviProfesionalci)
                {
                    Profesionalci.Add(pro as Profesionalac);
                }


                List<string> Klubovi = new List<string>();

                var sviKlubovi = from kl in db.Klubovi
                                 select kl;

                foreach (var kl in sviKlubovi)
                {
                    Klubovi.Add(kl.NazKl);
                }

                cmbKlubovi.ItemsSource = Klubovi;
            }

            DatGridProfesionalci.ItemsSource = Profesionalci;





        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Profesionalac editPro = new Profesionalac();
            editPro = DatGridProfesionalci.SelectedItem as Profesionalac;

            tbIme.Text = editPro.ImeIgr;
            tbPrezime.Text = editPro.PrzIgr;
            tbRang.Text = editPro.Rang.ToString();

            using (var db = new TKSistemModelContainer())
            {

                var trazeniKlub = from k in db.Klubovi
                                  where k.IdKl == editPro.KlubIdKl
                                  select k.NazKl;

                foreach (var kl in trazeniKlub)
                {
                    cmbKlubovi.Text = kl;
                }

            }

            idMenjam = editPro.IdIgr;
            btnDodajProfesionalca.Content = "Izmeni";

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Profesionalac deletePro = new Profesionalac();
            deletePro = DatGridProfesionalci.SelectedItem as Profesionalac;

            using (var db = new TKSistemModelContainer())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Igraci where IdIgr=" + deletePro.IdIgr);

                //resetuj listu za prikaz
                Profesionalci.Clear();

                var sviProfesionalci = from pro in db.Igraci
                                       where pro.TipIgr == "Profesionalac"
                                       select pro;

                foreach (var pro in sviProfesionalci)
                {
                    Profesionalci.Add(pro as Profesionalac);
                }

            }


        }

        private void btnDodajProfesionalca_Click(object sender, RoutedEventArgs e)
        {

            if (idMenjam == -1)
            {
                if (ValidatePro())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        Profesionalac p = new Profesionalac();

                        p.ImeIgr = tbIme.Text;
                        p.PrzIgr = tbPrezime.Text;
                        p.Rang = Int32.Parse(tbRang.Text);
                        p.TipIgr = "Profesionalac";

                        var trazeniKlub = from kl in db.Klubovi
                                          where kl.NazKl == cmbKlubovi.Text
                                          select kl;

                        foreach (var k in trazeniKlub)
                        {
                            p.Klub = k;
                        }

                        db.Igraci.Add(p);
                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Profesionalci.Clear();

                        var sviProfesionalci = from pro in db.Igraci
                                               where pro.TipIgr == "Profesionalac"
                                               select pro;

                        foreach (var pro in sviProfesionalci)
                        {
                            Profesionalci.Add(pro as Profesionalac);
                        }

                    }

                    btnDodajProfesionalca.Content = "Dodaj";
                    idMenjam = -1;
                    tbIme.Text = "";
                    tbPrezime.Text = "";
                    tbRang.Text = "";
                    cmbKlubovi.Text = "";

                }
                
            }
            else
            {
                if (ValidatePro())
                {
                    using (var db = new TKSistemModelContainer())
                    {

                        var trazeniPro = from igr in db.Igraci
                                         where igr.TipIgr == "Profesionalac" && igr.IdIgr == idMenjam
                                         select igr;

                        foreach (Profesionalac pro in trazeniPro)
                        {
                            pro.ImeIgr = tbIme.Text;
                            pro.PrzIgr = tbPrezime.Text;
                            pro.Rang = Int32.Parse(tbRang.Text);
                        }

                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Profesionalci.Clear();

                        var sviProfesionalci = from pro in db.Igraci
                                               where pro.TipIgr == "Profesionalac"
                                               select pro;

                        foreach (var pro in sviProfesionalci)
                        {
                            Profesionalci.Add(pro as Profesionalac);
                        }



                    }

                    btnDodajProfesionalca.Content = "Dodaj";
                    idMenjam = -1;
                    tbIme.Text = "";
                    tbPrezime.Text = "";
                    tbRang.Text = "";
                    cmbKlubovi.Text = "";
                }
                
            }
            
        }


        private bool ValidatePro()
        {
            bool rez = true;

            if (tbIme.Text.Trim() == "")
            {
                rez = false;
                errIme.Visibility = Visibility.Visible;
            }
            else
            {
                errIme.Visibility = Visibility.Hidden;
            }


            if (tbPrezime.Text.Trim() == "")
            {
                rez = false;
                errPrezime.Visibility = Visibility.Visible;
            }
            else
            {
                errPrezime.Visibility = Visibility.Hidden;
            }

            
            if (tbRang.Text.Trim() == "")
            {
                rez = false;
                errRang.Visibility = Visibility.Visible;
            }
            else
            {
                errRang.Visibility = Visibility.Hidden;
            }

            bool uh = false;

            if (tbRang.Text.Trim() != "")
            {
                try
                {
                    Int32.Parse(tbRang.Text);
                }
                catch
                {
                    uh = true;
                }

                if (uh)
                {
                    errRangBroj.Visibility = Visibility.Visible;
                    rez = false;
                }
                else
                {
                    errRangBroj.Visibility = Visibility.Hidden;
                }
            }
            


            if (cmbKlubovi.Text.Trim() == "")
            {
                rez = false;
                errKlub.Visibility = Visibility.Visible;

            }
            else
            {
                errKlub.Visibility = Visibility.Hidden;
            }

            return rez;

        }


    }
}
