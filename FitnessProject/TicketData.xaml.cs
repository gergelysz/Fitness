using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace FitnessProject
{
    /// <summary>
    /// Interaction logic for TicketData.xaml
    /// </summary>
    public partial class TicketData : Window
    {
        public TicketData()
        {
            InitializeComponent();
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb";
            MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessDb");

            conn.Open();

            //MySqlCommand comm = new MySqlCommand("select count(*) from tickets where barcode like" + MainWindow.barcode, conn);

            MySqlCommand comm = new MySqlCommand("select count(*) from tickets where barcode like " + MainWindow.barcode, conn);
            int count = int.Parse(comm.ExecuteScalar().ToString());
            //Console.WriteLine(comm.ExecuteScalar());


            if (count > 0)
            {
                txtTicketMessage.Visibility = Visibility.Hidden;
            }
            else
            {
                btnTicketSelect.Visibility = Visibility.Hidden;
            }
        }

        private void BtnTicketSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTicketBuy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
