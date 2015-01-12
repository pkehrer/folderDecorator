using Autofac;
using FolderDesigner.ImageEditing;
using FolderDesigner.ImageRetrieval;
using FolderDesigner.ViewModels;
using System.Collections.Generic;

namespace FolderDesigner
{
    static class Module
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AttributeChanger>().SingleInstance();
            builder.RegisterType<IconCacheResetter>().SingleInstance();
            builder.RegisterType<DirectoryNameSanitizer>().SingleInstance();
            builder.RegisterType<WebServiceUtil>().SingleInstance();
            builder.RegisterType<DecorationParametersFactory>().SingleInstance();

            builder.RegisterType<TheTvDbRetriever>().Keyed<IImageRetriever>(MediaType.Tv).SingleInstance();
            builder.RegisterType<TheMovieDbRetriever>().Keyed<IImageRetriever>(MediaType.Movie).SingleInstance();
            builder.RegisterType<LastFmRetriever>().Keyed<IImageRetriever>(MediaType.Music).SingleInstance();

            builder.RegisterType<ImageCropper>().SingleInstance();
            builder.RegisterType<ImageResizer>().SingleInstance();
            builder.RegisterType<ImageMagickIconConverter>().SingleInstance();

            builder.Register(b => new List<IImageProcessor>
            {
                b.Resolve<ImageCropper>(),
                b.Resolve<ImageResizer>()
            }).As<IReadOnlyCollection<IImageProcessor>>().SingleInstance();
            
            builder.RegisterType<FolderIconChanger>().SingleInstance();
            builder.RegisterType<FolderDecorator>().SingleInstance();
            builder.RegisterType<FolderUndecorator>().SingleInstance();
            builder.RegisterType<MainViewModel>();
            return builder.Build();
        }
    }
}
