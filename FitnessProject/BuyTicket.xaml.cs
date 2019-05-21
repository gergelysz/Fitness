using FitnessProject.Data;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Text;
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
    public partial class BuyTicket : Window
    {
        public Ticket ticket;
        public BuyTicket()
        {
            InitializeComponent();           
        }

        private void BackToTicket_Click(object sender, RoutedEventArgs e)
        {
            TicketData ticketData = new TicketData();
            ticketData.Show();
            this.Close();
        }

        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessDb");
            connection.Open();
            Console.WriteLine(validFromBuy.Text + " " + validUntilBuy.Text + " " + nrOfEntriesBuy.Text + " " + nrOfEntriesDayBuy.Text + " " + hourFromBuy.Text + " " + hourUntilBuy.Text + " " + weekendBuy.IsChecked );
            MySqlCommand comm = new MySqlCommand("insert into tickets(valid_from, valid_until, nr_of_entries, nr_of_entries_day, hour_from, hour_until, weekend, barcode) values('" 
                + validFromBuy.Text + "', '" 
                + validUntilBuy.Text + "', '" 
                + nrOfEntriesBuy.Text + "', '" 
                + nrOfEntriesDayBuy.Text + "', '" 
                + hourFromBuy.Text + "', '" 
                + hourUntilBuy.Text + "', '" 
                + weekendBuy.IsChecked + "', '"
                + MainWindow.barcode + "');", connection);
            comm.ExecuteNonQuery();
            connection.Close();
            TicketData ticket = new TicketData();
            ticket.Show();
            this.Close();
        }
    }
}
