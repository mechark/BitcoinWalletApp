using System;
using System.Collections.Generic;
using Transactions;
using QRCodeKeys;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Platform.Android;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        protected User user { get; set; }

        public string[] pubKey { get; set; } = { (string)App.Current.Properties["pubKey"] };

        public MainPage()
        {
            InitializeComponent();
            
            user = new User();
            this.BindingContext = this;
            
        }
    }
}