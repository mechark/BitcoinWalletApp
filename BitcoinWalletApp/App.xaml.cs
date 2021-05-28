using BitcoinWalletApp.Views;
using System;
using Xamarin.Forms;
using NBitcoin;
using Transactions;
using Xamarin.Forms.Xaml;
using BitcoinWalletApp.ViewModels;
using System.Collections.Generic;
using Android.Preferences;
using Xamarin.Essentials;

namespace BitcoinWalletApp
{
    public partial class App : Application
    {
        protected User User { get => new User(); }
        public App()
        {
            InitializeComponent();
            VersionTracking.Track();
            OnStart();

            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
     //       if (VersionTracking.IsFirstLaunchEver)
    //        {
                CreateWallet wallet = new CreateWallet();

                App.Current.Properties["pubKey"] = wallet.CreateKeys(Network.Main)["pubKey"];
                App.Current.Properties["privKey"] = wallet.CreateKeys(Network.Main)["privKey"];

                App.Current.Properties["UserBalance"] = 0;
       //     }
            // Только для тестирования. Должно быть удалено.
            if (User.HasTransactions)
            {
                UserInfo UserInfoWithTransactoins = new UserInfo("3JwMCMFL1edCTNxYmi52RszotYxRDm2MGn", true);

                App.Current.Properties["UserTransactionsTime"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsDateTime().ToArray());
                App.Current.Properties["UserTransactionsSum"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsAmount(MoneyUnit.BTC).ToArray());
                App.Current.Properties["UserTransactionType"] = String.Join(", ", UserInfoWithTransactoins.GetTypeOfTransaction().ToArray());
            } 
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
