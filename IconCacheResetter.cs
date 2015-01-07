using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class IconCacheResetter
    {
        public void ResetIconCache()
        {
            var p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = @"C:\Windows\System32\ie4uinit.exe",
                    Arguments = "-ClearIconCache"
                }
            };
            p.Start();
            p.WaitForExit();
        }
    }
}
