using SkladWPF.AdoModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SkladWPF.User_page
{
    /// <summary>
    /// Логика взаимодействия для AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        DateTime thisDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            
            //new DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Year.ToString("0000"); //получаем строку вида ДД.ММ.ГГГГ

        //private employee _Correctemploy = new employee();
        public AppWindow()
        {

            InitializeComponent();
            ProductCombo.ItemsSource = AppDataConnect.bd.Products.ToList();
            StorageCombo.ItemsSource = AppDataConnect.bd.Storage.ToList();

            CompositionTarget.Rendering += CompositionTarget_Rendering;

        }
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            TimeText.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            if (String.IsNullOrEmpty(NumberTxb.Text))
            {
                MessageBox.Show("Строка количество пуста");
            }

            else
            {
                request.Number = NumberTxb.Text;
                request.Date = thisDay;
                request.IDStorage = 1;


                if (ProductCombo.SelectedItem as Products == null)
                {
                    MessageBox.Show("Продукт не выбран");
                }
                else
                {
                    if (StorageCombo.SelectedItem as Storage == null)
                    {
                        MessageBox.Show("Склад не выбран");
                    }
                    else
                    {
                        var CurrentRole = ProductCombo.SelectedItem as Products;
                        request.IDProduct = CurrentRole.ID;
                        var CurrentRole2 = StorageCombo.SelectedItem as Storage;
                        request.IDStorage = CurrentRole2.ID;


                        AppDataConnect.bd.Request.Add(request);
                        AppDataConnect.bd.SaveChanges();
                        MessageBox.Show("Данные сохранены");
                        this.Close();
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

        private void NumberTxb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}

