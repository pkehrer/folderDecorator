using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class DirectoryNameSanitizer
    {

        public string SanitizeDirectoryName(string directoryName)
        {
            directoryName = ReplacePeriods(directoryName);
            return directoryName;
        }

        private string ReplacePeriods(string oldstring)
        {
            return oldstring.Replace(".", " ");
        }

    }
}
