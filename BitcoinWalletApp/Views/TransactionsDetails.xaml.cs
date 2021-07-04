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
using System.Xml.Serialization;
using BitcoinWalletApp.Repos;
using BitcoinWalletApp.Models;
using System.IO;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsDetails : ContentPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        public string CoinType { get => Settings.CoinType; }

        public TransactionsDetails()
        {
            InitializeComponent();
            
            TransactionsList.ItemsSource = User.TransactionsList;
            BindingContext = this;
        }

        private void TransactionsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var list = (ListView)sender;
            Navigation.PushPopupAsync(new TransactionDetails_Popup(e.ItemIndex));
            list.SelectedItem = null;
        }
    }
}