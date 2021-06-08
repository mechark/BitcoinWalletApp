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
            if (VersionTracking.IsFirstLaunchForCurrentBuild)
            {

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
                //Тестовый режим. Удалить позднее
                //     List<decimal> userTransactionsAmount = UserInfoWithTransactoins.GetUserTransactionsAmount(MoneyUnit.BTC);
                //       List<string> userTransactionsDateTime = UserInfoWithTransactoins.GetUserTransactionsDateTime();
                //       List<string> userTrasnactionsType = UserInfoWithTransactoins.GetTypeOfTransaction();
                //

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
                    //Тестовый режим. Удалить позднее
                    //        AmountOfTransactions = userTransactionsAmount,
                    //      TransactionDateTime = userTransactionsDateTime,
                    //    TransactionsType = userTrasnactionsType
                    //
                };

                

                foreach (KeyValuePair<string, string> Keys in UObject.Keys)
                {
                    UObject.Addresses.Add(new Address()
                    {
                        PublicKey = Keys.Key + "...",
                        PublicKeyBalance = UObject.GetBalance(MoneyUnit.BTC, Keys.Key).ToString(),
                        PublicKeyQRCode = UObject.GetQRKey(Keys.Key),
                    });
                }

                App.Current.Properties["UObject"] = UObject;
                //   }

                //    }
                // Только для тестирования. Должно быть удалено.
                //   if (User.HasTransactions)

                //     App.Current.Properties["UserTransactionsTime"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsDateTime().ToArray());
                //   App.Current.Properties["UserTransactionsSum"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsAmount(MoneyUnit.BTC).ToArray());
                // App.Current.Properties["UserTransactionType"] = String.Join(", ", UserInfoWithTransactoins.GetTypeOfTransaction().ToArray());

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
