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
using System.Windows.Shapes;

namespace TKSistemDB
{
    /// <summary>
    /// Interaction logic for IgracWindow.xaml
    /// </summary>
    public partial class IgracWindow : Window
    {
        public IgracWindow()
        {
            InitializeComponent();
        }

        private void DodajProfesionalca_Click(object sender, RoutedEventArgs e)
        {
            ProfesionalacWindow pw = new ProfesionalacWindow();
            pw.Show();
            this.Close();
        }

        private void DodajRekreativca_Click(object sender, RoutedEventArgs e)
        {
            RekreativacWindow rw = new RekreativacWindow();
            rw.Show();
            this.Close();
        }
    }
}
