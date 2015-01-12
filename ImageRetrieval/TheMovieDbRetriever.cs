using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FolderDesigner.ImageRetrieval
{
    public class TheMovieDbRetriever : IImageRetriever
    {
        const string API_KEY = "7f940ec80fbfbb991bc50ea5eda0dbc6";
        const string BASE_API_URL = @"http://api.themoviedb.org/3";
        const string BASE_IMG_URL = @"http://image.tmdb.org/t/p/w500";

        readonly WebServiceUtil _webUtil;
        
        public TheMovieDbRetriever(WebServiceUtil webUtil)
        {
            _webUtil = webUtil;
        }

        public Bitmap FindImageByName(string name)
        {
            var searchUrl = GetSearchUrl(name);

            string jsonString = _webUtil.GetResponse(searchUrl);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(jsonString);
            var results = dict["results"] as JArray;
            if (results == null || results.Count == 0) throw new Exception(string.Format("Movie {0} was not found.", name));

            var resultObjects = results.Select(r => JsonConvert.DeserializeObject<IDictionary<string, object>>(r.ToString()));

            var mostPopular = resultObjects.OrderByDescending(r => (double)r["popularity"]).First();

            var posterPath = mostPopular["poster_path"].ToString();

            var posterUrl = GetImageUrl(posterPath);
            return _webUtil.GetBitmapFromUrl(posterUrl);
        }

        private string GetSearchUrl(string movieName)
        {
            return string.Format("{0}/search/movie?api_key={1}&query={2}", BASE_API_URL, API_KEY, movieName);
        }

        private string GetImageUrl(string imageRelativePath)
        {
            return string.Format("{0}{1}", BASE_IMG_URL, imageRelativePath);
        }
    }
}
