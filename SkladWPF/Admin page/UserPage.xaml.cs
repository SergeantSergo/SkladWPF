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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        //string connectionString = "Data Source=DESKTOP-H7G4A3S\\SQLEXPRESS;Initial Catalog=SkladDbActual; Integrated Security=True"; // подключение к бд
        string connectionString = AppDataConnect.Var1;
        int selectedID;
        public UserPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserGrid.ItemsSource = AppDataConnect.bd.Users.ToList();
            RoleCombo.ItemsSource = AppDataConnect.bd.Role.ToList();
            //SuppliersGrid.ItemsSource = AppDataConnect.bd.Suppliers.ToList();
        }




        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить пользователя? Все связанные с ним данные будут удалены", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (UserGrid.SelectedItem as Users == null)
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                    
                    //MessageBox.Show("данные удалены");

                    var CurrentUser = UserGrid.SelectedItem as Users;
                    //int IDSklad = AppDataConnect.bd.Storage.;

                    Users selectedPerson = UserGrid.SelectedItem as Users;

                    if (selectedPerson != null)
                    {
                        selectedID = selectedPerson.ID;
                        // использовать выбранный ID здесь
                    }
                    

                        string temp = "";
                        temp += Convert.ToString(selectedID);

                        SqlConnection connection = new SqlConnection(connectionString);
                        connection.Open();

                        string query1 = $"Delete FROM Applications Where IDEmployees = {temp}";
                        SqlCommand command1 = new SqlCommand(query1, connection);

                        string query2 = $"Delete FROM Users Where ID = {temp}";
                        SqlCommand command2 = new SqlCommand(query2, connection);

                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        connection.Close();


                    UserGrid.ItemsSource = AppDataConnect.bd.Users.ToList();
                    MessageBox.Show("данные удалены");
                    }

                
            }
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Admin_page.SuppliersPage());
        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            //User users = new User();
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
                    if (String.IsNullOrEmpty(LoginTxb.Text))
                    {
                        MessageBox.Show("Логин пустой");
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(PassworTxb.Text))
                        {
                            MessageBox.Show("Пароль пуст");
                        }
                        else
                        {
                            if (RoleCombo.SelectedItem as Role == null)
                            {
                                MessageBox.Show("Склад не выбран");
                            }
                            else
                            {
                                var CurrentUser = AppDataConnect.bd.Users.FirstOrDefault(u => u.Login == LoginTxb.Text);

                                if (CurrentUser != null)
                                {
                                    MessageBox.Show("Логин занят");
                                }
                                else
                                {
                                    if (LoginTxb.Text.Length < 5)
                                    {
                                        MessageBox.Show("Слишком короткий логин");
                                    }
                                    else
                                    {
                                        if (PassworTxb.Text.Length < 5)
                                        {
                                            MessageBox.Show("Слишком короткий пароль");
                                        }
                                        else
                                        {

                                            string Name = NameTxb.Text;
                                            string Surname = SurnameTxb.Text;
                                            string Login = LoginTxb.Text;
                                            string Passwor = PassworTxb.Text;
                                            var CurrentRole = RoleCombo.SelectedItem as Role;
                                            int Role = CurrentRole.ID;

                                            SqlConnection connection = new SqlConnection(connectionString);
                                            connection.Open();
                                            string query1 = $"INSERT INTO Users Values ('{Name}', '{Surname}', '{Login}', '{Passwor}', {Role})";
                                            SqlCommand command1 = new SqlCommand(query1, connection);
                                            //AppDataConnect.bd.Storage.Add(storage);
                                            //AppDataConnect.bd.SaveChanges();
                                            MessageBox.Show("Пользователь добавлен");

                                            command1.ExecuteNonQuery();
                                            connection.Close();
                                            UserGrid.ItemsSource = AppDataConnect.bd.Users.ToList();
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
