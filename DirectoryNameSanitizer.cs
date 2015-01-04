using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class DirectoryNameSanitizer
    {

        public string SanitizeDirectoryName(string directoryName)
        {
            directoryName = ReplacePeriods(directoryName);
            directoryName = ChopOffDateAndTheRest(directoryName);
            directoryName = LowerCase(directoryName);
            return directoryName;
        }

        private string ReplacePeriods(string oldstring)
        {
            return oldstring.Replace(".", " ");
        }

        private string ChopOffDateAndTheRest(string oldstring)
        {
            var indexOfDate = Regex.Match(oldstring, @"\d{4}").Index; 
            return indexOfDate > 2 ? oldstring.Substring(0, indexOfDate) : oldstring;
        }

        private string LowerCase(string oldstring)
        {
            return oldstring.ToLower();
        }

    }
}
