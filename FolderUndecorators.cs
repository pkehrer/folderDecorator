using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class FolderUndecorator
    {
        private AttributeChanger _attributeChanger;

        public FolderUndecorator(AttributeChanger attributeChanger)
        {
            _attributeChanger = attributeChanger;
        }

        public void UndecorateFolder(string path)
        {
            _attributeChanger.MakeNotSystemFolder(path);
            var desktopIniPath = Path.Combine(path, "Desktop.ini");
            var foldericonPath = Path.Combine(path, "foldericon.ico");
            File.Delete(desktopIniPath);
            File.Delete(foldericonPath);
        }

    }
}
