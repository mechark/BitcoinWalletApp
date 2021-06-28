using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinWalletApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace BitcoinWalletApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressDetails_Popup : PopupPage
    {
        private User User { get => App.Current.Properties["UObject"] as User; }

        private Address Address { get; set; }

        public ICommand CopyAddressCommand => new Command(CopyAddress_Clicked);

        public ICommand DownloadQRCodeCommand => new Command(SaveImage_Clicked);

        public AddressDetails_Popup(int addressIndex)
        {
            InitializeComponent();
            Address = User.Addresses[addressIndex];

            AddressQRCode.Source = Address.PublicKeyQRCode;
            AddressLabel.Text = Address.PublicKey;
            BalanceLabel.Text = Address.PublicKeyBalance;

            BindingContext = this;
        }

        private void CopyAddress_Clicked() => User.CopySomething(Address.PublicKey);

        private void SaveImage_Clicked() => User.SaveImage(Address.PublicKey);

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