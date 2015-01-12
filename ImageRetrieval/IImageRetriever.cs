using System.Drawing;
using System.IO;

namespace FolderDesigner.ImageRetrieval
{
    public interface IImageRetriever
    {
        Bitmap FindImageByName(string name);
    }
}
