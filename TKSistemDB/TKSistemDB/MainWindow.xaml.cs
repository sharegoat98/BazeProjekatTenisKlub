using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TKSistemDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DodajFabriku_Click(object sender, RoutedEventArgs e)
        {
            FabrikaReketaWindow frw = new FabrikaReketaWindow();
            frw.Show();
        }

        

        private void ReketButton_Click(object sender, RoutedEventArgs e)
        {
            ReketWindow rw = new ReketWindow();
            rw.Show();
        }

        private void ZicaButton_Click(object sender, RoutedEventArgs e)
        {
            ZicaWindow zw = new ZicaWindow();
            zw.Show();
        }

        private void IgracButton_Click(object sender, RoutedEventArgs e)
        {
            IgracWindow iw = new IgracWindow();
            iw.Show();
        }

        private void KlubButton_Click(object sender, RoutedEventArgs e)
        {
            KlubWindow kw = new KlubWindow();
            kw.Show();
        }

        private void TrenerButton_Click(object sender, RoutedEventArgs e)
        {
            TrenerWindow tw = new TrenerWindow();
            tw.Show();
        }

        private void TurnirButton_Click(object sender, RoutedEventArgs e)
        {
            TurnirWindow tuw = new TurnirWindow();
            tuw.Show();
        }

        private void UgovorButton_Click(object sender, RoutedEventArgs e)
        {
            UgovorWindow uw = new UgovorWindow();
            uw.Show();
        }
    }
}
