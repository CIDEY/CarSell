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
    /// Логика взаимодействия для EditCarPage.xaml
    /// </summary>
    public partial class EditCarPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public EditCarPage(string name, string model, int hp, string equipment, int price, string infoCar, string color, Car car)
        {
            InitializeComponent();
            nameCar1.Text = name;
            model1.Text = model;
            hp1.Text = Convert.ToString(hp);
            equipment1.Text = equipment;
            price1.Text = Convert.ToString(price);
            infoForCar1.Text = Convert.ToString(infoCar);
            color1.Text = color;
            Helper.a = car.Id;
        }

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void GoToAuthPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
            KatalogPage.nameClientOnPage = null;
        }

        private void GoToProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
        }

        private void GoToBusket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void GoToFavorite(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavoritePage());
        }

        private void GoToCredit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void GoToCoupons(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Coupons());
        }

        private void EditCar(object sender, RoutedEventArgs e)
        {
            var p = (from Car in car_ShopEntities.Cars1 where Car.Id == Helper.a select Car).FirstOrDefault();
            p.Name = nameCar1.Text;
            p.Model = model1.Text;
            p.Hp = Convert.ToInt32(hp1.Text);
            p.Equipment = equipment1.Text;
            p.Price = Convert.ToInt32(price1.Text);
            p.InfoForCar = infoForCar1.Text;
            p.Color = color1.Text;
            car_ShopEntities.SaveChanges();
            var notificationManager = new NotificationManager();
            notificationManager.Show("Изменение внесены!", "В данные автомобиля внесены изменения.");
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
            if (Regex.IsMatch(nameCar1.Text, @"^[A-Z.0-9][a-z.0-9]+$"))
            {
                nameCar1.Background = Brushes.White;
            }
            else if (Regex.IsMatch(nameCar1.Text, @"^[a-z.0-9][A-z.0-9]+$"))
            {
                nameCar1.ToolTip = "Это поле введено не корректно!";
                nameCar1.Background = Brushes.Red;
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
            if (Regex.IsMatch(model1.Text, @"^[A-Z.0-9][a-z.0-9]+$"))
            {
                model1.Background = Brushes.White;
            }
            else if (Regex.IsMatch(model1.Text, @"^[a-z.0-9][A-z.0-9]+$"))
            {
                model1.ToolTip = "Это поле введено не корректно!";
                model1.Background = Brushes.Red;
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
            if (Regex.IsMatch(hp1.Text, @"^[0-9]+$"))
            {
                hp1.Background = Brushes.White;
            }
            else if (Regex.IsMatch(hp1.Text, @"^[0-9]+$"))
            {
                hp1.ToolTip = "Это поле введено не корректно!";
                hp1.Background = Brushes.Red;
            }
        }
    }
}