using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BitcoinWalletApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePage : ContentPage
    {
        private double DisplayHeight { get => DeviceDisplay.MainDisplayInfo.Height; }

        public ChangePage()
        {
            InitializeComponent();
            MainFrame.HeightRequest = DisplayHeight / 3.366906474820144;
        }
    }
}