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

namespace BitcoinWalletApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage : ContentPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        protected double DisplayWidth { get => DeviceDisplay.MainDisplayInfo.Width; }

        protected double DisplayHeight { get => DeviceDisplay.MainDisplayInfo.Height; }

        public SendPage()
        {
            InitializeComponent();
            AdressesInformationInitialize();
            SizeChanged += PageSizeChange;
            BindingContext = this;
        }

        void PageSizeChange(object sender, EventArgs e)
        {
            MainFrame.HeightRequest = DisplayHeight / 1;
        }

        protected void AdressesInformationInitialize()
        {
            foreach (KeyValuePair<string, string> keys in User.Keys)
            {
                User.Addresses.Add(new Address()
                {
                    PublicKey = keys.Key + "...",
                    PublicKeyBalance = User.GetBalance(MoneyUnit.BTC, keys.Key).ToString(),
                    PublicKeyQRCode = User.GetQRKey(keys.Key),
                    PublicKeyNumberOfTransactions = User.TransactionsCount.ToString()
                });
            }
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
                decimal sumToSend = Convert.ToDecimal(AmountToSend.Text);

                string transactionHash;
                Send userSender = new Send(privateKey, receiver);
                userSender.SendCoins(Network.TestNet, sumToSend, out transactionHash);

                ViewModels.Transaction transaction = new ViewModels.Transaction()
                {
                    UserAddress = address,
                    ReceiverAddress = receiver,
                    AmountOfTransaction = sumToSend.ToString(),
                    TransactionHash = transactionHash,
                    TransactionType = "Отправлено",
                    TransactionTime = DateTime.Now.ToString()
                };

                User.Transactions.Add(transaction);
                User.HasTransactions = true;

                return transactionHash;
            });
        }

    }
}