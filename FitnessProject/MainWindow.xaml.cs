﻿using FitnessProject.Data;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace FitnessProject
{
    public partial class MainWindow : Window
    {
        public static String barcode;
        public static User currentUser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBarCode.Text))
            {
                currentUser = new User();
                barcode = txtBarCode.Text;
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand("select * from users where barcode like " + barcode, mySqlConnection);

                try
                {
                    mySqlConnection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if(Convert.ToBoolean(reader["active"]))
                        {
                            currentUser.firstName = reader["FirstName"].ToString();
                            currentUser.lastName = reader["LastName"].ToString();
                            currentUser.email = reader["Email"].ToString();
                            currentUser.phoneNumber = reader["PhoneNumber"].ToString();
                            currentUser.birthday = reader["birthday"].ToString();
                            currentUser.admin = (int)reader["admin"];
                            currentUser.barcode = reader["barcode"].ToString();
                            currentUser.active = Convert.ToBoolean(reader["active"]);
                            mySqlConnection.Close();

                            // Check if admin or not
                            if (currentUser.admin == 1)
                            {
                                AdminData adminData = new AdminData();
                                adminData.Show();
                                this.Close();
                            }
                            else
                            {
                                TicketData ticketData = new TicketData();
                                ticketData.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Your account has been deleted, please contact us for more help!", "Account deleted", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("User with your barcode couldn't be found!", "Login failed");
                    }
                }
                catch (Exception i)
                {
                    MessageBox.Show("User with your barcode couldn't be found!" + i.Message, "Login failed");
                }
            }
            else
            {
                MessageBox.Show("Please enter your barcode first!", "Field empty");
            }
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
