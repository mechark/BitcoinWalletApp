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
using Xamarin.Forms.Internals;
using BitcoinWalletApp.Views.Popups;
using Rg.Plugins.Popup.Extensions;
using System.Xml.Serialization;
using System.IO;
using BitcoinWalletApp.Models;
using BitcoinWalletApp.Repos;

namespace BitcoinWalletApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage : ContentPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        public double HalfDisplayWidth { get => DeviceDisplay.MainDisplayInfo.Width / 3; }
        public double FourDisplayWidth { get => DeviceDisplay.MainDisplayInfo.Width / 4; }

        private BaseRepo<TransactionModel> Repo = new BaseRepo<TransactionModel>(Settings.DbPath);

        protected double DisplayHeight { get => DeviceDisplay.MainDisplayInfo.Height; }

        public SendPage()
        {
            InitializeComponent();

          //  AddressPicker.ItemsSource = User.PublicKeys;
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
                bool isDataCorrect = false;
                bool isTransactionSuccess = false;
                string transactionHash = null;
                string address = null;
                string privateKey = null;
                string receiver = Receiver.Text as string;
                decimal sumToSend = 0;
                string sum = AmountToSend.Text.Replace(" ", "");
                sum = AmountToSend.Text.Replace("sat|", "");

                if (sum.Length < 5)
                {
                    IncorrectnessAnimation(AmountToSend);
                }
                else if (receiver == null)
                {
                    IncorrectnessAnimation(Receiver);
                }
                else
                {
                    isDataCorrect = true;
                    sumToSend = Convert.ToDecimal(AmountToSend.Text.Replace(",", "")) / 1000000000;
                }

                if (isDataCorrect == true)
                {
                    Send userSender = new Send(privateKey, receiver);
                    userSender.SendCoins(Network.Main, sumToSend, out transactionHash);

                    TransactionModel transaction = new TransactionModel()
                    {
                        UserAddress = address,
                        ReceiverAddress = receiver,
                        Amount = Convert.ToDouble(sumToSend),
                        TransactionHash = transactionHash,
                        TransactionType = "Отправлено",
                        TransactionTime = DateTime.Now.ToLocalTime().ToShortDateString(),
                    };

                    Repo.Add(transaction);
                    User.HasTransactions = true;
                    isTransactionSuccess = true;
                }

                if (isTransactionSuccess)
                {
                    Navigation.PushPopupAsync(new SuccessfullTransaction_AddressesBook_Popup(isTransactionSuccess), true);
                }
                else
                {
                    Navigation.PushPopupAsync(new FailureTransaction_Popup(), true);
                }

                return transactionHash;
            });
        }

        private void IncorrectnessAnimation(View element)
        {
            bool _isAnimating = true;

            new Animation {
                { 0, 0.125, new Animation (v => element.TranslationX = v, 0, -13) },
                { 0.125, 0.250, new Animation (v => element.TranslationX = v, -13, 13) },
                { 0.250, 0.375, new Animation (v => element.TranslationX = v, 13, -11) },
                { 0.375, 0.5, new Animation (v => element.TranslationX = v, -11, 11) },
                { 0.5, 0.625, new Animation (v => element.TranslationX = v, 11, -7) },
                { 0.625, 0.75, new Animation (v => element.TranslationX = v, -7, 7) },
                { 0.75, 0.875, new Animation (v => element.TranslationX = v, 7, -5) },
                { 0.875, 1, new Animation (v => element.TranslationX = v, -5, 0) }
            }
                .Commit(this, "AppleShakeChildAnimations", length: 500, easing: Easing.Linear, finished: (x, y) => _isAnimating = false);
        }

        private void AmountToSend_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(AmountToSend.Text))
            {
                string sum = AmountToSend.Text.Replace(" ", "");
                sum = AmountToSend.Text.Replace("sat|", "");

                string res;

                if (sum.Length > 3)
                {
                    ConvertedToCurrency.Text = "₽| " + Math.Round(Convert.ToDouble(sum) / 100000000 * User.RubleToBTC, 1).ToString();
                }
                else if (AmountToSend.Text.Length < 5)
                {
                    AmountToSend.Text = AmountToSend.Text + " ";
                }
                else if (ConvertedToCurrency.Text.Length < 4)
                {
                    ConvertedToCurrency.Text = ConvertedToCurrency.Text + " ";
                }
                else
                {
                    ConvertedToCurrency.Text = "₽| ";
                }

                User.CommaInsert(sum, out res);
                AmountToSend.Text = "sat| " + res;
                
            }
            
        }

        protected override void OnAppearing()
        {
        }

        private void AmountToSend_Completed(object sender, EventArgs e)
        {
            AmountToSend.Text = AmountToSend.Text + " sat";
        }

        private void AddressesBook_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SuccessfullTransaction_AddressesBook_Popup());
        }
    }
}