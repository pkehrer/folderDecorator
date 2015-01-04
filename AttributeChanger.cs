using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class AttributeChanger
    {
        public void MakeSystemFolder(string directoryPath)
        {
            AddAttributes(directoryPath, FileAttributes.System);
        }

        public void MakeNotSystemFolder(string directoryPath)
        {
            RemoveAttributes(directoryPath, FileAttributes.System);
        }

        public void AddAttributes(string path, FileAttributes attributes)
        {
            if (File.Exists(path))
                File.SetAttributes(path, File.GetAttributes(path) | attributes);
        }

        public void RemoveAttributes(string path, FileAttributes attributes)
        {
            if (File.Exists(path))
                File.SetAttributes(path, File.GetAttributes(path) & ~attributes);
        }
    }
}
