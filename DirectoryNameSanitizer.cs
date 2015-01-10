using System.Text.RegularExpressions;

namespace FolderDesigner
{
    public class DirectoryNameSanitizer
    {
        public string SanitizeDirectoryName(MediaType type, string directoryName)
        {
            switch (type)
            {
                case MediaType.Movie:
                case MediaType.Tv:
                    return SanitizeMovieOrTVName(directoryName);
                case MediaType.Music:
                    return SanitizeMusicName(directoryName);
                default:
                    return directoryName;
            }
        }

        private string SanitizeMusicName(string directoryName)
        {
            directoryName = Regex.Replace(directoryName, @"\(.*\)", " ");
            directoryName = Regex.Replace(directoryName, @"\[.*\]", " ");
            directoryName = Regex.Replace(directoryName, @"\d{4}", " ");
            directoryName = directoryName.Replace('-', ' ');
            directoryName = Regex.Replace(directoryName, @"\s+", " ");
            directoryName = directoryName.ToLower();
            directoryName = directoryName.Trim();
            return directoryName;
        }

        private string SanitizeMovieOrTVName(string directoryName)
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
