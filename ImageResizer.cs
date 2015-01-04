using System.Drawing;
using System.IO;

namespace FolderDesigner
{
    public class ImageResizer
    {
        public void ResizeImage(string sourceImagePath, Size size)
        {
            var resizedPath = sourceImagePath + "_RESIZED";
            using (var sourceImage = Image.FromFile(sourceImagePath))
            {
                var resized = new Bitmap(sourceImage, size);
                resized.Save(resizedPath);
            }
            File.Delete(sourceImagePath);
            File.Move(resizedPath, sourceImagePath);
        }


    }
}
