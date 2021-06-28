using System;
using System.Collections.Generic;
using NBitcoin;
using QRCodeKeys;
using Transactions;
using System.IO;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Android.Content.Res;
using BitcoinWalletApp.Views.Popups;

namespace BitcoinWalletApp.ViewModels
{
    [Serializable]
    public class User
    {
        public User() { }

        // Properties

        private INavigation Navigation { get => Application.Current.MainPage.Navigation; }

        public UserInfo UserInfo { get; set; }

        public CreateWallet Wallet { get; set; }

        public string MainPubKey { get; set; }

        public string MainPrivKey { get; set; }

        public Dictionary<string, string> Keys { get; set; }

        public ObservableCollection<string> PublicKeys { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<Address> Addresses { get; set; } = new ObservableCollection<Address>();

        public decimal Balance { get; set; }

        public string CoinType { get; set; }

        //Transactions

        public bool HasTransactions { get; set; } = false;

        public int TransactionsCount { get; set; }

        public List<decimal> AmountOfTransactions { get; set; }

        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();

        public List<string> TransactionsType { get; set; }

        public List<string> TransactionDateTime { get; set; }

        public List<string> TransactionsHash { get; set; }


        // Methods

        public ImageSource GetQRKey(string UserPubKey)
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(UserPubKey);

            return ImageSource.FromStream(() => new MemoryStream(keyQRCodeBytes));
        }

        public decimal GetBalance(MoneyUnit moneyUnit, string userPubKey)
        {
            return UserInfo.GetUserBalance(moneyUnit, userPubKey);
        }

        public async Task<string> ChangeCoinTypeAsync()
        {
            return await Task.Run(() =>
            {
                foreach (Transaction transaction in Transactions)
                {
                    if (CoinType == "Sat")
                    {
                        string amountOfTransaction;
                        CommaInsert((Convert.ToDouble(transaction.DecimalAmountOfTransaction) * 100000).ToString(), out amountOfTransaction);

                        transaction.AmountOfTransaction = amountOfTransaction + " sat";
                    }
                    else if (CoinType == "mBTC")
                    {
                        transaction.AmountOfTransaction = Math.Round(transaction.DecimalAmountOfTransaction * 1000, 2).ToString() + " mBTC";
                    }
                    else if (CoinType == "BTC")
                    {
                        transaction.AmountOfTransaction = transaction.DecimalAmountOfTransaction + " BTC";
                    }
                }

                foreach (Address address in Addresses)
                {
                    if (CoinType == "Sat")
                    {
                        address.PublicKeyBalance = (address.DecimalBalance * 100000).ToString() + " sat";
                    }
                    else if (CoinType == "mBTC")
                    {
                        address.PublicKeyBalance = (address.DecimalBalance * 1000).ToString() + " mBTC";
                    }
                    else if (CoinType == "BTC")
                    {
                        address.PublicKeyBalance = address.DecimalBalance + " BTC";
                    }
                }

                return "";
            });
        }

        public void CommaInsert(string stringToChange, out string changedString)
        {
            changedString = "";

            if (!String.IsNullOrEmpty(stringToChange))
            {
                changedString = stringToChange.Replace(",", "");

                if (changedString.Length == 4)
                {
                    changedString = changedString.Insert(1, ",");
                }
                else if (changedString.Length == 5)
                {
                    changedString = changedString.Insert(2, ",");
                }
                else if (changedString.Length == 6)
                {
                    changedString = changedString.Insert(3, ",");
                }
                else if (changedString.Length == 7)
                {
                    changedString = changedString.Insert(1, ",");
                    changedString = changedString.Insert(5, ",");
                }
                else if (changedString.Length == 8)
                {
                    changedString = changedString.Insert(2, ",");
                    changedString = changedString.Insert(6, ",");
                }
                else if (changedString.Length == 9)
                {
                    changedString = changedString.Insert(3, ",");
                    changedString = changedString.Insert(7, ",");
                }
            }
        }
        public void CopySomething(string stringToCopy)
        {
            if (Clipboard.GetTextAsync().ToString() != stringToCopy)
            {
                Clipboard.SetTextAsync(stringToCopy);
                Navigation.PushPopupAsync(new CopyPopup(), true);
                Navigation.PopPopupAsync(true);
            }
        }

        public void SaveImage(string addressToGenerateQRCode)
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(addressToGenerateQRCode);

            DependencyService.Get<IMediaSave>().SavePicture(keyQRCodeBytes, "Address_" + addressToGenerateQRCode);
            Navigation.PushPopupAsync(new DownloadQRImagePopup());
            Navigation.PopPopupAsync(true);
        }
    }
}
