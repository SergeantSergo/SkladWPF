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

namespace SkladWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InButton_Click(object sender, RoutedEventArgs e)
        {
            var CurrentUser = AppDataConnect.bd.Users.FirstOrDefault(u => u.Login == TextBoxLog.Text && u.Passwor == PassBox.Password);

            if (CurrentUser != null)
            {
                AppDataConnect.IDUse = CurrentUser.ID;
                switch (CurrentUser.IDRole)
                {
                    case 1:
                        UserWindow UserWindowWindow = new UserWindow();
                        UserWindowWindow.Show();
                        Close();
                        break;

                    case 2:
                        ManagerWindow ManagerWindow = new ManagerWindow();
                        ManagerWindow.Show();
                        Close();
                        break;

                    case 3:
                        AdminWindow AdminWindow = new AdminWindow();
                        AdminWindow.Show();
                        Close();
                        break;

                    default:
                        MessageBox.Show("Ошибка роли пользователя");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Данного пользователя нет в базе данных");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
            }
            else
            {
                this.Close();
            }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
