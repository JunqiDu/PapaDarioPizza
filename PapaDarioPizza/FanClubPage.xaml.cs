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
    public sealed partial class FanClubPage : Page
    {
        public FanClubPage()
        {
            this.InitializeComponent();
        }

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

        public void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            FanClubs f = new FanClubs();
            f.FanEmail = tbEmail.Text;
            f.FanPassword = tbPassword.Text;
            f.FanNickname = tbName.Text;
            f.AddFan((App.Current as App).ConnectionString);
            tbResult.Text = "Welcome to join us!";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            FanClubs f = new FanClubs();
            f.FanEmail = tbEmail.Text;
            f.FanPassword = tbPassword.Text;
            f.FanNickname = tbName.Text;
            tbResult.Text = f.Login((App.Current as App).ConnectionString);
        }
    }
}
