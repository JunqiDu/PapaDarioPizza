using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PapaDarioPizza
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class OrderPage : Page
    {
        public OrderPage()
        {
            this.InitializeComponent();
        }

        //here are code to swap to different page
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OrderPage));
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FanClubPage));
        }

        private void HyperlinkButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ContactUsPage));
        }

        private void HyperlinkButton_Click_3(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        //all value may used in order
        public int pizzaSizePrice;
        public int pizzaToppingsPrice;
        public int pizzaPrice;
        public int sandwichPrice;
        public int friesPrice = 3;
        public int poutinePrice = 5;
        public int wingsPrice = 10;
        public int snackPrice;
        public int drinkingPrice;
        public double totalPrice;

        public void PizzaSize_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string pizzaSize = rb.Tag.ToString();
                switch (pizzaSize)
                {
                    case "4 Inch":
                        pizzaSizePrice = 6;
                        break;
                    case "6 Inch":
                        pizzaSizePrice = 8;
                        break;
                    case "8 Inch":
                        pizzaSizePrice = 10;
                        break;
                }
            }
        }

        public void ToppingsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox[] checkboxes = new CheckBox[] { cbBeef, cbMushroom, cbTomatox };
            foreach (CheckBox c in checkboxes)
            {
                if (c.IsChecked == true)
                {
                    pizzaToppingsPrice++;
                }
            }
        }

        public void Sandwich_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string sandwich = rb.Tag.ToString();
                switch (sandwich)
                {
                    case "Yes":
                        sandwichPrice = 7;
                        break;
                    case "No":
                        sandwichPrice = 0;
                        break;
                }
            }
        }

        public void SnacksCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (cbFries.IsChecked == true)
            {
                snackPrice += friesPrice;
            }
            if (cbPoutine.IsChecked == true)
            {
                snackPrice += poutinePrice;
            }
            if (cbWings.IsChecked == true)
            {
                snackPrice += wingsPrice;
            }
        }

        public void Drinking_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string drinking = rb.Tag.ToString();
                switch (drinking)
                {
                    case "Coke":
                        drinkingPrice = 2;
                        break;
                    case "Sprite":
                        drinkingPrice = 2;
                        break;
                    case "NetsTea":
                        drinkingPrice = 2;
                        break;
                }
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Orders o = new Orders();

            totalPrice = pizzaSizePrice + pizzaToppingsPrice + sandwichPrice + sandwichPrice + drinkingPrice;

            string check = o.CheckFan(o.OrderEmail);
            if (check == "Fan")
            {
                totalPrice = totalPrice * 0.9;
            }

            o.PizzaSize = pizzaSizePrice.ToString();
            o.PizzaTopping = pizzaToppingsPrice.ToString();
            o.Sandwich = sandwichPrice.ToString();
            o.Snack = snackPrice.ToString();
            o.Drinking = drinkingPrice.ToString();
            o.OrderEmail = tbEmail.Text;
            o.TotalPrice = totalPrice;
            o.AddOrder((App.Current as App).ConnectionString);

            tbSubmit.Text = "Your order success! The total price is " + totalPrice.ToString();
        }
    }
}
