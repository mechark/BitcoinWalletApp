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
        protected User User { get => App.Current.Properties["UObject"] as User; }

        public List<Transaction> Transaction { get; set; } = new List<Transaction>();

        public TransactionDetails()
        {
            InitializeComponent();

            TransactoinInitialize();

            BindingContext = this;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushPopupAsync(new TransactionDetails_Popup());
        }

        private void TransactoinInitialize()
        {
            Transaction = new List<Transaction>();
            for (int transactionsNumber = 0; transactionsNumber < User.TransactionsCount; transactionsNumber++)
            {
                Transaction.Add(new Transaction()
                {
                    AmountOfTransaction = User.AmountOfTransactions[transactionsNumber].ToString(),
                    TransactionType = User.TransactionsType[transactionsNumber],
                    TransactionTime = User.TransactionDateTime[transactionsNumber]
                });
            }
        }
    }
}