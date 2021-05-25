using BitcoinWalletApp.Views;
using System;
using Xamarin.Forms;
using NBitcoin;
using Transactions;
using Xamarin.Forms.Xaml;
using BitcoinWalletApp.ViewModels;
using System.Collections.Generic;

namespace BitcoinWalletApp
{
    public partial class App : Application
    {
        User User { get => new User(); }

        public App()
        {
            InitializeComponent();

            if (!App.Current.Properties.ContainsKey("UserBalance"))
            {
                OnStart();
            }

            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
            CreateWallet wallet = new CreateWallet();

            App.Current.Properties["pubKey"] = wallet.CreateKeys(Network.Main)["pubKey"];
            App.Current.Properties["privKey"] = wallet.CreateKeys(Network.Main)["privKey"];

            UserInfo userInfo = new UserInfo("3JwMCMFL1edCTNxYmi52RszotYxRDm2MGn");
            App.Current.Properties["UserBalance"] = userInfo.GetUserBalance(MoneyUnit.BTC);

            // Только для тестирования. Должно быть удалено.
            if (User.HasTransactions)
            {
                UserInfo UserInfoWithTransactoins = new UserInfo("3JwMCMFL1edCTNxYmi52RszotYxRDm2MGn", true);

                App.Current.Properties["UserTransactionsTime"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsDateTime().ToArray());
                App.Current.Properties["UserTransactionsSum"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsAmount(MoneyUnit.BTC).ToArray());
                App.Current.Properties["UserTransactionType"] = String.Join(", ", UserInfoWithTransactoins.GetTypeOfTransaction().ToArray());

                App.Current.Properties["IsFilled"] = true;
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
