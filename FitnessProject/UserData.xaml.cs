using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace FitnessProject
{
    public partial class UserData : Window
    {
        private MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb");
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

        private void deleteAccount(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your account?", "Delete account", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ExecuteQuery("update users set active = 0 where barcode like " + MainWindow.currentUser.barcode);
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            } 
        }
    }
}
