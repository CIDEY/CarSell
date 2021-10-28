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
    /// Логика взаимодействия для KatalogPage.xaml
    /// </summary>
    public partial class KatalogPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();

        public KatalogPage()
        {
            InitializeComponent();
            table.ItemsSource = car_ShopEntities.Cars1.ToList();
            var idAcc = (from Account in car_ShopEntities.Accounts1 where Account.Login == nameClientOnPage select Account).FirstOrDefault();

            if (idAcc.Type == "admin")
            {
                addCarbtn.Visibility = Visibility.Visible;
                editCarBtn.Visibility = Visibility.Visible;
                deleteCarBtn.Visibility = Visibility.Visible;
                couponBtn.Visibility = Visibility.Visible;
            }
            else
            {
                addCarbtn.Visibility = Visibility.Hidden;
                editCarBtn.Visibility = Visibility.Hidden;
                deleteCarBtn.Visibility = Visibility.Hidden;
                couponBtn.Visibility = Visibility.Hidden;
            }
        }

        public static string nameClientOnPage;

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void GoToAccInfo(object sender, RoutedEventArgs e)
        {
            var idLogin = (from Account in car_ShopEntities.Accounts1 where Account.Login == nameClientOnPage select Account.Login).FirstOrDefault();
            var balance = (from Account in car_ShopEntities.Accounts1 where Account.Login == nameClientOnPage select Account.Balance).FirstOrDefault();
            AccountPage.Login = idLogin;
            AccountPage.Balance = (int)balance;

            NavigationService.Navigate(new AccountPage());
        }

        private void BusketPage(object sender, RoutedEventArgs e)
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

        private void GoToBusket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void GoToCoupon(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Coupons());
        }

        private void GoToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void GoToAddCar(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCarPage());
        }

        private void GoToEditCar(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                MessageBox.Show("Выберите автомобиль");
            }

            else
            {
                var k = table.SelectedItem as Car;
                NavigationService.Navigate(new EditCarPage(k.Name, k.Model, Convert.ToInt32(k.Hp), k.Equipment, Convert.ToInt32(k.Price), k.InfoForCar, k.Color, k));
            }
        }

        private void RemoveCar(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                MessageBox.Show("Выберите автомобиль");
            }

            else
            {
                car_ShopEntities.Cars1.Remove(table.SelectedItem as Car);
                MessageBox.Show("Автомобиль удален из списка.");
                car_ShopEntities.SaveChanges();
                table.ItemsSource = car_ShopEntities.Cars1.ToList();
            }
        }

        private void ToFavorite(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                MessageBox.Show("Выберите автомобиль");
            }
            else
            {
                var idAcc = (from Account in car_ShopEntities.Accounts1 where Account.Login == KatalogPage.nameClientOnPage select Account).FirstOrDefault();
                Favorite favorite = new Favorite
                {
                    account = idAcc,
                    car = table.SelectedItem as Car
                };

                car_ShopEntities.Favorites1.Add(favorite);
                car_ShopEntities.SaveChanges();
                MessageBox.Show("Машина добавлена в избранное.");
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