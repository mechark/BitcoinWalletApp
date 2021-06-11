using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinWalletApp.Views.Popups;
using Rg.Plugins.Popup.Extensions;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetails : ContentPage
    {
        protected static User User { get => App.Current.Properties["UObject"] as User; }

        public TransactionDetails()
        {
            InitializeComponent();
            TransactionsList.ItemsSource = User.Transactions;

            BindingContext = this;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushPopupAsync(new TransactionDetails_Popup(e.ItemIndex));
        }
    }
}