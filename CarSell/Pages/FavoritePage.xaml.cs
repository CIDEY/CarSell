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
    /// Логика взаимодействия для FavoritePage.xaml
    /// </summary>
    public partial class FavoritePage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public FavoritePage()
        {
            InitializeComponent();
            var favoriteCar = from favorites in car_ShopEntities.Favorites1 where favorites.account.Login == KatalogPage.nameClientOnPage select favorites;
            table.ItemsSource = car_ShopEntities.Favorites1.ToList();
        }

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void GoToCredit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void DeleteToFavorite(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                MessageBox.Show("Выберите автомобиль");
            }

            else
            {
                car_ShopEntities.Favorites1.Remove(table.SelectedItem as Favorite);
                MessageBox.Show("Автомобиль удален из списка.");
                car_ShopEntities.SaveChanges();
                table.ItemsSource = car_ShopEntities.Favorites1.ToList();
            }
        }

        private void CarToBusket(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                MessageBox.Show("Выберите автомобиль");
            }
            else
            {
                var idAcc = (from Account in car_ShopEntities.Accounts1 where Account.Login == KatalogPage.nameClientOnPage select Account).FirstOrDefault();
                Basket baskets = new Basket
                {
                    account = idAcc,
                    car = table.SelectedItem as Car
                };

                car_ShopEntities.Baskets1.Add(baskets);
                car_ShopEntities.SaveChanges();
                MessageBox.Show("Машина добавлена в корзину.");
            }
        }
    }
}
