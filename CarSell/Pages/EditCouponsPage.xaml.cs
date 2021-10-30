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
    /// Логика взаимодействия для EditCouponsPage.xaml
    /// </summary>
    public partial class EditCouponsPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public EditCouponsPage(string code, int discount, Coupon coupons)
        {
            InitializeComponent();
            couponName1.Text = code;
            discountSize.Text = Convert.ToString(discount);
            Helper.a = coupons.Id;
        }

        private void BackToCoupons(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Coupons());
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
            KatalogPage.nameClientOnPage = null;
        }

        private void GoToAccInfo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
        }

        private void GoToBusket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void GoToFavoritePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavoritePage());
        }

        private void GoToCredit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void GoToCoupon(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Coupons());
        }

        private void EditCoupon(object sender, RoutedEventArgs e)
        {
            var p = (from Coupon in car_ShopEntities.Coupons1 where Coupon.Id == Helper.a select Coupon).FirstOrDefault();
            p.Code = couponName1.Text;
            p.Discount = Convert.ToInt32(discountSize.Text);
            car_ShopEntities.SaveChanges();
            var notificationManager = new NotificationManager();
            notificationManager.Show("Изменение внесены!", "В данные купона внесены изменения.");
            NavigationService.Navigate(new Coupons());
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void HpCarTextInput(object sender, TextCompositionEventArgs e)
        {
            var discountSize = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(discountSize.Text, @"[0-9]").Success)
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
            if (Regex.IsMatch(discountSize.Text, @"^[0-9]+$"))
            {
                discountSize.Background = Brushes.White;
            }
            else if (Regex.IsMatch(discountSize.Text, @"^[0-9]+$"))
            {
                discountSize.ToolTip = "Это поле введено не корректно!";
                discountSize.Background = Brushes.Red;
            }
        }
    }
}
