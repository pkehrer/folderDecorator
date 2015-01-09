using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class TheMovieDbRetriever : IImageRetriever
    {
        private const string API_KEY = "7f940ec80fbfbb991bc50ea5eda0dbc6";
        private const string BASE_API_URL = @"http://api.themoviedb.org/3";
        private const string BASE_IMG_URL = @"http://image.tmdb.org/t/p/w500";

        private readonly WebServiceUtil _webUtil;
        

        public TheMovieDbRetriever(WebServiceUtil webUtil)
        {
            _webUtil = webUtil;
        }

        private string GetSearchUrl(string movieName)
        {
            return string.Format("{0}/search/movie?api_key={1}&query={2}", BASE_API_URL, API_KEY, movieName);
        }

        private string GetImageUrl(string imageRelativePath)
        {
            return string.Format("{0}{1}", BASE_IMG_URL, imageRelativePath);
        }


        public void FindImageByName(string name, string destinationPath)
        {
            var searchUrl = GetSearchUrl(name);

            var request = WebRequest.Create(searchUrl);
            var response = (HttpWebResponse)request.GetResponse();
            string jsonString = null;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                jsonString = streamReader.ReadToEnd();
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(jsonString);
            var results = dict["results"] as JArray;
            if (results == null || results.Count == 0) throw new Exception(string.Format("Movie {0} was not found.", name));

            var resultObjects = results.Select(r => JsonConvert.DeserializeObject<IDictionary<string, object>>(r.ToString()));

            var mostPopular = resultObjects.OrderByDescending(r => (double)r["popularity"]).First();

            var posterPath = mostPopular["poster_path"].ToString();

            var posterUrl = GetImageUrl(posterPath);
            _webUtil.CopyUrlToFile(posterUrl, destinationPath);
        }

    }
}
