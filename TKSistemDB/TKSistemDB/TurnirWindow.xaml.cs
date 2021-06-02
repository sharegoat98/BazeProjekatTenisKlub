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
    /// Interaction logic for TurnirWindow.xaml
    /// </summary>
    public partial class TurnirWindow : Window
    {
        int idMenjam = -1;

        string nazivMenjam = "";

        public static BindingList<Turnir> Turniri { get; set; }

        public static List<string> Ugovori { get; set; }

        public TurnirWindow()
        {
            InitializeComponent();

            Turniri = new BindingList<Turnir>();

            Ugovori = new List<string>();

            using (var db = new TKSistemModelContainer())
            {
                var sviTurniri = from t in db.Turnirs
                                     select t;

                foreach (var tur in sviTurniri)
                {
                    Turniri.Add(tur);
                }

                DataGridTurniri.ItemsSource = Turniri;


                var sviUgovori = from ug in db.ima_ugovors
                                                  select ug;

                foreach (var uug in sviUgovori)
                {

                    Ugovori.Add("Fabrika: "+uug.FabrikaReketa.NazFab+" Klub: "+uug.Klub.NazKl);
                }

                cmbDomacinTurnira.ItemsSource = Ugovori;

            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Turnir updateTurnir = new Turnir();
            updateTurnir = DataGridTurniri.SelectedItem as Turnir;

            tbImeTurnira.Text = updateTurnir.ImeTur;

            using (var db = new TKSistemModelContainer())
            {
                var imeKlubaPrekoID = from kl in db.Klubovi
                                      where kl.IdKl == updateTurnir.ima_ugovor.KlubIdKl
                                      select kl;

                string imeKluba = "";

                foreach (var item in imeKlubaPrekoID)
                {
                    imeKluba = item.NazKl;
                }


                var imeFabrikePrekoID = from fab in db.FabrikaReketas
                                        where fab.IdFab == updateTurnir.ima_ugovor.FabrikaReketaIdFab
                                        select fab;

                string imeFabrike = "";

                foreach (var item in imeFabrikePrekoID)
                {
                    imeFabrike = item.NazFab;
                }

                string ugovorString = "Fabrika: " + imeFabrike + " Klub: " + imeKluba;

                foreach (var item in Ugovori)
                {
                    if (item == ugovorString)
                    {
                        cmbDomacinTurnira.Text = item;
                    }
                }

                nazivMenjam = updateTurnir.ImeTur;

            }

           

            idMenjam = updateTurnir.IdTur;
            btnDodajTurnir.Content = "Izmeni";
            errImePostoji.Visibility = Visibility.Hidden;
            errIme.Visibility = Visibility.Hidden;
            errDomacin.Visibility = Visibility.Hidden;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Turnir t = new Turnir();
            t = DataGridTurniri.SelectedItem as Turnir;

            using (var db = new TKSistemModelContainer())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Turnirs where IdTur=" + t.IdTur);

                //resetuj listu za prikaz
                Turniri.Clear();

                var sviTurniri = from tt in db.Turnirs
                                 select tt;

                foreach (var tur in sviTurniri)
                {
                    Turniri.Add(tur);
                }
            }
        }


        private void btnDodajTurnir_Click(object sender, RoutedEventArgs e)
        {

            if (idMenjam == -1)
            {
                if (ValidateTurnir())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        Turnir t = new Turnir();

                        t.ImeTur = tbImeTurnira.Text;

                        string nazivFabrikeUUgovoru = cmbDomacinTurnira.Text.Split(':')[1].Split(' ')[1];
                        string nazivKlubaUUgovoru = cmbDomacinTurnira.Text.Split(':')[2].Split(' ')[1];


                        var trazeniUgovor = from ug in db.ima_ugovors
                                            where ug.Klub.NazKl == nazivKlubaUUgovoru && ug.FabrikaReketa.NazFab == nazivFabrikeUUgovoru
                                            select ug;

                        foreach (var item in trazeniUgovor)
                        {
                            t.ima_ugovor = item;
                        }

                        db.Turnirs.Add(t);
                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Turniri.Clear();

                        var sviTurniri = from tt in db.Turnirs
                                         select tt;

                        foreach (var tur in sviTurniri)
                        {
                            Turniri.Add(tur);
                        }


                        tbImeTurnira.Text = "";
                        cmbDomacinTurnira.Text = "";
                        idMenjam = -1;
                        btnDodajTurnir.Content = "Dodaj";
                        nazivMenjam = "";
                    }
                }
                
            }
            else
            {
                if (ValidateTurnir())
                {
                    using (var db = new TKSistemModelContainer())
                    {


                        var turnirKojiMitreba = from tur in db.Turnirs
                                                where tur.IdTur == idMenjam
                                                select tur;

                        string nazivFabrikeUUgovoru = cmbDomacinTurnira.Text.Split(':')[1].Split(' ')[1];
                        string nazivKlubaUUgovoru = cmbDomacinTurnira.Text.Split(':')[2].Split(' ')[1];


                        var trazeniUgovor = from ug in db.ima_ugovors
                                            where ug.Klub.NazKl == nazivKlubaUUgovoru && ug.FabrikaReketa.NazFab == nazivFabrikeUUgovoru
                                            select ug;


                        foreach (var tur in turnirKojiMitreba)
                        {
                            tur.ImeTur = tbImeTurnira.Text;

                            foreach (var ugo in trazeniUgovor)
                            {
                                tur.ima_ugovor = ugo;
                            }

                        }

                        db.SaveChanges();

                        //resetuj listu za prikaz
                        Turniri.Clear();

                        var sviTurniri = from tt in db.Turnirs
                                         select tt;

                        foreach (var tur in sviTurniri)
                        {
                            Turniri.Add(tur);
                        }

                        


                        tbImeTurnira.Text = "";
                        cmbDomacinTurnira.Text = "";
                        idMenjam = -1;
                        btnDodajTurnir.Content = "Dodaj";
                        nazivMenjam = "";


                    }
                }
                
            }
            
        }


        private bool ValidateTurnir()
        {
            bool rez = true;

            if (tbImeTurnira.Text.Trim() == "")
            {
                errIme.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errIme.Visibility = Visibility.Hidden;
            }

            if (cmbDomacinTurnira.Text.Trim() == "")
            {
                errDomacin.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errDomacin.Visibility = Visibility.Hidden;
            }

            if (nazivMenjam != "")
            {
                bool nasao = false;

                using (var db = new TKSistemModelContainer())
                {
                    var svaImenaTurnira = from tur in db.Turnirs
                                          select tur.ImeTur;


                    foreach (var item in svaImenaTurnira)
                    {
                        if (item==tbImeTurnira.Text && tbImeTurnira.Text != nazivMenjam)
                        {
                            nasao = true;
                        }
                    }
                }

                if (nasao)
                {
                    errImePostoji.Visibility = Visibility.Visible;
                    rez = false;
                }
                else
                {
                    errImePostoji.Visibility = Visibility.Hidden;
                }

            }
            else
            {
                bool nasao = false;

                using (var db = new TKSistemModelContainer())
                {
                    var svaImenaTurnira = from tur in db.Turnirs
                                          select tur.ImeTur;


                    foreach (var item in svaImenaTurnira)
                    {
                        if (item == tbImeTurnira.Text)
                        {
                            nasao = true;
                        }
                    }
                }

                if (nasao)
                {
                    errImePostoji.Visibility = Visibility.Visible;
                    rez = false;
                }
                else
                {
                    errImePostoji.Visibility = Visibility.Hidden;
                }
            }


            return rez;


        }
    }
}
