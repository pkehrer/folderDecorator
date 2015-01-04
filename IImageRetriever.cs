using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public interface IImageRetriever
    {
        void FindImageByName(string name, string destinationPath);
    }
}
