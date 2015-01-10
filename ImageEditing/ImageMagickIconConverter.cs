using ImageMagick;
using System.IO;

namespace FolderDesigner.ImageEditing
{
    public class ImageMagickIconConverter : IIconConverter
    {
        public void ConvertToIcon(string sourceImage)
        {
            var tempFile = Path.GetDirectoryName(sourceImage) + @"\temp.ico";
            using (var image = new MagickImage(sourceImage))
            {
                image.Write(tempFile);
            }
            File.Delete(sourceImage);
            File.Move(tempFile, sourceImage);
        }
    }
}
