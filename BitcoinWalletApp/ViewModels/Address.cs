using System;
using System.Collections.Generic;
using System.Text;
using QRCodeKeys;
using Xamarin.Forms;

namespace BitcoinWalletApp.ViewModels
{
    public class Address
    {
        protected User User { get => App.Current.Properties["UObject"] as User; }

        public string PublicKey { get; set; }
        public string PublicKeyBalance { get; set; }
        public ImageSource PublicKeyQRCode { get; set; }
        public string PublicKeyNumberOfTransactions { get; set; }

    }
}
