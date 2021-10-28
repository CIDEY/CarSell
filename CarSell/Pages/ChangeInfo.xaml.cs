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
    /// Логика взаимодействия для ChangeInfo.xaml
    /// </summary>
    public partial class ChangeInfo : Page
    {
        car_shopEntities car_ShopEntities1 = new car_shopEntities();
        public ChangeInfo(string login, string pass, Account account)
        {
            InitializeComponent();
            loginBox.Text = login;
            passBox.Text = pass;
            Helper.a = account.Id;
        }

        private void BackToProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void GoToBasket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void GoToFavorite(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavoritePage());
        }

        private void ChangeLogin(object sender, RoutedEventArgs e)
        {
            var p = (from Account in car_ShopEntities1.Accounts1 where Account.Id == Helper.a select Account).FirstOrDefault();
            p.Login = loginBox.Text;
            car_ShopEntities1.SaveChanges();
            MessageBox.Show("Изменение внесены");
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            var p = (from Account in car_ShopEntities1.Accounts1 where Account.Id == Helper.a select Account).FirstOrDefault();
            p.Password = passBox.Text;
            car_ShopEntities1.SaveChanges();
            MessageBox.Show("Изменения внесены");
        }
    }
}
