﻿using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using NBitcoin;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using BitcoinWalletApp.Views.Popups;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAddresses : ContentPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        private bool _IsFirst = true;

        private bool IsFirst { get => _IsFirst; set => _IsFirst = value; }

        public ICommand AddAddress => new Command(AddPubKey);

        public MyAddresses()
        {
            InitializeComponent();
            AddressesList.ItemsSource = User.Addresses;

       /*     if (IsFirst)
            {
                AdressesInformationInitialize();
                IsFirst = false;
            } */
            
            BindingContext = this;
        }

        private async void AdressesInformationInitialize()
        { 
            await AddressesInformationInitializeAsync();
            AddingAddressIndincator.IsRunning = false;
            AddingAddressIndincator.IsVisible = false;
        }

        private async Task<string> AddressesInformationInitializeAsync()
        {
            return await Task.Run(() =>
            {
                foreach (KeyValuePair<string, string> keys in User.Keys)
                {
                    User.Addresses.Add(new Address()
                    {
                        PublicKey = keys.Key + "...",
                        PublicKeyBalance = User.GetBalance(MoneyUnit.BTC, keys.Key).ToString(),
                        PublicKeyQRCode = User.GetQRKey(keys.Key),
                    });
                }
                return "Key";
            });
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushPopupAsync(new AddressDetails_Popup());
        }

        private async void AddPubKey()
        {
            AddingAddressIndincator.IsRunning = true;
            AddingAddressIndincator.IsVisible = true;

            await AddPubKeyAsync();

            AddingAddressIndincator.IsRunning = false;
            AddingAddressIndincator.IsVisible = false;
        }

        private async Task<string> AddPubKeyAsync()
        {
            return await Task.Run(() =>
            {
                string address = User.Wallet.CreateKeys(Network.Main)["pubKey"];
                string privKey = User.Wallet.CreateKeys(Network.Main)["privKey"];

                User.Keys.Add(address, privKey);

                User.Addresses.Add(new Address()
                {
                    PublicKey = address,
                    PublicKeyBalance = User.GetBalance(MoneyUnit.BTC, address).ToString(),
                    PublicKeyQRCode = User.GetQRKey(address),
                });

                return address;
            });
        }
    }
}