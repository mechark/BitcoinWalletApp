using System;
using System.Collections.Generic;
using System.Text;
using Transactions;

namespace BitcoinWalletApp.ViewModels
{
    public class Transaction
    {
        public string AmountOfTransaction { get; set; }
        public string TransactionType { get; set; }
        public string TransactionTime { get; set; }
    }
}
