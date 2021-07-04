using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Transactions;
using Xamarin.Essentials;

namespace BitcoinWalletApp
{
    public static class Settings
    {
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

        public static string CoinType
        {
            get => Preferences.Get(nameof(CoinType), " BTC");
            set => Preferences.Set(nameof(CoinType), value);
        }

        public static double RubToBTC
        {
            get => Preferences.Get(nameof(CoinType), UserInfo.GetRubToBTC());
        }

        public static string DbPath
        {
            get => Preferences.Get(nameof(DbPath), (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Database\User.db3")));
        }
    }
}
