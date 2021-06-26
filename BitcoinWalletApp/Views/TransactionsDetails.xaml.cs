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
    public partial class TransactionsDetails : ContentPage
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
    }
}