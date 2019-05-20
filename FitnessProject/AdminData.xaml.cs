using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb";
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
            }
            catch (Exception e)
            {
                MessageBox.Show("query error " + e.Message);
            }
        }

        private void onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBoxSelect.SelectedIndex)
            {
                case 0:
                    RunQuery("select * from users");
                    break;
                case 1:
                    RunQuery("select * from tickets");
                    break;
                case 2:
                    RunQuery("select * from logins");
                    break;
            }
        }

        private void ExportToExcel()
        {
            dataGridInfo.SelectAllCells();
            dataGridInfo.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataGridInfo);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            dataGridInfo.UnselectAllCells();
            System.IO.StreamWriter file = null;

            switch (comboBoxSelect.SelectedIndex)
            {
                case 0:
                    file = new System.IO.StreamWriter("TableData_Users.xls");
                    break;
                case 1:
                    file = new System.IO.StreamWriter("TableData_Tickets.xls");
                    break;
                case 2:
                    file = new System.IO.StreamWriter("TableData_Logins.xls");
                    break;
            }

            if (file != null)
            {
                file.WriteLine(result.Replace(',', ' '));
                file.Close();
            }
        }

        private void btnSaveToTxt(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void btnSearchUser(object sender, RoutedEventArgs e)
        {
            RunQuery("select * from users where concat(FirstName, LastName, Email, PhoneNumber, birthday, admin, barcode)" +
                " like '%" + txtBoxSearchUser.Text + "%'");
        }
    }
}
