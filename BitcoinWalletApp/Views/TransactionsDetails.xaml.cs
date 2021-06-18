using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using BitcoinWalletApp.Views.Popups;
using Xamarin.Forms.Xaml;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsDetails : ContentPage, ICoinTypeChange
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        public TransactionsDetails()
        {
            InitializeComponent();

            
            TransactionsList.ItemsSource = User.Transactions;
            BindingContext = this;
        }

        private void TransactionsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushPopupAsync(new TransactionDetails_Popup(e.ItemIndex));
        }

        public async void ChangeCoinType()
        {
            await ChangeCoinTypeAsync();
        }

        public async Task<string> ChangeCoinTypeAsync()
        {
            return await Task.Run(() =>
            {
                foreach (Transaction transaction in User.Transactions)
                {
                    if (User.CoinType == "Sat")
                    {
                        transaction.AmountOfTransaction = (transaction.DecimalAmountOfTransaction * 100000).ToString() + " sat";
                    }
                    else if (User.CoinType == "mBTC")
                    {
                        transaction.AmountOfTransaction = (transaction.DecimalAmountOfTransaction * 1000).ToString() + " mBTC";
                    }
                    else if (User.CoinType == "BTC")
                    {
                        transaction.AmountOfTransaction = transaction.AmountOfTransaction + " BTC";
                    }
                }

                return "";
            });
        }
    }
}