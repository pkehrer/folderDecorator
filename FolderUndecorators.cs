using System.IO;

namespace FolderDesigner
{
    public class FolderUndecorator
    {
        readonly AttributeChanger _attributeChanger;

        public FolderUndecorator(AttributeChanger attributeChanger)
        {
            _attributeChanger = attributeChanger;
        }

        public void UndecorateFolder(string path)
        {
            _attributeChanger.MakeNotSystemFolder(path);
            var desktopIniPath = Path.Combine(path, Config.DesktopIniFilename);
            var foldericonPath = Path.Combine(path, Config.IconFilename);
            var sourceImagePath = Path.Combine(path, "sourceImage.jpg");
            File.Delete(desktopIniPath);
            File.Delete(foldericonPath);
            File.Delete(sourceImagePath);
        }
    }
}
