using BitcoinWalletApp.Views;
using System;
using Xamarin.Forms;
using NBitcoin;
using Transactions;
using Xamarin.Forms.Xaml;
using BitcoinWalletApp.ViewModels;

namespace BitcoinWalletApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var properties = App.Current.Properties;

            if (!properties.ContainsKey("pubKey"))
            {
                CreateWallet wallet = new CreateWallet();
                properties.Add("pubKey", wallet.CreateKeys(Network.Main)["pubKey"]);
                properties.Add("privKey", wallet.CreateKeys(Network.Main)["privKey"]);
            }

            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
