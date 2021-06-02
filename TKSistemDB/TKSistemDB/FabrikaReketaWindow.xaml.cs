using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
    /// Interaction logic for FabrikaReketaWindow.xaml
    /// </summary>
    public partial class FabrikaReketaWindow : Window
    {

        int idMenjam = -1;

        string imeKojeMenjam = "";

        public static BindingList<FabrikaReketa> FabrikeReketa { get; set; }


        public FabrikaReketaWindow()
        {
            InitializeComponent();

             FabrikeReketa = new BindingList<FabrikaReketa>();

            using (var db = new TKSistemModelContainer())
            {
                var sveFabrike = from fr in db.FabrikaReketas
                                 select fr;


                foreach (var fabRek in sveFabrike)
                {
                    FabrikeReketa.Add(fabRek);
                }

            }

            DataGridFabrikeReketa.ItemsSource = FabrikeReketa;


        }

      
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            FabrikaReketa frUpdate = new FabrikaReketa();
            frUpdate = DataGridFabrikeReketa.SelectedItem as FabrikaReketa;
            dodajFabrikuReketaButton.Content = "Izmeni";
            adrFabTB.Text = frUpdate.AdrFab;
            nazFabTB.Text = frUpdate.NazFab;
            idMenjam = frUpdate.IdFab;
            imeKojeMenjam = nazFabTB.Text;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            FabrikaReketa frDelete = new FabrikaReketa();
            frDelete = DataGridFabrikeReketa.SelectedItem as FabrikaReketa;

            using (var db = new TKSistemModelContainer())
            {

                List<int> IdeviTurniraKojiSeBrisu = new List<int>();
                List<int> IdeviUgvoraKojiSeBrisu = new List<int>();


                var reketiZaBrisanje = from rek in db.Rekets
                                       where rek.FabrikaReketa.IdFab == frDelete.IdFab
                                       select rek.SifRek;

                List<int> Ids = new List<int>();

                foreach (var item in reketiZaBrisanje)
                {
                    Ids.Add(item);
                    
                }

                List<Zica> zicee = new List<Zica>();

                foreach (int item in Ids)
                {
                    var sveZiceSaReketom = from zic in db.Zice
                                           where zic.ReketSifRek == item
                                           select zic;

                    foreach (var z in sveZiceSaReketom)
                    {
                        zicee.Add(z);
                    }
                }



                foreach (Zica zzz in zicee)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Zice where SifZic=" + zzz.SifZic);
                }


                foreach (var ind in Ids)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Rekets where SifRek=" + ind);
                }


                ObrisiTurnireSaOvoFabrikom(frDelete.IdFab);

                ObrisiUgovoreSaOvomFabrikom(frDelete.IdFab);

                db.Database.ExecuteSqlCommand("DELETE FROM FabrikaReketas where IdFab=" + frDelete.IdFab);




                FabrikeReketa.Clear();

                var sveFabrike = from fr1 in db.FabrikaReketas
                                 select fr1;


                foreach (var fabRek in sveFabrike)
                {
                    FabrikeReketa.Add(fabRek);
                }

            }
        }

        private void dodajFabrikuReketaButton_Click(object sender, RoutedEventArgs e)
        {
            if (idMenjam == -1)
            {
                if (ValidateFabrika())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        FabrikaReketa fr = new FabrikaReketa();
                        fr.AdrFab = adrFabTB.Text;
                        fr.NazFab = nazFabTB.Text;


                        db.FabrikaReketas.Add(fr);
                        db.SaveChanges();


                        FabrikeReketa.Clear();

                        var sveFabrike = from fr1 in db.FabrikaReketas
                                         select fr1;


                        foreach (var fabRek in sveFabrike)
                        {
                            FabrikeReketa.Add(fabRek);
                        }
                    }

                    idMenjam = -1;
                    dodajFabrikuReketaButton.Content = "Dodaj";
                    adrFabTB.Text = "";
                    nazFabTB.Text = "";
                    imeKojeMenjam = "";
                }
                
            }
            else
            {
                if (ValidateFabrika())
                {
                    using (var db = new TKSistemModelContainer())
                    {
                        var trazenaFabrika = from fr1 in db.FabrikaReketas
                                             where fr1.IdFab == idMenjam
                                             select fr1;

                        foreach (FabrikaReketa f in trazenaFabrika)
                        {
                            f.AdrFab = adrFabTB.Text;
                            f.NazFab = nazFabTB.Text;
                        }

                        db.SaveChanges();



                        FabrikeReketa.Clear();

                        var sveFabrike = from fr1 in db.FabrikaReketas
                                         select fr1;


                        foreach (var fabRek in sveFabrike)
                        {
                            FabrikeReketa.Add(fabRek);
                        }

                    }

                    idMenjam = -1;
                    dodajFabrikuReketaButton.Content = "Dodaj";
                    adrFabTB.Text = "";
                    nazFabTB.Text = "";
                    imeKojeMenjam = "";
                }
                


            }
            
        }

        private bool ValidateFabrika()
        {
            bool rez = true;

            if (nazFabTB.Text.Trim() == "")
            {
                errNaziv.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errNaziv.Visibility = Visibility.Hidden;
            }

            if (adrFabTB.Text.Trim() == "")
            {
                errAdresa.Visibility = Visibility.Visible;
                rez = false;
            }
            else
            {
                errAdresa.Visibility = Visibility.Hidden;
            }

            bool nasao=false;

            


            if (imeKojeMenjam == "")
            {
                foreach (FabrikaReketa fr in FabrikeReketa)
                {
                    if (fr.NazFab == nazFabTB.Text)
                    {
                        nasao = true;
                    }
                }


                if (nasao)
                {
                    errNazivPostoji.Visibility = Visibility.Visible;
                    rez = false;
                }
                else
                {
                    errNazivPostoji.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                foreach (FabrikaReketa fr in FabrikeReketa)
                {
                    if (fr.NazFab == nazFabTB.Text && nazFabTB.Text != imeKojeMenjam)
                    {
                        nasao = true;
                    }
                }


                if (nasao)
                {
                    errNazivPostoji.Visibility = Visibility.Visible;
                    rez = false;
                }
                else
                {
                    errNazivPostoji.Visibility = Visibility.Hidden;
                }
            }

            

            return rez;

        }

        private void ObrisiTurnireSaOvoFabrikom(int idFabrike)
        {
            using (var db = new TKSistemModelContainer())
            {
                var sviTurniri = from t in db.Turnirs
                                 where t.ima_ugovorFabrikaReketaIdFab == idFabrike
                                 select t.IdTur;

                List<int> turniriZaBrisanje = new List<int>();
                foreach (var item in sviTurniri)
                {
                    turniriZaBrisanje.Add(item);
                }

                foreach (int id in turniriZaBrisanje)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Turnirs where IdTur=" + id);
                }


            }


            

               
        }

        private void ObrisiUgovoreSaOvomFabrikom(int idFabrike)
        {
            using (var db = new TKSistemModelContainer())
            {

                var sviUgovori = from ug in db.ima_ugovors
                                 where ug.FabrikaReketaIdFab == idFabrike
                                 select ug;

                List<int> ugovoriZaBrisanje1 = new List<int>();
                List<int> ugovoriZaBrisanje2 = new List<int>();
                foreach(var item in sviUgovori)
                {
                    ugovoriZaBrisanje1.Add(item.FabrikaReketaIdFab);
                    ugovoriZaBrisanje2.Add(item.KlubIdKl);
                }


                for (int i =0; i < ugovoriZaBrisanje1.Count; i++)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM ima_ugovors where FabrikaReketaIdFab=" +ugovoriZaBrisanje1[i]+" and KlubIdKl="+ugovoriZaBrisanje2[i]);
                }

            }
        }


    }
}
