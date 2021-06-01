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

namespace BitcoinWalletApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetails_Popup : PopupPage
    {
        protected User User { get => App.Current.Properties["UObject"] as User; }

        public ICommand DownloadQRCode => new Command(SaveQRPicture);

        public TransactionDetails_Popup()
        {
            InitializeComponent();
            TransactionInitialize();
        }

        private void TransactionInitialize ()
        {
            UserQRCodeKey.Source = User.GetQRKey(User.MainPubKey);
            
        }

        private void SaveQRPicture ()
        {
            byte[] keyQRCodeBytes = QRCodeKey.GenerateQRKey(User.MainPubKey);

            DependencyService.Get<IMediaSave>().SavePicture(keyQRCodeBytes, "PublicKey" + User.MainPubKey);
            Navigation.PushPopupAsync(new DownloadQRImagePopup());
        }

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