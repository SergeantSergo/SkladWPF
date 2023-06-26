using SkladWPF.AdoModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkladWPF.User_page
{
    /// <summary>
    /// Логика взаимодействия для ProductStokeWindow.xaml
    /// </summary>
    public partial class ProductStokeWindow : Window
    {
        //DateTime thisDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        DateTime Day = new DateTime();

        public ProductStokeWindow()
        {
            InitializeComponent();
            ProductCombo.ItemsSource = AppDataConnect.bd.Products.ToList();
            SkladCombo.ItemsSource = AppDataConnect.bd.Storage.ToList();

            //NumberTxb.Text = thisDay.ToString;

            //string formattedTime = string.Format(new TimeWordFormatter(), "{0:W}", now.TimeOfDay);
            //TimeText.Text = Convert.ToString.thisDay;
        }
       
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ProductStock Prod = new ProductStock();
            if (String.IsNullOrEmpty(NumberTxb.Text))
            {
                MessageBox.Show("Строка количество пуста");
            }
            else
            {
                if (String.IsNullOrEmpty(StahTxb.Text))
                {
                    MessageBox.Show("Строка со стелажами пуста");
                }
                else
                {
                    
                        try
                        {
                            Day = DateTime.Parse(TimeTxb.Text);
                            Prod.Number = NumberTxb.Text;
                            Prod.ExpirationDate = Day;
                        }
                        catch
                        {
                            MessageBox.Show("Дата введена неверно");
                            return;
                        }

                        if (ProductCombo.SelectedItem as Products == null)
                        {
                            MessageBox.Show("Продукт не выбран");
                        }
                        else
                        {
                            if (SkladCombo.SelectedItem as Storage == null)
                            {
                                MessageBox.Show("Склад не выбран");
                            }
                            else
                            {
                                Prod.Number = NumberTxb.Text;
                                Prod.Shelf = StahTxb.Text;
                                var CurrentRole = ProductCombo.SelectedItem as Products;
                                Prod.IDProduct = CurrentRole.ID;
                                var CurrentRole2 = SkladCombo.SelectedItem as Storage;
                                Prod.IDStorage = CurrentRole2.ID;


                                AppDataConnect.bd.ProductStock.Add(Prod);
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

        private void StahTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StahTxb.Text.Contains(" "))
            {
                StahTxb.Text = StahTxb.Text.Replace(" ", "");
                StahTxb.SelectionStart = StahTxb.Text.Length;
            }
        }

        private void StahTxb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void NumberTxb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }

        //private void CheckBX_Checked(object sender, RoutedEventArgs e)
        //{
            
        //        TimeLab.Visibility = Visibility.Visible;
        //        TimeTxb.Visibility = Visibility.Visible;
            

            
        //}

        //private void CheckBX_Unchecked(object sender, RoutedEventArgs e)
        //{
        //        TimeLab.Visibility = Visibility.Hidden;
        //        TimeTxb.Visibility = Visibility.Hidden;
        //}
    }
