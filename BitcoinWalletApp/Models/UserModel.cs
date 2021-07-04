using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BitcoinWalletApp.Models
{
    [Table("UserModel")]
    public partial class UserModel : EntityBase
    {
        [StringLength(35)]
        [Required]
        public string PublicKey { get; set; }

        [StringLength(50)]
        [Required]
        public string PrivateKey { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public int TransactionsCount { get; set; }
    }
}
