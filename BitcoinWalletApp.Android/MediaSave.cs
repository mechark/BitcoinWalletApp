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

[assembly: Dependency(typeof(MediaSave))]
namespace BitcoinWalletApp.Droid
{
    public class MediaSave : IMediaSave
    {
        public void SavePicture(byte[] imageByte, string fileName)
        {
            var path = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).ToString(), "BWAIM");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = System.IO.Path.Combine(path, fileName + ".png");
            System.IO.File.WriteAllBytes(path, imageByte);
        }
    }
}