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

namespace BitcoinWalletApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, ICoinTypeChange
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        private ObservableCollection<ViewModels.Transaction> RecentTransactions
        {
            get
            {
                ObservableCollection<ViewModels.Transaction> recentTransactions = new ObservableCollection<ViewModels.Transaction>();
                
                if (User.Transactions.Count > 3)
                {
                    for (int transaction = 0; transaction < 4; transaction++)
                    {
                        recentTransactions.Add(User.Transactions[transaction]);
                    }
                }
                else
                {
                    for (int transaction = 0; transaction < User.Transactions.Count; transaction++)
                    {
                        recentTransactions.Add(User.Transactions[transaction]);
                    }
                }
                
                return recentTransactions;
            }
        }

        private double DisplayHeight { get => DeviceDisplay.MainDisplayInfo.Height; }

        public ICommand CopyAddressCommand => new Command(Copy_Clicked);

        public ICommand AllTransactionsShowCommand => new Command(AllTransactionsShow);

        public ICommand ChangeType => new Command(ChangeCoinType);

        public ICommand DownloadQRCode => new Command(SaveQRImage);

        public MainPage()
        {
            InitializeComponent();
            SizeChanged += PageSizeChange;
            UserInitialize(MoneyUnit.BTC);

            UserRecentTransaction.ItemsSource = RecentTransactions;
            BindingContext = this;
        }

        void PageSizeChange (object sender, EventArgs e)
        {
            MainFrame.HeightRequest = DisplayHeight / 3.366906474820144;
            UserRecentTransaction.HeightRequest = DisplayHeight / 16.83453237410072;
            UserPubKey.FontSize = DisplayHeight / 146.25;
            MyAddresses.Padding = DisplayHeight / 156;
            UserInitialize(MoneyUnit.BTC);

        }

        //Methods
        public async void UserInitialize(MoneyUnit moneyUnit)
        {
            UserPubKey.Text = User.MainPubKey;
            UserQRCodeKey.Source = User.GetQRKey(User.MainPubKey);
            UserBalance.Text = User.Balance.ToString() + " " + moneyUnit.ToString();
            UserRecentTransaction.ItemsSource = RecentTransactions;
            Plugin.Permissions.Abstractions.PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            // Понять работает ли обновление страницы только при обновлении данных или нет
            UserInfo UserInfo = new UserInfo(User.MainPubKey);
            App.Current.Properties["UserBalance"] = UserInfo.GetUserBalance(MoneyUnit.BTC, User.MainPubKey);

            if (User.HasTransactions)
            {
                UserInfo UserInfoWithTransactoins = new UserInfo(User.MainPubKey, true);

                App.Current.Properties["UserTransactionsTime"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsDateTime().ToArray());
                App.Current.Properties["UserTransactionsSum"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsAmount(MoneyUnit.BTC).ToArray());
                App.Current.Properties["UserTransactionType"] = String.Join(", ", UserInfoWithTransactoins.GetTypeOfTransaction().ToArray());

                App.Current.Properties["IsFilled"] = true;
            }
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

        private void SaveQRImage() => User.SaveImage(User.MainPubKey);

        private void MyAddresses_Clicked (object sender, EventArgs e) => Navigation.PushAsync(new MyAddresses(), true);

        private void Copy_Clicked() => User.CopySomething(User.MainPubKey);

        private void AllTransactionsShow() => Navigation.PushAsync(new TransactionsDetails(), true);

        private void UserRecentTransaction_ItemTapped(object sender, ItemTappedEventArgs e) => Navigation.PushPopupAsync(new TransactionDetails_Popup(e.ItemIndex));
    }
}