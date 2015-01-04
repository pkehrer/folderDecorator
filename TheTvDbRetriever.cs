using System;
using System.IO;
using System.Net;
using System.Xml;

namespace FolderDesigner
{
    public class TheTvDbRetriever : IImageRetriever
    {
        private readonly DirectoryNameSanitizer _sanitizer;

        public TheTvDbRetriever(DirectoryNameSanitizer sanitizer)
        {
            _sanitizer = sanitizer;
        }

        private const string _baseUrl = @"http://www.thetvdb.com";

        private string GetUrlForSeriesByName(string seriesName)
        {
            return string.Format("{0}/api/GetSeries.php?language=en&seriesname={1}", _baseUrl, seriesName);
        }

        private string GetUrlForBannerByRelativePath(string bannerRelativePath)
        {
            //return @"http://www.thetvdb.com/banners/graphical/79563-g4.jpg";
            return string.Format("{0}/banners/{1}", _baseUrl, bannerRelativePath);
        }
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public void FindImageByName(string seriesName, string destinationPath)
        {
            seriesName = _sanitizer.SanitizeDirectoryName(seriesName);
            seriesName = seriesName.Replace(" ", "+");
            var url = GetUrlForSeriesByName(seriesName);

            var request = WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();
            string xmlString = null;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                xmlString = streamReader.ReadToEnd();
            }

            var doc = new XmlDocument();
            doc.LoadXml(xmlString);
            var node = doc.DocumentElement.SelectNodes("/Data/Series").Item(0);
            if (node == null) throw new Exception(string.Format("Couldn't find TV show with name {0}", seriesName));

            var bannerRelativePath = node.SelectSingleNode("banner").FirstChild.Value;


            request = WebRequest.Create(GetUrlForBannerByRelativePath(bannerRelativePath));
            response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            using (var file = File.Create(destinationPath))
            {
                CopyStream(streamReader.BaseStream, file);
            }
        }
    }
}
