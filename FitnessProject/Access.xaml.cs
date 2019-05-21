using FitnessProject.Data;
using MySql.Data.MySqlClient;
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

namespace FitnessProject
{
    public partial class Access : Window
    {
        private MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb");

        public Access()
        {
            InitializeComponent();
            connection.Open();
            MySqlCommand comm = new MySqlCommand("select LastName from users where barcode like " + MainWindow.barcode, connection);
            MySqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            string name = reader["LastName"].ToString(); ;

            nameAccess.Text = nameAccess.Text + name + "!";
            connection.Close();

            connection.Open();
            MySqlCommand update = new MySqlCommand("UPDATE tickets SET nr_of_entries = nr_of_entries - 1 WHERE id = " + TicketData.selectedTicket, connection);
            update.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            MySqlCommand command = new MySqlCommand("select * from tickets where id=" + TicketData.selectedTicket, connection);
            MySqlDataReader read = command.ExecuteReader();
            if (read.HasRows)
            {
                read.Read();
                Ticket ticket = new Ticket();
                ticket.valid_from = read["valid_from"].ToString();
                ticket.valid_until = read["valid_until"].ToString();
                ticketDetailsAccess.Text = ticketDetailsAccess.Text + '\n';
                if(ticket.valid_from != "")
                {
                    ticketDetailsAccess.Text = ticketDetailsAccess.Text + "Valid between: " + ticket.valid_from + " - " + ticket.valid_until + '\n';
                }
                ticket.nr_of_entries = (int)read["nr_of_entries"];
                ticketDetailsAccess.Text = ticketDetailsAccess.Text + "Entry remained: " + ticket.nr_of_entries + '\n';
                ticket.nr_of_entries_day = (int)read["nr_of_entries_day"];
                ticketDetailsAccess.Text = ticketDetailsAccess.Text  + "Entries per day: " + ticket.nr_of_entries_day + '\n';
                ticket.hour_from = (int)read["hour_from"];
                ticket.hour_until = (int)read["hour_until"];
                ticketDetailsAccess.Text = ticketDetailsAccess.Text + "Entry between: " + ticket.hour_from + " - " + ticket.hour_until + '\n';
                ticket.weekend = (bool)read["weekend"];
                if (ticket.weekend)
                {
                    ticketDetailsAccess.Text = ticketDetailsAccess.Text + "Entry onlye in weekend" + '\n';
                }
            }
            connection.Close();
            connection.Open();
            MySqlCommand log = new MySqlCommand("insert into logins(barcode, ticket_id) values('"
                + MainWindow.barcode + "', '"
                + TicketData.selectedTicket + "');", connection);
            log.ExecuteNonQuery();
            connection.Close();
        }
    }
}
