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

namespace CarSell.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        car_shopEntities car_ShopEntities = new car_shopEntities();

        private void RegToAcc(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxLogin.Text = null;
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "")
            {
                textBoxLogin.Text = "Введите логин";
            }
        }

        private void RollUp(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void EnterToAcc(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPassword.Password.Trim();

            if (login.Length < 5 && login.Length > 16)
            {
                textBoxLogin.ToolTip = "Это поле введено не корректно!";
                textBoxLogin.Background = Brushes.DarkRed;
            }

            else if (pass.Length < 5 && pass.Length > 16)
            {
                textBoxPassword.ToolTip = "Это поле введено не корректно!";
                textBoxPassword.Background = Brushes.DarkRed;
            }

            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                textBoxPassword.ToolTip = "";
                textBoxPassword.Background = Brushes.Transparent;

                var countLoginAndPassword = from Account in car_ShopEntities.Accounts1 where Account.Login == login && Account.Password == pass select Account;

                if (countLoginAndPassword.Count() > 0)
                {
                    MessageBox.Show("Вы успешно вошли в аккаунт!", "Вход");
                    var clientFromDB = (from Account in car_ShopEntities.Accounts1 where Account.Login == login && Account.Password == pass select Account.Login).FirstOrDefault();
                    KatalogPage.nameClientOnPage = clientFromDB;
                    NavigationService.Navigate(new KatalogPage());
                }

                else
                {
                    MessageBox.Show("Вы неправильно ввели логин или пароль, пожалуйста проверьте правильность введенных данных", "Вход");
                }
            }
        }
    }
}
