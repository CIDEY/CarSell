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
    /// Логика взаимодействия для CreditPage.xaml
    /// </summary>
    public partial class CreditPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public static double Balance;
        public CreditPage()
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

        private void BusketPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void TakeToCredit(object sender, RoutedEventArgs e)
        {
            var Account = (from Accountt in car_ShopEntities.Accounts1 where Accountt.Login == KatalogPage.nameClientOnPage select Accountt).FirstOrDefault();

            if (!(bool)boolConfirm.IsChecked)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Вы не приняли соглашение!");
            }

            else if ((bool)boolConfirm.IsChecked)
            {

                if (srok.Text == "" || summ.Text == "" || srok.Text == "Введите срок займа" || summ.Text == "Введите сумму")
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Ошибка", "Поля пусты. Введите значения!");
                }
                else
                {
                    int mounth = Convert.ToInt32(srok.Text);
                    double mPay = Convert.ToDouble(mounthPay.Text);
                    double money = Convert.ToInt32(summ.Text);
                    Account.Balance += money;
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Пополнение счета", $"Счет пополнен на {summ.Text} рублей.");
                    car_ShopEntities.SaveChanges();
                    AccountPage.Balance += money;
                    NavigationService.Navigate(new AccountPage());
                }

            }
        }

        private void CreditMath(object sender, RoutedEventArgs e)
        {
            if (srok.Text == "" || summ.Text == "" || srok.Text == "Введите срок займа" || summ.Text == "Введите сумму")
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Поля пусты. Введите значения!");
            }
            else
            {
                int mounth = Convert.ToInt32(srok.Text);
                int money = Convert.ToInt32(summ.Text);
                if (mounth < 12)
                {
                    double itog = money / mounth;
                    mounthPay.Text = Convert.ToString(itog);
                }
                else if (mounth > 12)
                {
                    double itog_credit = money * (0.07+(0.07/(1+0.07))*mounth-1);
                    double itog = Math.Round(itog_credit, 2);
                    mounthPay.Text = Convert.ToString(itog);
                }
            }
        }

        private void mounthCredit_GotFocus(object sender, RoutedEventArgs e)
        {
            srok.Text = null;
        }

        private void mounthCredit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (srok.Text == "")
            {
                srok.Text = "Введите срок займа";
            }
        }

        private void amountOfMoney_GotFocus(object sender, RoutedEventArgs e)
        {
            summ.Text = null;
        }

        private void amountOfMoney_LostFocus(object sender, RoutedEventArgs e)
        {
            if (summ.Text == "")
            {
                summ.Text = "Введите сумму";
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void HpCarTextInput(object sender, TextCompositionEventArgs e)
        {
            var srok = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(srok.Text, @"[0-9]").Success)
            {
                regex = new Regex(@"[^0-9]");
            }
            else
            {
                regex = new Regex(@"[^0-9]");
            }

            e.Handled = regex.IsMatch(e.Text);
        }

        private void HpCarTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(srok.Text, @"^[0-9]+$"))
            {
                srok.Background = Brushes.White;
            }
            else if (Regex.IsMatch(srok.Text, @"^[0-9]+$"))
            {
                srok.ToolTip = "Это поле введено не корректно!";
                srok.Background = Brushes.Red;
            }
        }
    }
}