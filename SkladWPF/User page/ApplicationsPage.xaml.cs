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

namespace SkladWPF.User_page
{
    /// <summary>
    /// Логика взаимодействия для ApplicationsPage.xaml
    /// </summary>
    public partial class ApplicationsPage : Page
    {
        public ApplicationsPage()
        {
            InitializeComponent();

        }

        private void AddAppButton_Click(object sender, RoutedEventArgs e)
        {
            AppWindow AppWindow = new AppWindow();
            AppWindow.ShowDialog();
            NavigationService.Navigate(new User_page.ApplicationsPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationGrid.ItemsSource = AppDataConnect.bd.Request.ToList();
        }

        private void textBox_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ApplicationGrid.ItemsSource = AppDataConnect.bd.Request.Where(item => item.Products.NameProduct.Contains(textBox_search.Text) 
                || item.Number.Contains(textBox_search.Text) 
                || item.Storage.Name.Contains(textBox_search.Text)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditData_Click(object sender, RoutedEventArgs e)
        {
            //AppWindow AppWindow = new AppWindow((sender as Button).DataContext as employee);
            //AppWindow.Show();
        }

        private void DelAppButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (ApplicationGrid.SelectedItem as Request == null)
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                    var CurrentUser = ApplicationGrid.SelectedItem as Request;
                    AppDataConnect.bd.Request.Remove(CurrentUser);
                    AppDataConnect.bd.SaveChanges();

                    ApplicationGrid.ItemsSource = AppDataConnect.bd.Request.ToList();
                    MessageBox.Show("данные удалены");
                }
            }
        }
    }
}
