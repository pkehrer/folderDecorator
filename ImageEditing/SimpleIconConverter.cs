using System.Drawing;
using System.IO;

namespace FolderDesigner.ImageEditing
{
    public class SimpleIconConverter : IIconConverter
    {
        public void ConvertToIcon(string sourceImage)
        {
            var tempFile = Path.GetDirectoryName(sourceImage) + @"\temp.ico";
            using (FileStream stream = File.OpenWrite(tempFile))
            using (var bitmap = (Bitmap)Image.FromFile(sourceImage))
            {
               Icon.FromHandle(bitmap.GetHicon()).Save(stream);
            }
            File.Delete(sourceImage);
            File.Move(tempFile, sourceImage);
        }
    }
}
