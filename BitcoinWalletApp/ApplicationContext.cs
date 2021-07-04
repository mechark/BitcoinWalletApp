using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BitcoinWalletApp.Models;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace BitcoinWalletApp
{
    public class ApplicationContext : DbContext
    {
        private string _databasePath;

        public Microsoft.EntityFrameworkCore.DbSet<UserModel> UserModels { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<TransactionModel> TransactionModels { get; set; }

        public ApplicationContext(string databasePath)
        {
            _databasePath = databasePath;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }


    }
}
