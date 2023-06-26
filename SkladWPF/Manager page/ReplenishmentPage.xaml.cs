using SkladWPF.AdoModel;
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

namespace SkladWPF.Manager_page
{
    /// <summary>
    /// Логика взаимодействия для ReplenishmentPage.xaml
    /// </summary>
    public partial class ReplenishmentPage : Page
    {
        public ReplenishmentPage()
        {
            InitializeComponent();
            //var CurrentUser = ApplicationGrid.Items as Applications;
            //for (int i = ApplicationGrid.Items.Count - 1; i >= 0; i--)
            //{

            //    AppDataConnect.bd.Applications.Remove(CurrentUser);
            //    AppDataConnect.bd.SaveChanges();

            //    ApplicationGrid.ItemsSource = AppDataConnect.bd.Applications.ToList();
            //    MessageBox.Show("данные удалены");
            //}
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddReplenishWindow addReplenishWindow = new AddReplenishWindow();
            addReplenishWindow.ShowDialog();
            NavigationService.Navigate(new Manager_page.ReplenishmentPage());
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (ApplicationGrid.SelectedItem as Applications == null)
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                    var CurrentUser = ApplicationGrid.SelectedItem as Applications;
                    AppDataConnect.bd.Applications.Remove(CurrentUser);
                    AppDataConnect.bd.SaveChanges();

                    ApplicationGrid.ItemsSource = AppDataConnect.bd.Applications.ToList();
                    MessageBox.Show("данные удалены");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationGrid.ItemsSource = AppDataConnect.bd.Applications.ToList();
        }

        private void SearchTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ApplicationGrid.ItemsSource = AppDataConnect.bd.Applications.Where(item => item.Products.NameProduct.Contains(SearchTxb.Text) || item.Number.Contains(SearchTxb.Text) 
                || item.Suppliers.Name.Contains(SearchTxb.Text) || item.Storage.Name.Contains(SearchTxb.Text)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
