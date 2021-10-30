using Notification.Wpf;
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
    /// Логика взаимодействия для Coupons.xaml
    /// </summary>
    public partial class Coupons : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public Coupons()
        {
            InitializeComponent();
            table.ItemsSource = car_ShopEntities.Coupons1.ToList();
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

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void DeleteCoupon(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Выберите купон для удаления");
            }

            else
            {
                car_ShopEntities.Coupons1.Remove(table.SelectedItem as Coupon);
                var notificationManager = new NotificationManager();
                notificationManager.Show("Успешно!", "Купон удален из списка.");
                car_ShopEntities.SaveChanges();
                NavigationService.Navigate(new Coupons());
            }
        }

        private void GoToAddCoupon(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddToCoupons());
        }

        private void EditCoupon(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Выберите купон для внесения изменений");
            }

            else
            {
                var k = table.SelectedItem as Coupon;
                NavigationService.Navigate(new EditCouponsPage(k.Code, Convert.ToInt32(k.Discount), k));
            }
        }
    }
}
