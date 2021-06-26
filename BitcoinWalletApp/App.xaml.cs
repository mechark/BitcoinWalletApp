using BitcoinWalletApp.Views;
using System;
using Xamarin.Forms;
using NBitcoin;
using Transactions;
using Xamarin.Forms.Xaml;
using BitcoinWalletApp.ViewModels;
using BitcoinWalletApp.Views.TabbedPages;
using System.Collections.Generic;
using Android.Preferences;
using Xamarin.Essentials;
using System.Runtime.Serialization.Formatters.Binary;

namespace BitcoinWalletApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            VersionTracking.Track();
            OnStart();

            MainPage = new NavigationPage(new ParentPage());
        }

        protected override void OnStart()
        {
          //  if (VersionTracking.IsFirstLaunchForCurrentBuild)
         //   {

                CreateWallet wallet = new CreateWallet(Network.Main);

                string PubKey = wallet.CreateKeys(Network.Main)["pubKey"];
                string PrivateKey = wallet.CreateKeys(Network.Main)["privKey"];

                Dictionary<string, string> keys = new Dictionary<string, string>();
                keys.Add(PubKey, PrivateKey);

                UserInfo UserInfoWithTransactoins = new UserInfo(PubKey);

                decimal userTotalbalance = 0;
                string userMainPubKey = "";
                string userMainPrivKey = "";
                int userTransactionsCount = 0;

                foreach (KeyValuePair<string, string> address in keys)
                {
                    userMainPubKey = address.Key;
                    userMainPrivKey = address.Value;
                    userTransactionsCount = Convert.ToInt32(UserInfoWithTransactoins.TransactionsCount);
                }


                User UObject = new User()
                {
                    Wallet = wallet,
                    Keys = keys,
                    MainPubKey = userMainPubKey,
                    MainPrivKey = userMainPrivKey,
                    Balance = userTotalbalance,
                    TransactionsCount = userTransactionsCount,
                    UserInfo = new UserInfo(userMainPubKey),
                    CoinType = " BTC"
                    //Тестовый режим. Удалить позднее
                    //        AmountOfTransactions = userTransactionsAmount,
                    //      TransactionDateTime = userTransactionsDateTime,
                    //    TransactionsType = userTrasnactionsType
                    //
                };

                UObject.Addresses.Add(new Address()
                {
                    PublicKey = userMainPubKey,
                    PublicKeyBalance = userTotalbalance.ToString() + UObject.CoinType,
                    DecimalBalance = userTotalbalance,
                    PublicKeyQRCode = UObject.GetQRKey(userMainPubKey),
                });

                UObject.Transactions.Add(new ViewModels.Transaction
                {
                    AmountOfTransaction = "0.00257474" + UObject.CoinType,
                    DecimalAmountOfTransaction = 0.00257474m,
                    TransactionTime = "22.03.2020 13:39",
                    TransactionType = "Получено",
                    TransactionHash = "d58aaa60033f9bc51d110b5e30d817c385ec3b1d1b2bc8274567247acec19ee4",
                    UserAddress = "3JwMCMFL1edCTNxYmi52RszotYxRDm2MGn",
                    ReceiverAddress = "1Gh6DKC8CDP0Ym1U78vdW0l79x",
                    TransactionTypeColor = Color.FromHex("#009900")
                });

                UObject.Transactions.Add(new ViewModels.Transaction
                {
                    AmountOfTransaction = "0.00257474" + UObject.CoinType,
                    DecimalAmountOfTransaction = 0.00257474m,
                    TransactionTime = "22.03.2020 13:39",
                    TransactionType = "Получено",
                    TransactionHash = "d58aaa60033f9bc51d110b5e30d817c385ec3b1d1b2bc8274567247acec19ee4",
                    UserAddress = "3JwMCMFL1edCTNxYmi52RszotYxRDm2MGn",
                    ReceiverAddress = "1Gh6DKC8CDP0Ym1U78vdW0l79x",
                    TransactionTypeColor = Color.FromHex("#009900")
                });

                UObject.Transactions.Add(new ViewModels.Transaction
                {
                    AmountOfTransaction = "0.00257474" + UObject.CoinType,
                    DecimalAmountOfTransaction = 0.00257474m,
                    TransactionTime = "22.03.2020 13:39",
                    TransactionType = "Получено",
                    TransactionHash = "d58aaa60033f9bc51d110b5e30d817c385ec3b1d1b2bc8274567247acec19ee4",
                    UserAddress = "3JwMCMFL1edCTNxYmi52RszotYxRDm2MGn",
                    ReceiverAddress = "1Gh6DKC8CDP0Ym1U78vdW0l79x",
                    TransactionTypeColor = Color.FromHex("#009900")
                });

                UObject.PublicKeys.Add(userMainPubKey);
                UObject.HasTransactions = true;

                App.Current.Properties["UObject"] = UObject;


            //    }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
