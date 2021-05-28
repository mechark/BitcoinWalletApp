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
        public void SaveImage(string name, Stream data, string location = "temp")
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Orders", location);
            Directory.CreateDirectory(documentsPath);

            string filePath = Path.Combine(documentsPath, name);

            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
        }
    }
}