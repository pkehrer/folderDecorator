using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class DecorationParametersFactory
    {
        readonly DirectoryNameSanitizer _sanitizer;

        public DecorationParametersFactory(DirectoryNameSanitizer sanitizer)
        {
            _sanitizer = sanitizer;
        }

        public DecorationParameters GetParameters(MediaType mediaType, string directoryPath)
        {
            return new DecorationParameters
            {
                DirectoryPath = directoryPath,
                MediaName = _sanitizer.SanitizeDirectoryName(mediaType, new FileInfo(directoryPath).Name),
                FolderIconPath = Path.Combine(directoryPath, Config.IconFilename),
                MediaType = mediaType
            };
        }
    }
}
