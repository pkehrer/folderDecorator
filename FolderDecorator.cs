using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class FolderDecorator
    {
        private readonly IconMaker _iconMaker;
        private readonly FolderIconChanger _desktopIniMaker;
        
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
