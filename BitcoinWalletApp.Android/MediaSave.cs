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
using System.IO;
using Java.IO;
using Android.Graphics;
using File = Java.IO.File;
using AndroidX.Core.Content;
using Android;
using Android.Content.PM;
using Google.Android.Material.Snackbar;
using BitcoinWalletApp.Views.Popups;
using System.Threading;
using Xamarin.Essentials;
using Plugin.Permissions;
using Android.Media;

[assembly: Dependency(typeof(MediaSave))]
namespace BitcoinWalletApp.Droid
{
    public class MediaSave : IMediaSave
    {
        public async void SavePicture(byte[] imageByte, string fileName)
        {
            //   var path = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).ToString(), "BWAIM");

            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted)
            {
                var path = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString(), "BWAIM");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = System.IO.Path.Combine(path, fileName + ".png");
                System.IO.File.WriteAllBytes(path, imageByte);

                MediaScannerConnection.ScanFile(Android.App.Application.Context, new string[] { path }, null, null);
            }
            else
            {
                Plugin.Permissions.Abstractions.PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
            }
        }
    }
}