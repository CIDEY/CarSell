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
    /// Логика взаимодействия для ReplenishNextPage.xaml
    /// </summary>
    public partial class ReplenishNextPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public static double Balance;
        public ReplenishNextPage()
        {
            InitializeComponent();
        }

        private void BackToReplenish(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
            KatalogPage.nameClientOnPage = null;
        }

        private void pay_GotFocus(object sender, RoutedEventArgs e)
        {
            pay.Text = null;
        }

        private void pay_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pay.Text == "")
            {
                pay.Text = "Введите сумму пополнения";
            }
        }

        private void PayTextInput(object sender, TextCompositionEventArgs e)
        {
            var name = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(name.Text, @"[0-9]").Success)
            {
                regex = new Regex(@"[^0-9]");
            }
            else
            {
                regex = new Regex(@"[^0-9]");
            }

            e.Handled = regex.IsMatch(e.Text);
        }

        private void PayTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(pay.Text, @"^[0-9]+$"))
            {
                pay.Background = Brushes.White;
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void BusketPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void AddToPayBalance(object sender, RoutedEventArgs e)
        {
            var Account = (from Accountt in car_ShopEntities.Accounts1 where Accountt.Login == KatalogPage.nameClientOnPage select Accountt).FirstOrDefault();
            if (pay == null || pay.Text == "Введите сумму")
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Не удалось пополнить счет. Что-то пошло не так!");
            }
            else
            {
                double money = Convert.ToDouble(pay.Text);

                if (money < 100000 & money > 0)
                {
                    double procent = 0.2;
                    double comission = money * procent;
                    money = money - (comission);
                    Account.Balance += money;
                    car_ShopEntities.SaveChanges();
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Пополнение", $"Счет пополнен на {money}₽. Комиссия составила {comission}₽.");
                    NavigationService.Navigate(new AccountPage());
                }

                else if (money > 100000)
                {
                    Account.Balance += Convert.ToDouble(pay.Text);
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Пополнение", $"Счет пополнен на {money}₽.");
                    car_ShopEntities.SaveChanges();
                    NavigationService.Navigate(new AccountPage());
                }

                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Ошибка", "Не удалось пополнить счет. Что-то пошло не так!");
                }
            }
        }
    }
}