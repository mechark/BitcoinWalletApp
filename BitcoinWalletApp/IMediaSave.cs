using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinWalletApp
{
    public interface IMediaSave
    {
        void SavePicture(byte [] imageInBytes, string imageName);
    }
}
