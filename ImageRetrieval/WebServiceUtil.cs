using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class WebServiceUtil
    {
        public void CopyUrlToFile(string url, string filePath)
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFile(url, filePath);
            }
        }
    }
}
