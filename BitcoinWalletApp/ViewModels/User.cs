using System;
using System.Collections.Generic;
using NBitcoin;
using QRCodeKeys;
using Transactions;
using System.IO;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BitcoinWalletApp.ViewModels
{
    [Serializable]
    public class User
    {
        public User () { }

        // Properties

        public UserInfo UserInfo { get; set; }

        public CreateWallet Wallet { get; set; }

        public string MainPubKey { get; set; }

        public string MainPrivKey { get; set; }

        public Dictionary<string, string> Keys { get; set; }

        public ObservableCollection<Address> Addresses { get; set; } = new ObservableCollection<Address>();

        public decimal Balance { get; set; }

        //Transactions

        public bool HasTransactions { get => UserInfo.HasTransactions(); }

        public int TransactionsCount { get; set; }

        public List<decimal> AmountOfTransactions { get; set; }

        public List<string> TransactionsType { get; set; }

        public List<string> TransactionDateTime { get; set; }

        // Methods

        public ImageSource GetQRKey(string UserPubKey)
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(UserPubKey);

            return ImageSource.FromStream(() => new MemoryStream(keyQRCodeBytes));
        }  

        public decimal GetBalance (MoneyUnit moneyUnit, string userPubKey)
        {
            return UserInfo.GetUserBalance(moneyUnit, userPubKey);
        }
    }
}
