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
    /// Логика взаимодействия для AddCarPage.xaml
    /// </summary>
    public partial class AddCarPage : Page
    {
        public AddCarPage()
        {
            InitializeComponent();
        }
        car_shopEntities car_ShopEntities = new car_shopEntities();
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

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void AddToCar(object sender, RoutedEventArgs e)
        {
            Car car = new Car()
            {
                Name = nameCarBox.Text,
                Model = modelCarBox.Text,
                Hp = Convert.ToInt32(HpBox.Text),
                Equipment = equipmentCarBox.Text,
                Price = Convert.ToInt32(priceCarBox.Text),
                InfoForCar = infoCarBox.Text,
                Color = colorCarBox.Text
            };

            car_ShopEntities.Cars1.Add(car);
            car_ShopEntities.SaveChanges();
            MessageBox.Show("Машина добавлена!");
            NavigationService.Navigate(new KatalogPage());
        }
    }
}
