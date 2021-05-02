using System;
using System.Collections.Generic;
using NBitcoin;
using QRCodeKeys;
using Transactions;
using System.IO;
using Xamarin.Forms;

namespace BitcoinWalletApp.ViewModels
{
    public class User
    {
        // Properties
        protected UserInfo userInfo
        {
            get => new UserInfo(pubKey);
            set => new UserInfo(pubKey);
        }

        public string pubKey { 
            get => (string)App.Current.Properties["pubKey"];
            set
            {
                if (App.Current.Properties["pubKey"] != null)
                {
                    value = (string)App.Current.Properties["pubKey"];
                }
            }
        }
        public string privKey 
        {
            get => (string)App.Current.Properties["privKey"];
            set
            {
                if (App.Current.Properties["privKey"] != null)
                {
                    value = (string)App.Current.Properties["privKey"];
                }
            }
        }

        public decimal GetBalance
        {
            get => userInfo.GetUserBalance(Network.Main, MoneyUnit.Satoshi);
        }

        public DateTime GetDateTimeLastTransaction
        {
            get => userInfo.GetUserRecentTransactionDateTime;
        }

        public decimal GetAmountLastTransaction
        {
            get => userInfo.GetUserRecentTransactionsTotalReceived(Network.Main, MoneyUnit.Satoshi);
        }

        // Methods
        public ImageSource GetQRKey()
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(pubKey);

            return ImageSource.FromStream(() => new MemoryStream(keyQRCodeBytes));
        }
    }
}
