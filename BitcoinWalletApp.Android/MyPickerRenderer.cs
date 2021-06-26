using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BitcoinWalletApp.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics;

[assembly: ExportRenderer(typeof(Picker), typeof(MyPickerRenderer))]
namespace BitcoinWalletApp.Droid
{
    public class MyPickerRenderer : PickerRenderer
    {
        public MyPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            
        }
    }
}