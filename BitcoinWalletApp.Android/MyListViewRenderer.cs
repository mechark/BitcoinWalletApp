using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BitcoinWalletApp.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(MyListViewRenderer))]
namespace BitcoinWalletApp.Droid
{
    public class MyListViewRenderer : ListViewRenderer
    {
        public MyListViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            Control.SetSelector(Android.Resource.Color.Transparent);
            Control.CacheColorHint = Android.Graphics.Color.Transparent;
        }
    }
}