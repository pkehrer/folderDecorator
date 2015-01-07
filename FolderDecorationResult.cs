using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ? string.Format("Successfully decorated {0}", Directory)
                : string.Format("Error decorating {0}:{1}{2}", Directory, Environment.NewLine, ErrorMessage);
        }

    }
}
