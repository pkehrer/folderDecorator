using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class DirectoryUtil
    {
        public static string GetTemporaryFileName(string extension)
        {
            extension = extension.Replace(".", "");
            return Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "." + extension);
        }
    }
}
