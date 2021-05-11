using System;
using System.Collections.Generic;
using Transactions;
using QRCodeKeys;
using NBitcoin;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;
using System.Windows;
using Xamarin.Forms.Platform.Android;
using Xamarin.Essentials;
using System.Windows.Input;
using Info.Blockchain.API.BlockExplorer;
using Info.Blockchain.API.Models;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public static User User { get => new User(); }
        public ICommand CopyAddressCommand => new Command(Copy_Clicked);

        public MainPage()
        {
            InitializeComponent();
            UserInitialize(MoneyUnit.Satoshi);

            this.BindingContext = this;
        }

        //Methods
        public void UserInitialize(MoneyUnit moneyUnit)
        {
            UserPubKey.Text = User.PubKey;
            UserBalance.Text = User.GetBalance(moneyUnit).ToString() + " " + moneyUnit.ToString();
            UserQRCodeKey.Source = User.GetQRKey();

            if (User.HasTransactions)
            {
                CreateRecentTransactionLabels(User.TransactionsCount, moneyUnit);
            }
        }

        private void CreateRecentTransactionLabels(int transactionsCount, MoneyUnit moneyUnit)
        {
            if (User.HasTransactions)
            {
                for (int transaction = 0; transaction < transactionsCount; transaction++)
                {

                    Label AmountOfTransaction = new Label
                    {
                        Text = User.AmountLastTransaction(moneyUnit).ToString() + " " + moneyUnit.ToString(),
                        TextColor = Color.Black,
                    };

                    Label TypeOfTransaction = new Label
                    {
                        Text = User.TransactionType,
                        TextColor = Color.Black,
                        HorizontalOptions = LayoutOptions.Center
                    };

                    Label DateTimeOfTransaction = new Label
                    {
                        Text = User.TransactionDateTime[transaction],
                        TextColor = Color.Black,
                        VerticalOptions = LayoutOptions.Center
                    };

                    TypeAndAmountOfTransaction.Children.Add(AmountOfTransaction);
                    TypeAndAmountOfTransaction.Children.Add(TypeOfTransaction);
                    DateOfTransaction.Children.Add(DateTimeOfTransaction);
                }
            }
            else
            {
                Label noTransactions = new Label
                {
                    Text = User.TransactionType,
                    TextColor = Color.Black
                };

                Grid.SetColumn(noTransactions, 1);
                TypeAndAmountOfTransaction.Children.Add(noTransactions);
            }
        } 

        private void Copy_Clicked()
        {
            if (!Clipboard.HasText)
            {
                Clipboard.SetTextAsync(User.PubKey);

                UserPubKey.Text = "Скопировано";
                Thread.Sleep(10000);
                UserPubKey.Text = User.PubKey;
            }

        }

    }
}