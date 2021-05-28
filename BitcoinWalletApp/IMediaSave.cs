using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BitcoinWalletApp
{
    public interface IMediaSave
    {
        void SavePicture(byte [] imageInBytes, string imageName);
    }
}
