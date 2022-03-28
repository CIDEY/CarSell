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

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void AddToCar(object sender, RoutedEventArgs e)
        {
            if (nameCarBox.Text == "" || modelCarBox.Text == "" || nameCarBox.Text == "" || HpBox.Text == "" || equipmentCarBox.Text == "" || priceCarBox.Text == "" || infoCarBox.Text == "" || colorCarBox.Text == "")
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Поля пусты. Введите значения!");
            }
            else
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
                var notificationManager = new NotificationManager();
                notificationManager.Show("Добавление авто", "Автомобиль добавлен в каталог.");
                NavigationService.Navigate(new KatalogPage());
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void NameCarTextInput(object sender, TextCompositionEventArgs e)
        {
            var nameCarBox = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(nameCarBox.Text, @"[A-Z]").Success)
            {
                regex = new Regex(@"[^a-z]");
            }
            else
            {
                regex = new Regex(@"[^A-Z]");
            }

            e.Handled = regex.IsMatch(e.Text);
        }

        private void NameCarTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(modelCarBox.Text, @"^[A-Z.0-9][a-z.0-9]+$"))
            {
                modelCarBox.Background = Brushes.White;
            }
            else if (Regex.IsMatch(modelCarBox.Text, @"^[a-z.0-9][A-z.0-9]+$"))
            {
                modelCarBox.ToolTip = "Это поле введено не корректно!";
                modelCarBox.Background = Brushes.Red;
            }
        }

        private void ModelCarTextInput(object sender, TextCompositionEventArgs e)
        {
            var nameCarBox = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(nameCarBox.Text, @"[A-Z.0-9]").Success)
            {
                regex = new Regex(@"[^a-z.0-9]");
            }
            else
            {
                regex = new Regex(@"[^A-Z.0-9]");
            }

            e.Handled = regex.IsMatch(e.Text);
        }

        private void ModelCarTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(modelCarBox.Text, @"^[A-Z.0-9][a-z.0-9]+$"))
            {
                modelCarBox.Background = Brushes.White;
            }
            else if (Regex.IsMatch(modelCarBox.Text, @"^[a-z.0-9][A-z.0-9]+$"))
            {
                modelCarBox.ToolTip = "Это поле введено не корректно!";
                modelCarBox.Background = Brushes.Red;
            }
        }

        private void HpCarTextInput(object sender, TextCompositionEventArgs e)
        {
            var nameCarBox = e.Source as TextBox;
            Regex regex;

            if (Regex.Match(nameCarBox.Text, @"[0-9]").Success)
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
            if (Regex.IsMatch(modelCarBox.Text, @"^[0-9]+$"))
            {
                modelCarBox.Background = Brushes.White;
            }
            else if (Regex.IsMatch(modelCarBox.Text, @"^[0-9]+$"))
            {
                modelCarBox.ToolTip = "Это поле введено не корректно!";
                modelCarBox.Background = Brushes.Red;
            }
        }
    }
}
