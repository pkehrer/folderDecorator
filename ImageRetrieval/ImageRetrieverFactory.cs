using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class ImageRetrieverFactory
    {
        private readonly TheTvDbRetriever _tvRetriever;
        private readonly TheMovieDbRetriever _movieRetriever;

        public ImageRetrieverFactory(
            TheTvDbRetriever tvRetriever,
            TheMovieDbRetriever movieRetriever)
        {
            _tvRetriever = tvRetriever;
            _movieRetriever = movieRetriever;
        }

        public IImageRetriever GetImageRetriever(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.Tv:
                    return _tvRetriever;
                case MediaType.Movie:
                default:
                    return _movieRetriever;
                
            }
        }

    }
}
