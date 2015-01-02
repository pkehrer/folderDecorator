﻿using System.IO;
using System.Net;
using System.Xml;

namespace FolderDesigner
{
    class TheTvDbRetriever : IImageRetriever
    {
        private const string _baseUrl = @"http://www.thetvdb.com";

        private string GetUrlForSeriesByName(string seriesName)
        {
            seriesName = seriesName.Replace(" ", "+");
            return string.Format("{0}/api/GetSeries.php?language=en&seriesname={1}", _baseUrl, seriesName);
        }

        private string GetUrlForBannerByRelativePath(string bannerRelativePath)
        {
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
