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
    /// Логика взаимодействия для CreditPage.xaml
    /// </summary>
    public partial class CreditPage : Page
    {
        car_shopEntities car_ShopEntities = new car_shopEntities();
        public static double Balance;
        public CreditPage()
        {
            InitializeComponent();
        }
        private void BackToReplenish(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Replenish());
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void BusketPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void TakeToCredit(object sender, RoutedEventArgs e)
        {
            var Account = (from Accountt in car_ShopEntities.Accounts1 where Accountt.Login == KatalogPage.nameClientOnPage select Accountt).FirstOrDefault();
            int mounth = Convert.ToInt32(srok.Text);
            double mPay = Convert.ToDouble(mounthPay.Text);
            double money = Convert.ToInt32(summ.Text);

            if (!(bool)boolConfirm.IsChecked)
            {
                MessageBox.Show("Вы не приняли соглашение.");
            }

            else if ((bool)boolConfirm.IsChecked)
            {
                if (srok.Text == "" || summ.Text == "" || srok.Text == "Введите срок займа" || summ.Text == "Введите сумму")
                {
                    MessageBox.Show("Поля пусты. Введите значения!");
                }
                else
                { 
                    Account.Balance += money;
                    MessageBox.Show($"Счет пополнен на {summ.Text} рублей.");
                    car_ShopEntities.SaveChanges();
                    AccountPage.Balance += money;
                    NavigationService.Navigate(new AccountPage());
                }

                #region noActual
                //if (mounth < 12)
                //{
                //    Account.Balance += money;
                //    MessageBox.Show($"Счет пополнен на {summ.Text} рублей.");
                //    car_ShopEntities.SaveChanges();
                //    AccountPage.Balance += money;
                //    NavigationService.Navigate(new AccountPage());
                //}

                //else if (mounth > 12)
                //{
                //    double payment = (money / mounth) + money * 0.5833333333333333;
                //    money = Math.Round(payment, 2);
                //    Account.Balance += money;
                //    MessageBox.Show($"Счет пополнен на {summ.Text} рублей.");
                //    car_ShopEntities.SaveChanges();
                //    AccountPage.Balance += money;
                //    NavigationService.Navigate(new AccountPage());
                //}
                #endregion
            }
        }

        private void CreditMath(object sender, RoutedEventArgs e)
        {
            #region noActual
            //double mounth, mPay, money;

            //mounth = double.Parse(srok.Text);
            //mPay = double.Parse(mounthPay.Text);
            //money = double.Parse(summ.Text);



            //mPay = money / mounth;
            ////mPay = mounthPay.ToString();
            //mounthPay.Text = mPay.ToString();

            //string mounthPay1 = Convert.ToString(mounthPay.Text);

            //mounthPay1 = mounthPay.Text;
            #endregion
            if (srok.Text == "" || summ.Text == "" || srok.Text == "Введите срок займа" || summ.Text == "Введите сумму")
            {
                MessageBox.Show("Поля пусты. Введите значения!");
            }
            else
            {
                int mounth = Convert.ToInt32(srok.Text);
                int money = Convert.ToInt32(summ.Text);
                if (mounth < 12)
                {
                    double itog = money / mounth;
                    mounthPay.Text = Convert.ToString(itog);
                }
                else if (mounth > 12)
                {
                    double payment = (money / mounth) + money * 0.5833333333333333;
                    double itog = Math.Round(payment, 2);
                    mounthPay.Text = Convert.ToString(itog);
                }
            }
        }
    }
}