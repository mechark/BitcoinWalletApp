﻿using System;
using BitcoinWalletApp.ViewModels;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using TabbedDemo.Droid;
using Android.Views;

namespace BitcoinWalletApp.Droid
{
    [Activity(Label = "BitcoinWalletApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static int TabHeight { get; set; }

        private User User { get => App.Current.Properties["UObject"] as User; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

            int resourceId = Resources.GetIdentifier("design_bottom_navigation_height", "dimen", this.PackageName);
            int height = 0;
            if (resourceId > 0)
            {
                height = Resources.GetDimensionPixelSize(resourceId);
                TabHeight = height;
            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}