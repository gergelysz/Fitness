using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FitnessProject
{
    public partial class AdminData : Window
    {
        public AdminData()
        {
            InitializeComponent();

            RunQuery("select * from users");
        }

        private void RunQuery(string query)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessDb";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(query, mySqlConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "LoadData");
            dataGridInfo.DataContext = dataSet;

            try
            {
                mySqlConnection.Open();
                MySqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                    }
                }
                else
                {
                    MessageBox.Show("query executed");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("query error " + e.Message);
            }
        }

        private void onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(comboBoxSelect.SelectedIndex)
            {
                case 0:
                    RunQuery("select * from users");
                    break;
                case 1:
                    RunQuery("select * from tickets");
                    break;
                case 2:
                    break;
            }
        }

        private void saveDataToFile()
        {
            string file_name;
            StringBuilder stringBuilder = new StringBuilder();

            switch (comboBoxSelect.SelectedIndex)
            {
                case 0:
                    file_name = "FitnessTableDataUsers.txt";
                    stringBuilder.Append("FirstName\t\tLastName\t\tEmail\t\tPhoneNumber\t\tadmin\t\tbarcode\n");
                    foreach (DataRowView rowView in dataGridInfo.ItemsSource)
                    {
                        stringBuilder.Append(rowView[0].ToString() + "\t\t" +
                            rowView[1].ToString() + "\t\t" +
                            rowView[2].ToString() + "\t\t" +
                            rowView[3].ToString() + "\t\t" +
                            rowView[4].ToString() + "\t\t" +
                            rowView[5].ToString() + "\n");
                    }
                    break;
                case 1:
                    file_name = "FitnessTableDataTickets.txt";
                    break;
                case 2:
                    file_name = "FitnessTableDataLogins.txt";
                    break;
                default:
                    return;
            }

            File.WriteAllText(file_name, stringBuilder.ToString());
        }

        private void btnSaveToTxt(object sender, RoutedEventArgs e)
        {
            saveDataToFile();
        }
    }
}
