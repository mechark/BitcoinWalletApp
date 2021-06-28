using BitcoinWalletApp.ViewModels;
using NBitcoin;
using System;
using System.Collections.Generic;
using Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BitcoinWalletApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage : ContentPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        private string amountToSend;

        public string SumToSend
        {
            get { return amountToSend; }
            set
            {
                amountToSend = value;

                OnPropertyChanged(nameof(SumToSend));
            }
        }

        protected double DisplayHeight { get => DeviceDisplay.MainDisplayInfo.Height; }

        public SendPage()
        {
            InitializeComponent();

            AddressPicker.ItemsSource = User.PublicKeys;
            SizeChanged += PageSizeChange;
            BindingContext = this;
        }

        void PageSizeChange(object sender, EventArgs e)
        {
            MainFrame.HeightRequest = DisplayHeight / 3.366906474820144;
        }

        private async void Button_SendCoins(object sender, EventArgs e)
        {
            await SendCoinsAsync();
        }

        private async Task<string> SendCoinsAsync()
        {
            return await Task.Run(() =>
            {
                string address = AddressPicker.Items[AddressPicker.SelectedIndex];
                string privateKey = User.Keys[address];
                string receiver = Receiver.Text;
                decimal sumToSend = Convert.ToDecimal(SumToSend.Replace(",", "")) / 1000000000;

                string transactionHash;
                Send userSender = new Send(privateKey, receiver);
                userSender.SendCoins(Network.Main, sumToSend, out transactionHash);

                ViewModels.Transaction transaction = new ViewModels.Transaction()
                {
                    UserAddress = address,
                    ReceiverAddress = receiver,
                    AmountOfTransaction = sumToSend.ToString(),
                    TransactionHash = transactionHash,
                    TransactionType = "Отправлено",
                    TransactionTime = DateTime.Now.ToString(),
                    TransactionTypeColor = Color.FromHex("#CC0033")
                };

                if (transaction.TransactionType == "Получено")
                {
                    transaction.TransactionTypeColor = Color.FromHex("#009900");
                }

                User.Transactions.Add(transaction);
                User.HasTransactions = true;

                return transactionHash;
            });
        }

        private void AmountToSend_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(SumToSend))
            {
                string sum = SumToSend.Replace(",", "");

                if (sum.Length == 4)
                {
                    sum = sum.Insert(1, ",");
                }
                else if (sum.Length == 5)
                {
                    sum = sum.Insert(2, ",");
                }
                else if (sum.Length == 6)
                {
                    sum = sum.Insert(3, ",");
                }
                else if (sum.Length == 7)
                {
                    sum = sum.Insert(1, ",");
                    sum = sum.Insert(5, ",");
                }
                else if (sum.Length == 8)
                {
                    sum = sum.Insert(2, ",");
                    sum = sum.Insert(6, ",");
                }
                else if (sum.Length == 9)
                {
                    sum = sum.Insert(3, ",");
                    sum = sum.Insert(7, ",");
                }

                AmountToSend.Text = sum;
            }
            
        }

        protected override void OnAppearing()
        {
            AddressPicker.ItemsSource = User.PublicKeys;
        }

        private void AmountToSend_Completed(object sender, EventArgs e)
        {
            AmountToSend.Text = AmountToSend.Text + " sat";
        }
    }
}