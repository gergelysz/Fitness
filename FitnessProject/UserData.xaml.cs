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

            txtBoxFirstName.Text = MainWindow.currentUser.firstName;
            txtBoxLastName.Text = MainWindow.currentUser.lastName;
            txtBoxEmail.Text = MainWindow.currentUser.email;
            txtBoxPhoneNumber.Text = MainWindow.currentUser.phoneNumber;
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

        public void ExecuteQuery(string query)
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
