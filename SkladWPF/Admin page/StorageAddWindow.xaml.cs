using SkladWPF.AdoModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace SkladWPF.Admin_page
{
    /// <summary>
    /// Логика взаимодействия для StorageAddWindow.xaml
    /// </summary>
    public partial class StorageAddWindow : Window
    {

        string connectionString = AppDataConnect.Var1;
        int selectedID;
        string selectedName;


        public StorageAddWindow()

        {
            InitializeComponent();
            //MessageBox.Show(connectionString);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StorageGrid.ItemsSource = AppDataConnect.bd.Storage.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Storage storage = new Storage();
            if (String.IsNullOrEmpty(NameTxb.Text))
            {
                MessageBox.Show("Название пустое");
            }
            else
            {

                var CurrentUser = AppDataConnect.bd.Storage.FirstOrDefault(u => u.Name == NameTxb.Text );

                if (CurrentUser != null)
                {
                    MessageBox.Show("Данный склад уже существует");
                }
                else
                {

                    string Name = NameTxb.Text;


                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string query1 = $"INSERT INTO Storage Values ('{Name}')";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    //AppDataConnect.bd.Storage.Add(storage);
                    //AppDataConnect.bd.SaveChanges();
                    MessageBox.Show("Склад добавлен");

                    command1.ExecuteNonQuery();
                    connection.Close();
                    StorageGrid.ItemsSource = AppDataConnect.bd.Storage.ToList();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить Склад? Все связанные с ним данные будут удалены", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (StorageGrid.SelectedItem as Storage == null)
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                   
                    //var CurrentUser = StorageGrid.SelectedItem as Storage;
                    //int IDSklad = AppDataConnect.bd.Storage.;

                    Storage selectedSklad1 = StorageGrid.SelectedItem as Storage;
                    Storage nameSklad = StorageGrid.SelectedItem as Storage;
                    if (selectedSklad1 != null)
                    {
                        selectedID = selectedSklad1.ID;
                        // использовать выбранный ID здесь
                    }
                    if (nameSklad != null)
                    {
                        selectedName = nameSklad.Name;
                        // использовать выбранный ID здесь
                    }


                    string temp = "";
                        temp += Convert.ToString(selectedID);
                        string temp2 = "";
                        temp2 += selectedName;
                        SqlConnection connection = new SqlConnection(connectionString);
                        connection.Open();
                        string query1 = $"Delete FROM Request Where IDStorage = {temp}";
                        SqlCommand command1 = new SqlCommand(query1, connection);
                        string query2 = $"Delete FROM ProductStock Where IDStorage = {temp}";
                        SqlCommand command2 = new SqlCommand(query2, connection);
                        string query3 = $"Delete FROM Applications Where IDStorage = {temp}";
                        SqlCommand command3 = new SqlCommand(query3, connection);

                        string query4 = $"Delete FROM Storage Where ID = {temp}";
                        SqlCommand command4 = new SqlCommand(query4, connection);

                        string query5 = $"Delete FROM RelocationPlan Where Storage1 = '{temp2}' or Storage2 = '{temp2}'";
                        SqlCommand command5 = new SqlCommand(query5, connection);

                    //MessageBox.Show(temp);
                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        command3.ExecuteNonQuery();
                        command5.ExecuteNonQuery();
                        command4.ExecuteNonQuery();
                        connection.Close();

                    StorageGrid.ItemsSource = AppDataConnect.bd.Storage.ToList();
                    MessageBox.Show("данные удалены");

                    }



                }

            }
        }
    }

