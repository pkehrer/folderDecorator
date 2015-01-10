
namespace FolderDesigner.ImageRetrieval
{
    public class ImageRetrieverFactory
    {
        readonly TheTvDbRetriever _tvRetriever;
        readonly TheMovieDbRetriever _movieRetriever;
        readonly LastFmRetriever _musicRetriever;

        public ImageRetrieverFactory(
            TheTvDbRetriever tvRetriever,
            TheMovieDbRetriever movieRetriever,
            LastFmRetriever musicRetriever)
        {
            _tvRetriever = tvRetriever;
            _movieRetriever = movieRetriever;
            _musicRetriever = musicRetriever;
        }

        public IImageRetriever GetImageRetriever(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.Tv:
                    return _tvRetriever;
                case MediaType.Music:
                    return _musicRetriever;
                case MediaType.Movie:
                default:
                    return _movieRetriever;
                
            }
        }
    }
}
