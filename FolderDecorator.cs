using System;
using System.IO;

namespace FolderDesigner
{
    public class FolderDecorator
    {
        readonly IconMaker _iconMaker;
        readonly FolderIconChanger _desktopIniMaker;
        
        public FolderDecorator(
            IconMaker iconMaker, 
            FolderIconChanger desktopIniMaker)
        {
            _iconMaker = iconMaker;
            _desktopIniMaker = desktopIniMaker;
        }

        public FolderDecorationResult DecorateFolder(MediaType type, string directoryPath)
        {
            try
            {
                var folderName = directoryPath.Substring(directoryPath.LastIndexOf(@"\")).Replace(@"\", "");
                _iconMaker.MakeIconByName(type, folderName, Path.Combine(directoryPath, "foldericon.ico"));
                _desktopIniMaker.CreateDesktopIni(directoryPath, "foldericon.ico");
                return new FolderDecorationResult
                {
                    Success = true,
                    Directory = directoryPath
                };
            } catch (Exception e) {
                return new FolderDecorationResult
                {
                    Success = false,
                    ErrorMessage = e.Message,
                    Directory = directoryPath
                };
            }
        }
    }
}
