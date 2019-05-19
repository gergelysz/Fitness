using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace FitnessProject
{
    public partial class Registration
    {
        private MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessDb");
        private MySqlCommand command;

        public Registration()
        {
            InitializeComponent();
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


        private void BtnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnSubmitRegister_Click(object sender, RoutedEventArgs e)
        {
            // Get all barcodes in list to later check if the generated
            // one is in the list or not.
            OpenConnection();
            List<string> registeredBarcodes = new List<String>();
            command = new MySqlCommand("select barcode from users", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                registeredBarcodes.Add(reader.ToString());
            }
            reader.Close();
            CloseConnection();

            Random rand;
            int ccc;

            do
            {
                rand = new Random(100);
                ccc = rand.Next(000000000, 999999999);
            } while (registeredBarcodes.Contains(ccc.ToString()));

            MessageBox.Show("Login barcode created: " + ccc, "Barcode");


            if (!string.IsNullOrWhiteSpace(txtFirstName.Text) &&
            !string.IsNullOrWhiteSpace(txtLastName.Text) &&
            !string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string addUserQuery = "insert into users(FirstName, LastName, Email, PhoneNumber, birthday, admin, barcode) values('" +
                    txtFirstName.Text + "', '" +
                    txtLastName.Text + "', '" +
                    txtEmail.Text + "', '" +
                    txtPhoneNumber.Text + "', '" +
                    datePickerBirthday.Text + "', '" +
                    0 + "', '" +
                    ccc.ToString() +
                    "');";

                ExecuteQuery(addUserQuery);
            }
        }
    }
}