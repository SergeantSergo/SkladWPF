using SkladWPF.AdoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

namespace SkladWPF.Manager_page
{
    /// <summary>
    /// Логика взаимодействия для DistributionPage.xaml
    /// </summary>
    public partial class DistributionPage : Page
    {
        DateTime Day = new DateTime();
        public DistributionPage()
        {
            InitializeComponent();
            ProductCombo.ItemsSource = AppDataConnect.bd.Products.ToList();
            Sklad1Combo.ItemsSource = AppDataConnect.bd.Storage.ToList();
            Sklad2Combo.ItemsSource = AppDataConnect.bd.Storage.ToList();
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            RelocationPlan relocation = new RelocationPlan();
            if (String.IsNullOrEmpty(NumberTxb.Text))
            {
                MessageBox.Show("Строка количество пуста");
            }

            else
            {
                try
                {
                    Day = DateTime.Parse(DateTxb.Text);
                    relocation.Number = NumberTxb.Text;
                    relocation.Date = Day;
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
                    if (Sklad1Combo.SelectedItem as Storage == null)
                    {
                        MessageBox.Show("Склад-поставщик не выбран");
                    }
                    else
                    {
                        if (Sklad2Combo.SelectedItem as Storage == null)
                        {
                            MessageBox.Show("Склад-приемник не выбран");
                        }
                        else
                        {
                            if (Sklad2Combo.SelectedItem as Storage == Sklad1Combo.SelectedItem as Storage)
                            {
                                MessageBox.Show("Невозможно Распределить продукцию в тот же склад");
                            }
                            else
                            {

                                var CurrentRole = ProductCombo.SelectedItem as Products;
                                relocation.IDProduct = CurrentRole.ID;
                                var CurrentRole2 = Sklad1Combo.SelectedItem as Storage;
                                relocation.Storage1 = CurrentRole2.Name;
                                var CurrentRole3 = Sklad2Combo.SelectedItem as Storage;
                                relocation.Storage2 = CurrentRole3.Name;


                                AppDataConnect.bd.RelocationPlan.Add(relocation);
                                AppDataConnect.bd.SaveChanges();
                                MessageBox.Show("Данные сохранены");
                                PlanGrid.ItemsSource = AppDataConnect.bd.RelocationPlan.ToList();
                            }
                        }
                    }

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlanGrid.ItemsSource = AppDataConnect.bd.RelocationPlan.ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить план?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var item = ((Button)sender).DataContext as RelocationPlan;
                AppDataConnect.bd.RelocationPlan.Remove(item);
                AppDataConnect.bd.SaveChanges();
                MessageBox.Show("Данные удалены");
                PlanGrid.ItemsSource = AppDataConnect.bd.RelocationPlan.ToList();

            }
        }

        private void NumberTxb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void NumberTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberTxb.Text.Contains(" "))
            {
                NumberTxb.Text = NumberTxb.Text.Replace(" ", "");
                NumberTxb.SelectionStart = NumberTxb.Text.Length;
            }
        }
    }
}

