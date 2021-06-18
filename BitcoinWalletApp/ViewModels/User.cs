﻿using System;
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

        public string CoinType { get; set; } = "BTC";

        //Transactions

        public bool HasTransactions { get; set; } = false;

        public int TransactionsCount { get; set; }

        public List<decimal> AmountOfTransactions { get; set; }

        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();

        public List<string> TransactionsType { get; set; }

        public List<string> TransactionDateTime { get; set; }

        public List<string> TransactionsHash { get; set; }

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
