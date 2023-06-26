
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
using static System.Net.Mime.MediaTypeNames;

namespace SkladWPF
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            Frame.Navigate(new User_page.InfoPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {

            }
            else
            {
                MainWindow Auth = new MainWindow();
                Auth.Show();
                Close();
            }
        }


        private void InfoButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new User_page.InfoPage());
        }

        private void ApplicationsButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new User_page.ApplicationsPage());
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new User_page.ProductsPage());
        }

        private void ExitButton_Click_1(object sender, RoutedEventArgs e)
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

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
