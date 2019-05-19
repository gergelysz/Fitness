using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace FitnessProject
{
    public partial class UserData : Window
    {
        private MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessDb");
        private MySqlCommand command;

        public UserData()
        {
            InitializeComponent();

            txtBlockAccountInfo.Text = MainWindow.currentUser.firstName + " " +
                MainWindow.currentUser.lastName;

            txtBlockTicketInfo.Text = "Your ticket code: " + MainWindow.currentUser.barcode;
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void ExecuteQuery(String query)
        {
            try
            {
                OpenConnection();
                command = new MySqlCommand(query, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    // Query executed
                }
                else
                {
                    // Query not executed
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
