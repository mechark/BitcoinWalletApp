using System;
using System.Collections.Generic;
using Transactions;
using QRCodeKeys;
using NBitcoin;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BitcoinWalletApp.Views.Popups;
using System.Threading;
using System.Windows;
using Xamarin.Forms.Platform.Android;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Net.NetworkInformation;
using System.IO;
using System.Globalization;
using Rg.Plugins.Popup.Extensions;
using Drawing = System.Drawing;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Android.Content.Res;
using Android.Webkit;
using Plugin.Permissions;
using System.Xml.Serialization;
using BitcoinWalletApp.Models;

namespace BitcoinWalletApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, ICoinTypeChange
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

           private ObservableCollection<TransactionModel> RecentTransactions
           {
               get
               {
                   ObservableCollection<TransactionModel> recentTransactions = new ObservableCollection<TransactionModel>();

                   if (User.TransactionsCount > 3)
                   {
                       for (int transaction = 0; transaction < 4; transaction++)
                       {
                           recentTransactions.Add(User.TransactionsList[transaction]);
                       }
                   }
                   else
                   {
                       for (int transaction = 0; transaction < User.TransactionsCount; transaction++)
                       {
                           recentTransactions.Add(User.TransactionsList[transaction]);
                       }
                   }

                   return recentTransactions;
               }
           }
        
        public Color TransactionTypeColor { get; set; } = Color.FromHex("#CC0033");

     /*   public ObservableCollection<TransactionModel> TransactionModels
        {
            get
            {
                ObservableCollection<TransactionModel> transactionModels = new ObservableCollection<TransactionModel>();

                foreach (TransactionModel transactionModel in User.GetAllTransaction())
                {
                    TransactionTypeColor = Color.FromHex("#CC0033");

                    if (transactionModel.TransactionType == "Получено")
                    {
                        TransactionTypeColor = Color.FromHex("#009900");
                    }
                }
            }
        }
     */
        private double DisplayHeight { get => DeviceDisplay.MainDisplayInfo.Height; }

        public string CoinType { get => Settings.CoinType; }

        public ICommand CopyAddressCommand => new Command(Copy_Clicked);

        public ICommand AllTransactionsShowCommand => new Command(AllTransactionsShow);

        public ICommand ChangeType => new Command(ChangeCoinType);

        public ICommand DownloadQRCode => new Command(SaveQRImage);

        public MainPage()
        {
            InitializeComponent();
            SizeChanged += PageSizeChange;
            UserInitialize(MoneyUnit.BTC);

          //  UserRecentTransaction.ItemsSource = RecentTransactions;
            StoragePermission();
            BindingContext = this;
        }

        void PageSizeChange (object sender, EventArgs e)
        {
            MainFrame.HeightRequest = DisplayHeight / 3.366906474820144;
            UserPubKey.FontSize = DisplayHeight / 146.25;

            if (!User.HasTransactions)
            {
                UserRecentTransactionWrapper.HeightRequest = 50;
                UserRecentTransaction.IsVisible = false;
            }
            else
            {
                UserRecentTransaction.ItemsSource = RecentTransactions;
            }

            UserInitialize(MoneyUnit.BTC);
        }

        private async Task<decimal> CheckBalance(MoneyUnit moneyUnit)
        {
            return await Task.Run(() =>
            {
                decimal balance = User.GetBalance(MoneyUnit.BTC, User.PublicKey);
                UserBalance.Text = balance.ToString() + " " + moneyUnit.ToString();

                return balance;
            });
        }

        private async void StoragePermission()
        {
            Plugin.Permissions.Abstractions.PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
        }

        //Methods
        private void UserInitialize(MoneyUnit moneyUnit)
        {
            UserPubKey.Text = User.PublicKey;
            UserQRCodeKey.Source = User.GetQRKey(User.PublicKey);
            UserBalance.Text = User.Balance.ToString() + " " + moneyUnit.ToString();
           // UserRecentTransaction.ItemsSource = RecentTransactions;
        }

        public async void ChangeCoinType()
        {
            var change = await DisplayActionSheet(null, null, null, "BTC", "Sat", "mBTC");
            int balance = Convert.ToInt32(User.Balance);

            if (change != null && change != ChangeCoin.Text)
            {
                ChangeCoin.Text = change;
                User.CoinType = change;

                await User.ChangeCoinTypeAsync();

                if (change == "Sat")
                {
                    UserBalance.Text = (balance * 100000).ToString() + " sat";
                }
                else if (change == "mBTC")
                {
                    UserBalance.Text = (balance * 1000).ToString() + " mBTC";
                }
                else if (change == "BTC")
                {
                    UserBalance.Text = "";
                    UserBalance.Text = balance.ToString() + " BTC";
                }

                UserRecentTransaction.ItemsSource = null;
                UserRecentTransaction.ItemsSource = RecentTransactions;

            }
        }

        private void SaveQRImage() => User.SaveImage(User.PublicKey);

        private void Copy_Clicked() => User.CopySomething(User.PublicKey);

        private void AllTransactionsShow() => Navigation.PushAsync(new TransactionsDetails(), true);

        private void UserRecentTransaction_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var list = (ListView)sender;
            Navigation.PushPopupAsync(new TransactionDetails_Popup(e.ItemIndex));
            list.SelectedItem = null;
        }

    }
}