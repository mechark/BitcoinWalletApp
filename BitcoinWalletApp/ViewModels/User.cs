using System;
using System.Collections.Generic;
using NBitcoin;
using QRCodeKeys;
using Transactions;
using System.IO;
using Xamarin.Forms;

namespace BitcoinWalletApp.ViewModels
{
    public class User
    {


        // Properties

        protected UserInfo UserInfo { get => new UserInfo("3JwMCMFL1edCTNxYmi52RszotYxRDm2MGn"); }

        public string PubKey { get => (string)App.Current.Properties["pubKey"]; }

        public string PrivKey { get => (string)App.Current.Properties["privKey"]; }

        public bool HasTransactions { get => UserInfo.HasTransactions(); }

        public int TransactionsCount { get => (int)UserInfo.TransactionsCount; }

        public List<string> TransactionDateTime { get => UserInfo.GetUserRecentTransactionsDateTime(); }

        public string TransactionType { get => UserInfo.GetTypeOfTransaction(); }

        // Methods

        public ImageSource GetQRKey()
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(PubKey);

            return ImageSource.FromStream(() => new MemoryStream(keyQRCodeBytes));
        }

        public decimal AmountLastTransaction(MoneyUnit moneyUnit)
        {
            return UserInfo.GetUserRecentTransactionsAmount(moneyUnit);
        }  

        public decimal GetBalance(MoneyUnit moneyUnit)
        {
            return UserInfo.GetUserBalance(moneyUnit);
        }

    }
}
