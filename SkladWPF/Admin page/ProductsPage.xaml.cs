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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        string connectionString = AppDataConnect.Var1; // ссылка на бд
        int selectedID;
        public ProductsPage()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить продукт? Все связанные с ним данные будут удалены", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (ProductGrid.SelectedItem as Products == null)
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                    var CurrentUser = ProductGrid.SelectedItem as Products;
                    //int IDSklad = AppDataConnect.bd.Storage.;

                    Products selectedPerson = ProductGrid.SelectedItem as Products;

                    if (selectedPerson != null)
                    {
                        selectedID = selectedPerson.ID;
                        // использовать выбранный ID здесь
                    }
                    

                        string temp = "";
                        temp += Convert.ToString(selectedID);

                        SqlConnection connection = new SqlConnection(connectionString);
                        connection.Open();

                        string query1 = $"Delete FROM ProductStock Where IDProduct = {temp}";
                        SqlCommand command1 = new SqlCommand(query1, connection);

                        string query2 = $"Delete FROM Request Where IDProduct = {temp}";
                        SqlCommand command2 = new SqlCommand(query2, connection);

                        string query3 = $"Delete FROM RelocationPlan Where IDProduct = {temp}";
                        SqlCommand command3 = new SqlCommand(query3, connection);

                        string query4 = $"Delete FROM Applications Where IDProduct = {temp}";
                        SqlCommand command4 = new SqlCommand(query4, connection);

                        string query5 = $"Delete FROM Products Where ID = {temp}";
                        SqlCommand command5 = new SqlCommand(query5, connection);

                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        command3.ExecuteNonQuery();
                        command4.ExecuteNonQuery();
                        command5.ExecuteNonQuery();
                        connection.Close();

                    ProductGrid.ItemsSource = AppDataConnect.bd.Products.ToList();
                    MessageBox.Show("данные удалены");
                    }


                
            }
        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            //Products products = new Products();
            if (String.IsNullOrEmpty(NameProductTxb.Text))
            {
                MessageBox.Show("Пустое название продукта");
            }

            else
            {
                if (String.IsNullOrEmpty(TypeTxb.Text))
                {
                    MessageBox.Show("Пустой тип продукта");
                }
                else
                {
                    if (String.IsNullOrEmpty(UnitTxb.Text))
                    {
                        MessageBox.Show("Пустая единица измерения продукта");
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(UnitPriceTxb.Text))
                        {
                            MessageBox.Show("Пустая цена за ед.");
                        }
                        else
                        {
                            var CurrentUser = AppDataConnect.bd.Products.FirstOrDefault(u => u.NameProduct == NameProductTxb.Text);

                            if (CurrentUser != null)
                            {
                                MessageBox.Show("Продукт уже существует");
                            }
                            else
                            {
                                string Name = NameProductTxb.Text;
                                string Type = TypeTxb.Text;
                                string Unit = UnitTxb.Text;
                                string Price = UnitPriceTxb.Text;


                                SqlConnection connection = new SqlConnection(connectionString);
                                connection.Open();
                                string query1 = $"INSERT INTO Products Values ('{Name}', '{Type}', '{Unit}', '{Price}')";
                                SqlCommand command1 = new SqlCommand(query1, connection);
                                //AppDataConnect.bd.Storage.Add(storage);
                                //AppDataConnect.bd.SaveChanges();
                                MessageBox.Show("Продукт добавлен");

                                command1.ExecuteNonQuery();
                                connection.Close();
                                ProductGrid.ItemsSource = AppDataConnect.bd.Products.ToList();
                            }
                        }
                    }
                }


                //if (ProductCombo.SelectedItem as Products == null)
                //{
                //    MessageBox.Show("Продукт пустой");
                //}
                //else
                //{
                //    var CurrentRole = ProductCombo.SelectedItem as Products;
                //    request.IDProduct = CurrentRole.ID;


                //    AppDataConnect.bd.Request.Add(request);
                //    AppDataConnect.bd.SaveChanges();
                //    MessageBox.Show("пользователь добавлен в базу данных");
                //    this.Close();
                //}

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductGrid.ItemsSource = AppDataConnect.bd.Products.ToList();
        }
    }
}
