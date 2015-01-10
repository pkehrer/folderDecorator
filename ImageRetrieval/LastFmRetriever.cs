using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderDesigner.ImageRetrieval
{
    public class LastFmRetriever : IImageRetriever
    {
        const string API_KEY = @"aa1f65a551115f56627077be31bdfc68";
        const string SECRET = @"f4c4273ec53d0be842a8adbfb686414e";

        readonly WebServiceUtil _webUtil;

        public LastFmRetriever(WebServiceUtil webUtil)
        {
            _webUtil = webUtil;
        }

        public void FindImageByName(string albumName, string destinationPath)
        {
            var jsonString = _webUtil.GetResponse(GetSearchUrl(albumName));

            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(jsonString);
            var resultsObject = dict["results"] as JObject;
            if (resultsObject == null) throw new Exception(string.Format("Response from LastFM invalid for {0}", albumName));
            var albummatches = resultsObject["albummatches"] as JObject;
            if (albummatches == null) throw new Exception(string.Format("No results returned for search {0}", albumName));
            var albums = albummatches["album"] as JArray;
            if (albums == null || albums.Count() == 0) throw new Exception(string.Format("No results returned for search {0}", albumName));

            var imgUrl = albums.First["image"].First(i => i["size"].ToString() == "extralarge")["#text"].ToString();
            _webUtil.CopyUrlToFile(imgUrl, destinationPath);
        }

        private string GetSearchUrl(string name)
        {
            return string.Format(@"http://ws.audioscrobbler.com/2.0/?method=album.search&album={0}&api_key={1}&format=json",
                name.Replace(' ', '+'),
                API_KEY);
        }
    }
}
