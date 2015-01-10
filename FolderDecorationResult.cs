using System;
using System.IO;

namespace FolderDesigner
{
    public class FolderDecorationResult
    {
        public string Directory { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return Success 
                ? string.Format("Successfully decorated {0}", Path.GetFileName(Directory))
                : string.Format("Error decorating {0}:{1}{2}", Path.GetFileName(Directory), Environment.NewLine, ErrorMessage);
        }
    }
}
