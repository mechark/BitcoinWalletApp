using System;
using System.Collections.Generic;
using System.Text;
using BitcoinWalletApp.Models;
using BitcoinWalletApp.ViewModels;
using Xamarin.Essentials;

namespace BitcoinWalletApp
{
    public static class TemporarilyUserData
    {
        public static string PublicKey
        {
            get => Preferences.Get(nameof(PublicKey), string.Empty);
            set => Preferences.Set(nameof(PublicKey), value);
        }

        public static string PrivateKey
        {
            get => Preferences.Get(nameof(PrivateKey), string.Empty);
            set => Preferences.Set(nameof(PrivateKey), value);
        }

        public static double Balance
        {
            get => Preferences.Get(nameof(Balance), 0.0);
            set => Preferences.Set(nameof(Balance), value);
        }

        public static int TransactionsCount
        {
            get => Preferences.Get(nameof(TransactionsCount), 0);
            set => Preferences.Set(nameof(TransactionsCount), value);
        }
    }
}
