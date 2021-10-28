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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public static double Balance;
        public static string Login;
        public AccountPage()
        {
            InitializeComponent();
            login.Content = Login;
            balance.Content = Balance;
            var pHistory = from phistorys in car_ShopEntities.Phistories1 where phistorys.account.Login == KatalogPage.nameClientOnPage select phistorys.car1;
            table.Items.Clear();
            table.ItemsSource = pHistory.ToList();
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void EditAcc(object sender, RoutedEventArgs e)
        {
            var k = (from Account in car_ShopEntities.Accounts1 where Account.Login == KatalogPage.nameClientOnPage select Account).FirstOrDefault();
            NavigationService.Navigate(new ChangeInfo(k.Login, k.Password, k));
        }

        private void ReplenishBtn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void GoToCreditPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void GoToFavorite(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavoritePage());
        }

        private void GoToBasket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }
    }
}
