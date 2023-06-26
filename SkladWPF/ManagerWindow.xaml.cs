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
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            Frame.Navigate(new Manager_page.ApplicationPage());
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

        private void SkladButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Manager_page.SkladPage());
        }

        private void ReplenishmentButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Manager_page.ReplenishmentPage());
        }

        private void ApplicationsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Manager_page.ApplicationPage());
        }

        private void DistributionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Manager_page.DistributionPage());
        }
    }
}
