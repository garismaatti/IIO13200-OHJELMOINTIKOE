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

namespace K2362_T1b
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //init class
        private Laskuri lsk = new Laskuri();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Calculate button
        private void button_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = lsk.calculateString(tbText.Text);
            lbResult.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                lbResult.Items.Add(list[i]);
            }

            string sum = "Yhteensä ";
            sum += tbText.Text.Length;
            sum += " ja ";
            sum += list.Count;
            sum += " eri kirjainta.";
            labResul.Content = sum;
        }

        //Clear input
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbText.Clear();
        }

        //Creal all
        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            tbText.Clear();
            lbResult.Items.Clear();
            labResul.Content = "Yhteensä 0 merkkiä ja 0 eri kirjainta.";
        }
    }
}
