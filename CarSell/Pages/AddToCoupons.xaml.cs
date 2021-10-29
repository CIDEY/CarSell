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
    /// Логика взаимодействия для AddToCoupons.xaml
    /// </summary>
    public partial class AddToCoupons : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public AddToCoupons()
        {
            InitializeComponent();
        }

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
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

        private void AddToCoupon(object sender, RoutedEventArgs e)
        {
            Coupon coupon = new Coupon()
            {
                Code = couponTxtBox.Text,
                Discount = Convert.ToInt32(discountSum.Text)
            };

            car_ShopEntities.Coupons1.Add(coupon);
            car_ShopEntities.SaveChanges();
            MessageBox.Show("Купон добавлен!");
            NavigationService.Navigate(new Coupons());
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void DiscountTextInput(object sender, TextCompositionEventArgs e)
        {
            var discountSum = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(discountSum.Text, @"[0-9]").Success)
            {
                regex = new Regex(@"[^0-9]");
            }
            else
            {
                regex = new Regex(@"[^0-9]");
            }

            e.Handled = regex.IsMatch(e.Text);
        }

        private void DiscountTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(discountSum.Text, @"^[0-9]+$"))
            {
                discountSum.Background = Brushes.White;
            }
            else if (Regex.IsMatch(discountSum.Text, @"^[0-9]+$"))
            {
                discountSum.ToolTip = "Это поле введено не корректно!";
                discountSum.Background = Brushes.Red;
            }
        }
    }
}
