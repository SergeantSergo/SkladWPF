using SkladWPF.AdoModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SkladWPF.Manager_page
{
    /// <summary>
    /// Логика взаимодействия для AddReplenishWindow.xaml
    /// </summary>
    public partial class AddReplenishWindow : Window
    {
        DateTime Day = new DateTime();
        public AddReplenishWindow()
        {
            InitializeComponent();
            ProductCombo.ItemsSource = AppDataConnect.bd.Products.ToList();
            StorageCombo.ItemsSource = AppDataConnect.bd.Storage.ToList();
            SuppliersCombo.ItemsSource = AppDataConnect.bd.Suppliers.ToList();

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //TimeText.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Applications application = new Applications();
            if (String.IsNullOrEmpty(NumberTxb.Text))
            {
                MessageBox.Show("Строка количество пуста");
            }
            else
            {
                application.Number = NumberTxb.Text;
                //request.Date = thisDay;

                if (ProductCombo.SelectedItem as Products == null)
                {
                    MessageBox.Show("Продукт не выбран");
                }
                else
                {
                    if (StorageCombo.SelectedItem as Storage == null)
                    {
                        MessageBox.Show("Склад не выбран");
                    }
                    else
                    {
                        if (SuppliersCombo.SelectedItem as Suppliers == null)
                        {
                            MessageBox.Show("Поставщик не выбран");
                        }
                        else
                        {
                            try
                            {
                                Day = DateTime.Parse(TimeTxb.Text);
                            }
                            catch
                            {
                                MessageBox.Show("Дата введена неверно");
                                return;
                            }
                            //var CurrentUser = AppDataConnect.bd.Users.FirstOrDefault(u => u.ID == u.ID);
                            application.Date = Day;
                            var CurrentRole = ProductCombo.SelectedItem as Products;
                            application.IDProduct = CurrentRole.ID;
                            var CurrentRole2 = StorageCombo.SelectedItem as Storage;
                            application.IDStorage = CurrentRole2.ID;
                            var CurrentRole3 = SuppliersCombo.SelectedItem as Suppliers;
                            application.IDSupplier = CurrentRole3.ID;
                            application.IDEmployees = AppDataConnect.IDUse; //

                            AppDataConnect.bd.Applications.Add(application);
                            AppDataConnect.bd.SaveChanges();
                            MessageBox.Show("Данные сохранены");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void NumberTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberTxb.Text.Contains(" "))
            {
                NumberTxb.Text = NumberTxb.Text.Replace(" ", "");
                NumberTxb.SelectionStart = NumberTxb.Text.Length;
            }
        }

        private void NumberTxb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
