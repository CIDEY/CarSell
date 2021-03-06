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
    /// Логика взаимодействия для BusketPage.xaml
    /// </summary>
    public partial class BusketPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public BusketPage()
        {
            InitializeComponent();
            var busketCar = from busket in car_ShopEntities.Baskets1 where busket.account.Login == KatalogPage.nameClientOnPage select busket;
            table.Items.Clear();
            table.ItemsSource = busketCar.ToList();
        }

        private void BackToKatalog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KatalogPage());
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
            KatalogPage.nameClientOnPage = null;
        }

        private void GoToCredit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void GoToFavorite(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavoritePage());
        }

        private void DeleteCarForBasket(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem == null)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Выберите автомобиль!");
            }

            else
            {
                car_ShopEntities.Baskets1.Remove(table.SelectedItem as Basket);
                var notificationManager = new NotificationManager();
                notificationManager.Show("Удаление", "Автомобиль удален из списка!");
                car_ShopEntities.SaveChanges();
                table.ItemsSource = car_ShopEntities.Baskets1.ToList();
            }
        }

        private void SumPrice(object sender, RoutedEventArgs e)
        {
            var busketCar = from busket in car_ShopEntities.Baskets1 where busket.account.Login == KatalogPage.nameClientOnPage select busket;
            var summ1 = 0;
            foreach (var item in busketCar)
            {
                summ1 += Convert.ToInt32(item.car.Price);
            }
            summPriceCar.Text = Convert.ToString(summ1);
        }

        private void UseToCoupon(object sender, RoutedEventArgs e)
        {
            if (summPriceCar.Text == "")
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Выведите сначала ИТОГ!");
            }
            else
            {
                var busketCar = from busket in car_ShopEntities.Baskets1 where busket.account.Login == KatalogPage.nameClientOnPage select busket;
                var summ1 = 0;
                foreach (var item in busketCar)
                {
                    summ1 += Convert.ToInt32(item.car.Price);
                }


                string coupon = couponBox.Text.Trim();

                var countCoupon = from Coupon in car_ShopEntities.Coupons1 where Coupon.Code == coupon select Coupon;
                if (countCoupon.Count() > 0)
                {
                    var couponUse = (from Coupon in car_ShopEntities.Coupons1 where couponBox.Text == Coupon.Code select Coupon.Discount).FirstOrDefault();
                    if (summ1 > couponUse)
                    {
                        int itogo = Convert.ToInt32(summPriceCar.Text);
                        int discount = Convert.ToInt32(Convert.ToInt32(couponUse));
                        itogo -= discount;
                        summPriceCar.Text = Convert.ToString(itogo);
                        checkCoupon.IsEnabled = false;
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Скидка", $"Скидка применена в размере - {discount} рублей");
                    }
                    else
                    {
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Ошибка", "У данного купона размер скидки выше общей цены вашей корзины.");
                    }
                }
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Ошибка", "Данный купон не существует.");
                }
            }
        }

        private void OrderAdd(object sender, RoutedEventArgs e)
        {
            if (summPriceCar.Text == "")
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Ошибка", "Выведите сначала ИТОГ.");
            }
            else
            {
                var countBalance1 = (from Account in car_ShopEntities.Accounts1 where Account.Login == KatalogPage.nameClientOnPage select Account).FirstOrDefault();

                int summPrice = Convert.ToInt32(summPriceCar.Text);

                if (countBalance1.Balance > summPrice)
                {
                    countBalance1.Balance -= summPrice;
                    
                    var listBusket = (from Buskets in car_ShopEntities.Baskets1 where Buskets.account.Login == KatalogPage.nameClientOnPage select Buskets).ToList();
                    foreach (var item in listBusket)
                    {
                        Phistory phistory = new Phistory
                        {
                            IdAccount = item.IdAccount,
                            IdCar = item.IdCar
                        };
                        car_ShopEntities.Phistories1.Add(phistory);
                    }
                    
                    
                    car_ShopEntities.Baskets1.RemoveRange(table.ItemsSource as List<Basket>);
                    car_ShopEntities.SaveChanges();
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Покупка совершена успешно", "Ожидаем вас в автосалоне.");
                    table.ItemsSource = null;
                }
                else
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Ошибка", "Покупка невозможна. Недостаточно средств.");
                }
            }
            
        }
    }
}