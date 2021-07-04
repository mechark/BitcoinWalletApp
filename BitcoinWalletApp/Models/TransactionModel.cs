using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace BitcoinWalletApp.Models
{
    [Table("TransactionModel")]
    public partial class  TransactionModel : EntityBase
    {
        public double Amount { get; set; }

        public string TransactionType { get; set; }

        public string TransactionTime { get; set; }

        [MaxLength(35)]
        public string UserAddress { get; set; }

        [MaxLength(35)]
        public string ReceiverAddress { get; set; }

        public string TransactionHash { get; set; }

        public string StringAmount { get; set; }

        public string TransactionTypeColor { get; set; }
    }
}
