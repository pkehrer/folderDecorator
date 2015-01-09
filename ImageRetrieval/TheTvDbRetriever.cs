using System;
using System.IO;
using System.Net;
using System.Xml;

namespace FolderDesigner
{
    public class TheTvDbRetriever : IImageRetriever
    {
        private readonly WebServiceUtil _webUtil;

        public TheTvDbRetriever(WebServiceUtil webUtil)
        {
            _webUtil = webUtil;
        }

        private const string _baseUrl = @"http://www.thetvdb.com";

        private string GetUrlForSeriesByName(string seriesName)
        {
            return string.Format("{0}/api/GetSeries.php?language=en&seriesname={1}", _baseUrl, seriesName);
        }

        private string GetUrlForBannerByRelativePath(string bannerRelativePath)
        {
            return string.Format("{0}/banners/{1}", _baseUrl, bannerRelativePath);
        }


        public void FindImageByName(string seriesName, string destinationPath)
        {
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
            var bannerUrl = GetUrlForBannerByRelativePath(bannerRelativePath);
            _webUtil.CopyUrlToFile(bannerUrl, destinationPath);
        }
    }
}
