using BitcoinWalletApp.Views;
using System;
using Xamarin.Forms;
using NBitcoin;
using Transactions;
using Xamarin.Forms.Xaml;
using BitcoinWalletApp.ViewModels;
using System.Data.Entity.Infrastructure;
using BitcoinWalletApp.Views.TabbedPages;
using System.Collections.Generic;
using System.Linq;
using Android.Preferences;
using Xamarin.Essentials;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;
using BitcoinWalletApp.Repos;
using BitcoinWalletApp.Models;

namespace BitcoinWalletApp
{
    public partial class App : Application
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        static ApplicationContext database;

        public static ApplicationContext Database
        {
            get
            {
                if (database == null)
                {
                    database = new ApplicationContext(Settings.DbPath);
                }

                return database;
            }
        }

        public static BaseRepo<UserModel> UserRepo = new BaseRepo<UserModel>(Settings.DbPath);
        public static BaseRepo<TransactionModel> TransactionRepo = new BaseRepo<TransactionModel>(Settings.DbPath);

        public App()
        {
            InitializeComponent();

            if (Settings.FirstRun)
            {
                UserInitialize();
                Settings.FirstRun = false;
            }
            else
            {
                Current.Properties.Add("UObject", new User());
                User.HasTransactions = true;
            }

            MainPage = new NavigationPage(new ParentPage());
        }

        private void UserInitialize()
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys = Wallet.CreateKeys(Network.Main);

            UserModel UModelObject = new UserModel()
            {
                Id = 1,
                Timestamp = DateTime.Now,
                PublicKey = keys["pubKey"],
                PrivateKey = keys["privKey"],
                Balance = UserInfo.GetUserBalance(MoneyUnit.BTC, keys["pubKey"]),
                TransactionsCount = 1,
            };

            TransactionModel transactionModel = new TransactionModel()
            {
                Id = 1,
                Timestamp = DateTime.Now,
                UserAddress = keys["pubKey"],
                ReceiverAddress = "38UmuUqPCrFmQo4khkomQwZ4VbY2nZMJ67",
                Amount = 0.01245822,
                TransactionHash = "d58aaa60033f9bc51d110b5e30d817c385ec3b1d1b2bc8274567247acec19ee4",
                TransactionTime = DateTime.Now.ToLocalTime().ToShortDateString(),
                TransactionType = "Отправлено",
                StringAmount = "0.01245822" + Settings.CoinType,
                TransactionTypeColor = "#CC0033"
            };

            //#009900 Получено
            UserRepo.Add(UModelObject);
            TransactionRepo.Add(transactionModel);
            UserRepo.SaveChanges();


            // Добавляю простые значения для значительного ускорения загрузки
            TemporarilyUserData.PublicKey = UModelObject.PublicKey;
            TemporarilyUserData.PrivateKey = UModelObject.PrivateKey;
            TemporarilyUserData.Balance = Convert.ToDouble(UModelObject.Balance);
            TemporarilyUserData.TransactionsCount = UModelObject.TransactionsCount;

            Current.Properties.Add("UObject", new User());
            User.HasTransactions = true;
        }

        protected override void OnSleep()
        {
            Current.SavePropertiesAsync().Wait();
        }

        protected override void OnResume()
        {
        }
    }
}
