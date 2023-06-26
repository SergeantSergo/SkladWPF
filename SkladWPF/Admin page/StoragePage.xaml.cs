using SkladWPF.AdoModel;
using SkladWPF.User_page;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для StoragePage.xaml
    /// </summary>
    public partial class StoragePage : Page
    {
        public StoragePage()
        {
            InitializeComponent();
        }
        private void textBox_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ProductGrid.ItemsSource = AppDataConnect.bd.ProductStock.Where(item => item.Products.NameProduct.Contains(textBox_search.Text) || item.Number.Contains(textBox_search.Text)
                || item.Shelf.Contains(textBox_search.Text) || item.Storage.Name.Contains(textBox_search.Text)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductGrid.ItemsSource = AppDataConnect.bd.ProductStock.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ProductStokeWindow AppWindow = new ProductStokeWindow();
            AppWindow.ShowDialog();
            NavigationService.Navigate(new Admin_page.StoragePage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (ProductGrid.SelectedItem as ProductStock == null)
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                    var CurrentUser = ProductGrid.SelectedItem as ProductStock;
                    AppDataConnect.bd.ProductStock.Remove(CurrentUser);
                    AppDataConnect.bd.SaveChanges();

                    ProductGrid.ItemsSource = AppDataConnect.bd.ProductStock.ToList();
                    MessageBox.Show("данные удалены");
                }

            }
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            StorageAddWindow storageAddWindow = new StorageAddWindow();
            storageAddWindow.ShowDialog();
            NavigationService.Navigate(new Admin_page.StoragePage());
        }
    }
}
