﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using QRCodeKeys;
using BitcoinWalletApp.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Xml.Serialization;
using System.IO;
using BitcoinWalletApp.Repos;
using BitcoinWalletApp.Models;

namespace BitcoinWalletApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetails_Popup : PopupPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        public TransactionModel Transaction { get; set; }

        public ICommand CopyHashCommand => new Command(CopyHash_Clicked);
        public ICommand CopyFromSendCommand => new Command(CopyFromSend_Clicked);
        public ICommand CopyToSendCommand => new Command(CopyToSend_Clicked);

        public TransactionDetails_Popup(int selectedItemIndex)
        {
            Transaction = User.TransactionsList[selectedItemIndex];
            InitializeComponent();

            HashLabel.Text = Transaction.TransactionHash.Substring(0, 22) + "...";
            FromLabel.Text = Transaction.UserAddress.Substring(0, 22) + "...";
            ToLabel.Text = Transaction.ReceiverAddress.Substring(0, 22) + "...";
            AmountLabel.Text = Transaction.StringAmount;
            TimeLabel.Text = Transaction.TransactionTime;
            TypeLabel.Text = Transaction.TransactionType;
            TypeLabel.TextColor = Color.FromHex("#CC0033");

            MainFrame.HeightRequest = Details.Height;
            BindingContext = this;
        }

        private void CopyHash_Clicked () => User.CopySomething(Transaction.TransactionHash);
        private void CopyFromSend_Clicked () => User.CopySomething(Transaction.UserAddress);
        private void CopyToSend_Clicked () => User.CopySomething(Transaction.ReceiverAddress);

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();

        }
    }
}