using SkladWPF.AdoModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkladWPF.Admin_page
{
    /// <summary>
    /// Логика взаимодействия для SuppliersPage.xaml
    /// </summary>
    public partial class SuppliersPage : Page
    {
        //string connectionString = "Data Source=DESKTOP-H7G4A3S\\SQLEXPRESS;Initial Catalog=SkladDbActual; Integrated Security=True"; // подключение к бд
        string connectionString = AppDataConnect.Var1;
        int selectedID;
        public SuppliersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SuppliersGrid.ItemsSource = AppDataConnect.bd.Suppliers.ToList();
        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            Suppliers suppliers = new Suppliers();
            if (String.IsNullOrEmpty(NameTxb.Text))
            {
                MessageBox.Show("Имя не заполнено");
            }

            else
            {
                if (String.IsNullOrEmpty(SurnameTxb.Text))
                {
                    MessageBox.Show("Фамилия не заполнена");
                }
                else
                {
                    if (String.IsNullOrEmpty(PhoneTxb.Text))
                    {
                        MessageBox.Show("Телефон не заполнен");
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(MailTxb.Text))
                        {
                            MessageBox.Show("Почта не заполнена");
                        }
                        else
                        {

                            string Name = NameTxb.Text;
                            string Surname = SurnameTxb.Text;
                            string Phone = PhoneTxb.Text;
                            string Mail = MailTxb.Text;


                            SqlConnection connection = new SqlConnection(connectionString);
                            connection.Open();
                            string query1 = $"INSERT INTO Suppliers Values ('{Name}', '{Surname}', '{Mail}', '{Phone}')";
                            SqlCommand command1 = new SqlCommand(query1, connection);
                            //AppDataConnect.bd.Storage.Add(storage);
                            //AppDataConnect.bd.SaveChanges();
                            MessageBox.Show("Поставщик добавлен");

                            command1.ExecuteNonQuery();
                            connection.Close();
                            SuppliersGrid.ItemsSource = AppDataConnect.bd.Suppliers.ToList();

                        }
                    }
                }

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить поставщика? Все связанные с ним данные будут удалены", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (SuppliersGrid.SelectedItem as Suppliers == null)
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                    var CurrentUser = SuppliersGrid.SelectedItem as Suppliers;
                    //int IDSklad = AppDataConnect.bd.Storage.;

                    Suppliers selectedPerson = SuppliersGrid.SelectedItem as Suppliers;

                    if (selectedPerson != null)
                    {
                        selectedID = selectedPerson.ID;
                        // использовать выбранный ID здесь
                    }
                    

                        string temp = "";
                        temp += Convert.ToString(selectedID);

                        SqlConnection connection = new SqlConnection(connectionString);
                        connection.Open();

                        string query1 = $"Delete FROM Applications Where IDSupplier = {temp}";
                        SqlCommand command1 = new SqlCommand(query1, connection);

                        string query2 = $"Delete FROM Suppliers Where ID = {temp}";
                        SqlCommand command2 = new SqlCommand(query2, connection);

                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        connection.Close();

                    SuppliersGrid.ItemsSource = AppDataConnect.bd.Suppliers.ToList();
                    MessageBox.Show("данные удалены");
                    }



                    //    var CurrentUser = SuppliersGrid.SelectedItem as Suppliers;
                    //    AppDataConnect.bd.Suppliers.Remove(CurrentUser);
                    //    AppDataConnect.bd.SaveChanges();
                    //    SuppliersGrid.ItemsSource = AppDataConnect.bd.Suppliers.ToList();



                    //    MessageBox.Show("данные удалены");
                    //}
                
            }
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Admin_page.UserPage());
        }
    }
}
