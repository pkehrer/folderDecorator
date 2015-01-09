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
            directoryName = directoryName
                .Replace(".", " ")
                .Replace("&", "and");
            directoryName = ChopOffDateAndTheRest(directoryName);
            directoryName = ChopOff720p1080p(directoryName);
            directoryName = directoryName.ToLower();
            directoryName = directoryName.Trim();
            return directoryName;
        }

        private string ChopOffDateAndTheRest(string oldstring)
        {
            oldstring = ChopPattern(oldstring, @"\[\d{4}");
            oldstring = ChopPattern(oldstring, @"\(\d{4}");
            return ChopPattern(oldstring, @"\d{4}"); 
        }

        private string ChopOff720p1080p(string oldstring)
        {
            oldstring = ChopPattern(oldstring, "1080p");
            return ChopPattern(oldstring, "720p");
        }


        private string ChopPattern(string oldstring, string pattern)
        {
            var indexOfDate = Regex.Match(oldstring, pattern).Index;
            return indexOfDate > 2 ? oldstring.Substring(0, indexOfDate) : oldstring;
        }

    }
}
