using System;
using System.Collections.Generic;
using NBitcoin;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAddresses : ContentPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        public List<Address> Addresses { get; protected set; } = new List<Address>();

        public MyAddresses()
        {
            InitializeComponent();
            AdressesInformationInitialize();
            
            BindingContext = this;
        }

        protected void AdressesInformationInitialize()
        {
            foreach (KeyValuePair<string, string> keys in User.Keys)
            {
                Addresses.Add(new Address()
                {
                    PublicKey = keys.Key,
                    PublicKeyBalance = User.GetBalance(MoneyUnit.BTC).ToString(),
                    PublicKeyQRCode = User.GetQRKey(keys.Key),
                    PublicKeyNumberOfTransactions = User.TransactionsCount.ToString()
                });
            }
        }
    }
}