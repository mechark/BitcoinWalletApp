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
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Android.Content.Res;
using BitcoinWalletApp.Views.Popups;
using System.Runtime.Serialization;
using BitcoinWalletApp.Repos;
using BitcoinWalletApp.Models;

namespace BitcoinWalletApp.ViewModels
{
    public class User
    {
        public User()
        {
            foreach (TransactionModel transaction in App.TransactionRepo.GetAll())
            {
                TransactionsList.Add(transaction);
            }
        }

        // Properties

        private INavigation Navigation { get => Application.Current.MainPage.Navigation; }

        public string PublicKey { get; set; } = TemporarilyUserData.PublicKey;

        public string PrivateKey { get; set; } = TemporarilyUserData.PrivateKey;

        public decimal Balance { get; set; } = Convert.ToDecimal(TemporarilyUserData.Balance);

        public double RubleToBTC { get => Settings.RubToBTC; }

        public string CoinType { get; set; } = Settings.CoinType;

        //Transactions

        public ObservableCollection<TransactionModel> TransactionsList = new ObservableCollection<TransactionModel>();

        public bool HasTransactions { get; set; } = false;

        public int TransactionsCount { get; set; } = TemporarilyUserData.TransactionsCount;


        // Methods
        public ImageSource GetQRKey(string UserPubKey)
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(UserPubKey);

            return ImageSource.FromStream(() => new MemoryStream(keyQRCodeBytes));
        }

        public List<TransactionModel> GetAllTransaction() => App.TransactionRepo.GetAll();

        public decimal GetBalance(MoneyUnit moneyUnit, string userPubKey)
        {
            return UserInfo.GetUserBalance(moneyUnit, userPubKey);
        }

        public async Task<string> ChangeCoinTypeAsync()
        {
            return await Task.Run(() =>
            {
                if (HasTransactions)
                {
                    
                    foreach (TransactionModel transaction in TransactionsList)
                    {
                        transaction.StringAmount.Replace($" {CoinType}", "");
                        if (CoinType == "Sat")
                        {
                            string amountOfTransaction = (transaction.Amount * 100000).ToString().Replace(",", "");
                            string amount = String.Empty;
                            CommaInsert(amountOfTransaction, out amount);

                            transaction.StringAmount = amount + " sat";
                        }
                        else if (CoinType == "mBTC")
                        {
                            transaction.StringAmount = Math.Round(transaction.Amount * 1000, 2).ToString() + " mBTC";
                        }
                        else if (CoinType == "BTC")
                        {
                            transaction.StringAmount = transaction.Amount.ToString() + " BTC";
                        }
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
                changedString = stringToChange.Replace(" ", "");
                int stringLength = changedString.Length;

                switch (stringLength)
                {
                    case 4:
                        changedString = changedString.Insert(1, " ");
                        break;
                    case 5:
                        changedString = changedString.Insert(2, " ");
                        break;
                    case 6:
                        changedString = changedString.Insert(3, " ");
                        break;
                    case 7:
                        changedString = changedString.Insert(1, " ");
                        changedString = changedString.Insert(5, " ");
                        break;
                    case 8:
                        changedString = changedString.Insert(2, " ");
                        changedString = changedString.Insert(6, " ");
                        break;
                    case 9:
                        changedString = changedString.Insert(3, " ");
                        changedString = changedString.Insert(7, " ");
                        break;
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
