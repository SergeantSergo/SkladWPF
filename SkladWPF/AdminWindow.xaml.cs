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
using System.Windows.Shapes;

namespace SkladWPF
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public class NavigationManager
        {
            public static Frame StartFrame { get; set; }
        }

        public AdminWindow()
        {
            InitializeComponent();
            
            Frame.Navigate(new User_page.ApplicationsPage());
            NavigationManager.StartFrame = Frame;
            
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                //do yes stuff
                MainWindow Auth = new MainWindow();
                Auth.Show();
                Close();
            }
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new User_page.ApplicationsPage());
        }

        private void ProductStrockButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Admin_page.StoragePage());
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Admin_page.UserPage());
        }

        private void ApplicationsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Manager_page.ReplenishmentPage());
        }

        private void ReloacationButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Manager_page.DistributionPage());
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Admin_page.ProductsPage());
        }
    }
}
