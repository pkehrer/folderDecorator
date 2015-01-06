using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class ImageMagickIconConverter : IIconConverter
    {

        public void ConvertToIcon(string sourceImage)
        {
            var tempFile = Path.GetDirectoryName(sourceImage) + @"\temp.ico";
            var p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = @"ImageMagick\convert.exe",
                    Arguments = string.Format("\"{0}\" \"{1}\"", sourceImage, tempFile)
                }
            };
            p.Start();
            p.WaitForExit();
            File.Delete(sourceImage);
            File.Move(tempFile, sourceImage);
        }
    }
}
