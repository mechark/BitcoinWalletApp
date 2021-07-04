using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BitcoinWalletApp
{
    public class EntityBase
    {
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        [Timestamp]
        public DateTime Timestamp { get; set; }
    }
}
