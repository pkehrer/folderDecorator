using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    class SystemFolderitizer
    {
        public void MakeSystemFolder(string directoryPath)
        {
            var p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
                    Arguments = "-Command attrib.exe +s '" + directoryPath + "'"
                }
            };
            p.Start();
            p.WaitForExit();
        }

    }
}
