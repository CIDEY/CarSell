using Notification.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarSell.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        car_shopEntities car_ShopEntities2 = new car_shopEntities();

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            loginBox.Text = null;
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "")
            {
                loginBox.Text = "Введите логин";
                loginBox.Background = Brushes.White;
            }
        }

        private void Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            passBox.Text = null;
        }

        private void Pass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passBox.Text == "")
            {
                passBox.Text = "Введите пароль";
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void RollUp(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void LoginTextInput(object sender, TextCompositionEventArgs e)
        {
            var name = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(name.Text, @"[A-Z.0-9]").Success)
            {
                regex = new Regex(@"[^a-z.0-9]");
            }
            else
            {
                regex = new Regex(@"[^A-Z.0-9]");
            }

            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoginTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(loginBox.Text, @"^[A-Z.0-9][a-z.0-9]+$"))
            {
                loginBox.Background = Brushes.Green;
            }
            else if (Regex.IsMatch(loginBox.Text, @"^[a-z.0-9][A-z.0-9]+$"))
            {
                loginBox.ToolTip = "Это поле введено не корректно!";
                loginBox.Background = Brushes.Red;
            }
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.Trim();
            string pass = passBox.Text.Trim();
            string passAgain = passBoxAgain.Text.Trim();

            if (login.Length < 5)
            {
                loginBox.ToolTip = "Это поле введено не корректно!";
                //loginBox.Background = Brushes.DarkRed;
                loginBox.BorderBrush = Brushes.Red;
            }
            else if (pass.Length < 5)
            {
                passBox.ToolTip = "Это поле введено не корректно!";
                //passBox.Background = Brushes.DarkRed;
                passBox.BorderBrush = Brushes.Red;
            }
            else if (pass != passAgain)
            {
                passBoxAgain.ToolTip = "Данные не совпадают!";
                //passBoxAgain.Background = Brushes.DarkRed;
                passBoxAgain.BorderBrush = Brushes.Red;
                passBoxAgain.BorderThickness = new Thickness(3,3,3,3);
            }
            else
            {
                loginBox.ToolTip = "";
                loginBox.Background = Brushes.Transparent;
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                passBoxAgain.ToolTip = "";
                passBoxAgain.Background = Brushes.Transparent;

                Account account1 = new Account
                {
                    Login = loginBox.Text,
                    Password = passBox.Text,
                    Balance = 0
                };

                car_ShopEntities2.Accounts1.Add(account1);
                car_ShopEntities2.SaveChanges();
                var notificationManager = new NotificationManager();
                notificationManager.Show("Регистрация", "Регистрация учетной записи пройдена успешно!");

                NavigationService.Navigate(new AuthPage());

            }
        }
    }
}