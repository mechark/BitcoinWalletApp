using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using System.IO;
using Xamarin.Forms;
using BitcoinWalletApp.iOS;

[assembly: Dependency(typeof(MediaSave))]
namespace BitcoinWalletApp.iOS
{
    public class MediaSave : IMediaSave
    {
        public void SavePicture(byte[] imageByte, string fileName)
        {
            
            var path = Path.Combine(Environment.SpecialFolder.CommonPictures.ToString(), "BWAIM");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, fileName + ".png");
            File.WriteAllBytes(path, imageByte);
        }
    }
}