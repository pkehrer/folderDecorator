using System.IO;

namespace FolderDesigner
{
    class DesktopIniMaker
    {
        public void CreateDesktopIni(string directoryPath, string relativeIconPath)
        {
            var filePath = string.Format(@"{0}\Desktop.ini", directoryPath);
            var lines = new[]
            {
                @"[.ShellClassInfo]",
                @"ConfirmFileOp=0",
                @"NoSharing=1",
                @"IconFile=" + relativeIconPath,
                @"IconResource=" + relativeIconPath,
                @"IconIndex=0",
                @"InfoTip=Made with Paul's Folder Decorator."
            };
            File.Delete(filePath);
            File.WriteAllLines(filePath, lines);
        }

    }
}
