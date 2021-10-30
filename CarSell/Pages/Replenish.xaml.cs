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
    /// Логика взаимодействия для Replenish.xaml
    /// </summary>
    public partial class Replenish : Page
    {
        public Replenish()
        {
            InitializeComponent();
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void BackToProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
            KatalogPage.nameClientOnPage = null;
        }

        private void ToReplenish(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReplenishNextPage());
        }

        private void GoToCreditPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreditPage());
        }

        private void BusketPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void GoToFavorite(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavoritePage());
        }
    }
}
