using System;
using System.Collections.Generic;
using Transactions;
using QRCodeKeys;
using NBitcoin;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Platform.Android;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public static User user { get; set; } = new User();

        public string[] pubKey { get; set; } = { (string)App.Current.Properties["pubKey"] };

        public MainPage()
        {
            InitializeComponent();
            UserInitialize();

            this.BindingContext = this;

        }

        public void UserInitialize()
        {
            UserPubKey.Text = user.pubKey;
            UserBalance.Text = user.GetBalance(Network.Main, MoneyUnit.BTC).ToString();
            UserQRCodeKey.Source = user.GetQRKey();
        }
    }
}