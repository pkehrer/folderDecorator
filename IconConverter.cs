using System.Diagnostics;
using System.IO;

namespace FolderDesigner
{
    class IconConverter
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
                    FileName = "png2ico.exe",
                    Arguments = "\"" + tempFile + "\" \"" + sourceImage + "\""
                }
            };
            p.Start();
            p.WaitForExit();
            File.Delete(sourceImage);
            File.Move(tempFile, sourceImage);
        }
    }
}
