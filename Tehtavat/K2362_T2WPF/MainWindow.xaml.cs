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
using System.Configuration;
using System.Data;
using System.Xml;

namespace K2362_T2WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Debug xml
        private string fileUrl = "../../Countries.xml";
        // Real XML
        //private string fileUrl = ConfigurationSettings.AppSettings["MaatTiedosto"] ?? "Not Found";
        // ? XML
        //private string fileUrl = ConfigurationManager.AppSettings["MaatTiedosto"] ?? "Not Found";




        public MainWindow()
        {
            InitializeComponent();

            if (fileUrl.Length < 2)
            {
                MessageBox.Show("URL to file is not valid! \nClosing program.");
                System.Environment.Exit(2);

            }

            InitFields();
        }







        private void InitFields()
        {
            cbFiltter.Items.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load(fileUrl);
            XmlNodeList nodeList = doc.SelectNodes("DATA/ROW");

            cbFiltter.Items.Add("All"); //To list all
            foreach (XmlNode node in nodeList)
                if (!cbFiltter.Items.Contains(node.SelectSingleNode("Region").InnerText))
                {
                    cbFiltter.Items.Add(node.SelectSingleNode("Region").InnerText);
                }

        }

        //Doing basic filttering for other functions
        private DataTable basicFiltterig()
        {
            try
            {
                string filu = @fileUrl;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataView dv = new DataView();
                ds.ReadXml(filu);
                dt = ds.Tables[0];

                dv = dt.DefaultView;
                if (cbFiltter.SelectedIndex != -1 && cbFiltter.SelectedIndex != 0)
                {
                    dt.DefaultView.RowFilter = "Region = '" + cbFiltter.SelectedItem.ToString() + "'";
                }
                dt.DefaultView.Sort = "Name";


                //Only show what is needed
                List<DataColumn> columList = new List<DataColumn>();
                List<string> filterColums = new List<string>();
                filterColums.Add("Name");
                filterColums.Add("Continent");
                filterColums.Add("Population");
                filterColums.Add("LocalName");
                filterColums.Add("HeadOfState");



                foreach (DataColumn column in dt.Columns)
                {
                    //Find only needed colums
                    if ( !filterColums.Contains(column.ToString()) )
                    {
                        columList.Add(column);
                        //dt.Columns.Remove(column);
                    }
                }

                //remove colums
                foreach (DataColumn column in columList)
                {
                    dt.Columns.Remove(column);
                }






                return dt;
             }
            catch (Exception ex)
            {
                MessageBox.Show("There were on error while handling data! Error message:\n" + ex.Message + "\n\nClosing program.");
                System.Environment.Exit(2);
            }
            return new DataTable();
        }
        


        private void UpdateFiltter()
        {
            DataView dv = new DataView();
            DataTable dt = basicFiltterig();
            dv = dt.DefaultView;

            //dv.Sort = "Population";
            dt.DefaultView.Sort = "Name";
            dt.DefaultView.FindRows("Population");
            dgView.ItemsSource = dv;
        }


        


        //Button Get clicked
        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            UpdateFiltter();
        }

        private void btnCount_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            DataTable dt = basicFiltterig();
            dv = dt.DefaultView;

            // start counting 
            lbCount.Content = "Country count: " + dv.Count;
        }

        private void btnCountPolulation_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            DataTable dt = basicFiltterig();
            dv = dt.DefaultView;



            // start counting 
            int rows = dv.Count;
            int totalP = 0;
            for (int j=0; j < rows; j++)
            {
                string pStr = dv.ToTable().Rows[j].Field<string>("Population");
                int p = 0;
                int.TryParse(pStr, out p);
                totalP += p;
            }
            lbPopulation.Content = "Population: " + totalP;


            

            dgView.ItemsSource = dv;

        }



        //Changed filtter
        private void cbFiltter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UpdateFiltter();
        }
    }
}
