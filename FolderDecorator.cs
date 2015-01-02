using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    class FolderDecorator
    {
        private readonly IconMaker _iconMaker;
        private readonly DesktopIniMaker _desktopIniMaker;
        private readonly SystemFolderitizer _systemFolderitizer;

        public FolderDecorator(
            IconMaker iconMaker, 
            DesktopIniMaker desktopIniMaker,
            SystemFolderitizer systemFolderitizer)
        {
            _iconMaker = iconMaker;
            _desktopIniMaker = desktopIniMaker;
            _systemFolderitizer = systemFolderitizer;
        }

        public void DecorateFolder(string directoryPath)
        {
            var folderName = directoryPath.Substring(directoryPath.LastIndexOf(@"\")).Replace(@"\", "");
            _iconMaker.MakeIconByName(folderName, Path.Combine(directoryPath, "foldericon.ico"));
            _desktopIniMaker.CreateDesktopIni(directoryPath, "foldericon.ico");
            _systemFolderitizer.MakeSystemFolder(directoryPath);
        }

    }
}
