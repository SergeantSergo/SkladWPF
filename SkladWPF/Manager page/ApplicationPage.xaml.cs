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
    /// Логика взаимодействия для ApplicationPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        public ApplicationPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationGrid.ItemsSource = AppDataConnect.bd.Request.ToList();
        }

        private void SearchTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ApplicationGrid.ItemsSource = AppDataConnect.bd.Request.Where(item => item.Products.NameProduct.Contains(SearchTxb.Text) 
                || item.Number.Contains(SearchTxb.Text) 
                || item.Storage.Name.Contains(SearchTxb.Text)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
