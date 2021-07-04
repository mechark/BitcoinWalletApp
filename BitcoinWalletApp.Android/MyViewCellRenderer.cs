using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using BitcoinWalletApp.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ViewCell), typeof(MyViewCellRenderer))]
namespace BitcoinWalletApp.Droid
{
    public class MyViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private bool _selected = false;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;
                var extendedViewCell = sender as ViewCell;
                if (_selected)
                    _cellCore.SetBackgroundColor(Android.Graphics.Color.ParseColor("#444D57"));
                else
                    _cellCore.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}
