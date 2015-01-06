
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FolderDesigner
{
    public class SimpleIconConverter : IIconConverter
    {
        public void ConvertToIconOLD(string sourceImage)
        {
            var tempFile = Path.GetDirectoryName(sourceImage) + @"\temp.ico";
            var p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = "png2ico.exe",
                    Arguments = "\"" + tempFile + "\" \"" + sourceImage + "\""
                }
            };
            p.Start();
            p.WaitForExit();
            File.Delete(sourceImage);
            File.Move(tempFile, sourceImage);
        }

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
