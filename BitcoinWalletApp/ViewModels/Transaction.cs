using System;
using System.Collections.Generic;
using System.Text;
using Transactions;
using Xamarin.Forms;

namespace BitcoinWalletApp.ViewModels
{
    public class Transaction
    {
        public string AmountOfTransaction { get; set; }
        public decimal DecimalAmountOfTransaction { get; set; }
        public string TransactionType { get; set; }
        public string TransactionTime { get; set; }
        public string UserAddress { get; set; }
        public string ReceiverAddress { get; set; }
        public string TransactionHash { get; set; }
    }
}
