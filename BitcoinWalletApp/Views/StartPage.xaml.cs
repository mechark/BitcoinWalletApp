using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Transactions;
using NBitcoin;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        protected MainPage page { get; set; }

        public StartPage()
        {
            InitializeComponent();
            page = new MainPage();
        }

        private void CreateWallet_Clicked(object sender, EventArgs e)
        { 

            Navigation.PushAsync(page);
        }
    }
}