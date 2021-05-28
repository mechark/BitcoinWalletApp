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
using System.Net.NetworkInformation;
using System.IO;
using System.Globalization;
using Rg.Plugins.Popup.Extensions;
using Drawing = System.Drawing;

namespace BitcoinWalletApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        protected static User User { get => new User(); }

        protected double DisplayWidth { get => DeviceDisplay.MainDisplayInfo.Width; }

        protected double DisplayHeight { get => DeviceDisplay.MainDisplayInfo.Height; }

        protected Label AmountOfTransaction { get; set; }

        public ICommand CopyAddressCommand => new Command(Copy_Clicked);

        public ICommand AllTransactionsShowCommand => new Command(AllTransactionsShow);

        public ICommand DownloadQRCode => new Command(SaveQRImage);

        public MainPage()
        {
            InitializeComponent();
            UserInitialize(MoneyUnit.BTC);
            SizeChanged += PageSizeChange;

            this.BindingContext = this;
        }

        void PageSizeChange (object sender, EventArgs e)
        {
            UserQRCodeKey.WidthRequest = DisplayWidth / 2.35;
            MainFrame.HeightRequest = DisplayHeight / 1;
            UserPubKey.FontSize = DisplayHeight / 146.25;
            MyAddresses.Padding = DisplayHeight / 156;
        }

        //Methods
        public void UserInitialize(MoneyUnit moneyUnit)
        {
            UserPubKey.Text = User.PubKey;
            UserQRCodeKey.Source = User.GetQRKey();
            UserBalance.Text = User.Balance.ToString() + " " + moneyUnit.ToString();

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

                    FlexLayout layout = new FlexLayout
                    {
                        Direction = FlexDirection.Row,
                        JustifyContent = FlexJustify.SpaceBetween
                    };

                    AmountOfTransaction = new Label
                    {
                        Text = User.AmountOfTransactions[transaction] + " " + moneyUnit.ToString(),
                        TextColor = Color.White,
                        FontSize = DisplayHeight / 167.1428571428571,
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    Label TypeOfTransaction = new Label
                    {
                        Text = User.TransactionsType[transaction],
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center,
                        FontSize = DisplayHeight / 167.1428571428571
                    };

                    if (User.TransactionsType[transaction] == "Получено")
                    {
                        TypeOfTransaction.TextColor = Color.FromHex("#00CC00");
                    }
                    else if (User.TransactionsType[transaction] == "Отправлено")
                    {
                        TypeOfTransaction.TextColor = Color.FromHex("#CC0000");
                    }

                    Label DateTimeOfTransaction = new Label
                    {
                        Text = User.TransactionDateTime[transaction],
                        TextColor = Color.White,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = DisplayHeight / 167.1428571428571
                    };

                    TypeAndAmountOfTransaction.Children.Add(layout);

                    layout.Children.Add(AmountOfTransaction);
                    layout.Children.Add(TypeOfTransaction);
                    layout.Children.Add(DateTimeOfTransaction);
                }
            }
            else
            {
                Label noTransactions = new Label
                {
                    Text = User.TransactionsType[0],
                    TextColor = Color.Black
                };

                Grid.SetColumn(noTransactions, 1);
                TypeAndAmountOfTransaction.Children.Add(noTransactions);
            }
        }


        private void Refresh_Clicked(object sender, EventArgs e)
        {
            // Понять работает ли обновление страницы только при обновлении данных или нет
            UserInfo UserInfo = new UserInfo(User.PubKey);
            App.Current.Properties["UserBalance"] = UserInfo.GetUserBalance(MoneyUnit.BTC);

            if (User.HasTransactions)
            {
                UserInfo UserInfoWithTransactoins = new UserInfo(User.PubKey, true);

                App.Current.Properties["UserTransactionsTime"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsDateTime().ToArray());
                App.Current.Properties["UserTransactionsSum"] = String.Join(", ", UserInfoWithTransactoins.GetUserTransactionsAmount(MoneyUnit.BTC).ToArray());
                App.Current.Properties["UserTransactionType"] = String.Join(", ", UserInfoWithTransactoins.GetTypeOfTransaction().ToArray());

                App.Current.Properties["IsFilled"] = true;
            }
        }

        private async void ChangeCoin_Clicked(object sender, EventArgs e)
        {
            var change = await DisplayActionSheet(null, null, null, "BTC", "Sat", "mBTC");
            ChangeCoin.Text = change;

            if (change == "BTC")
            {
                UserBalance.Text = User.Balance.ToString() + " " + change;
            }
            else if (change == "Sat")
            {
                UserBalance.Text = (User.Balance * 100000).ToString() + " " + change;
            }
            else if (change == "mBTC")
            {
                UserBalance.Text = (User.Balance * 1000).ToString() + " " + change;
            }
            else
            {
                ChangeCoin.Text = "BTC";
                UserBalance.Text = User.Balance.ToString() + "BTC";
            }



        }

        private void SaveQRImage()
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(User.PubKey);

            DependencyService.Get<IMediaSave>().SavePicture(keyQRCodeBytes, "PublicKey" + User.PubKey);
            Navigation.PushPopupAsync(new DownloadQRImagePopup());
        }

        private void MyAddresses_Clicked (object sender, EventArgs e)
        {
            
        }

        private void Copy_Clicked()
        {
            if (Clipboard.GetTextAsync().ToString() != User.PubKey)
            {
                Clipboard.SetTextAsync(User.PubKey);
                Navigation.PushPopupAsync(new CopyPopup());
                Navigation.PopPopupAsync(true);
            }
        }

        private void AllTransactionsShow()
        {
            Navigation.PushAsync(new TransactionsInfo());
        }


    }
}