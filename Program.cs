using System.Drawing;
using System.IO;

namespace FolderDesigner
{
    class Program
    {
        void Main(string[] args)
        {
            var folderDecorator =
                new FolderDecorator(
                        new IconMaker(
                        new TheTvDbRetriever(),
                        new ImageCropper(),
                        new ImageResizer(),
                        new IconConverter()),
                    new DesktopIniMaker(),
                    new SystemFolderitizer());

            var startingDir = args[0];

            foreach (var directory in Directory.GetDirectories(startingDir))
            {
                try
                {
                    folderDecorator.DecorateFolder(directory);
                }
                catch
                {
                }
            }

            
            
            

        }
    }
}
