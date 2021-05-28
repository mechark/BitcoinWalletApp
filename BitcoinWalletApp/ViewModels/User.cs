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
        public User ()
        {
            
        }

        // Properties

        protected UserInfo UserInfo { get => new UserInfo(PubKey); }

        public string PubKey { get => (string)App.Current.Properties["pubKey"]; }

        public string PrivKey { get => (string)App.Current.Properties["privKey"]; }

        public bool HasTransactions { get => UserInfo.HasTransactions(); }

        public int TransactionsCount { get => (int)UserInfo.TransactionsCount; }

        public int Balance { get => Convert.ToInt32(App.Current.Properties["UserBalance"]); }

        // Transactions *******************

        public string [] AmountOfTransactions { get => App.Current.Properties["UserTransactionsSum"].ToString().Split(); }

        public string [] TransactionsType { get => App.Current.Properties["UserTransactionType"].ToString().Split(); }

        public string [] TransactionDateTime { get => App.Current.Properties["UserTransactionsTime"].ToString().Split(); }

        // Transactions *******************


        // Methods

        public ImageSource GetQRKey()
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(PubKey);

            return ImageSource.FromStream(() => new MemoryStream(keyQRCodeBytes));
        }  
    }
}
