using System.IO;
using System.Net;

namespace FolderDesigner.ImageRetrieval
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

        public string GetResponse(string url)
        {
            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            string jsonString = null;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                jsonString = streamReader.ReadToEnd();
            }
            return jsonString;
        }
    }
}
