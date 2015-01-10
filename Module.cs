using Autofac;
using FolderDesigner.ImageEditing;
using FolderDesigner.ImageRetrieval;
using FolderDesigner.ViewModels;

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
            builder.RegisterType<IconMaker>().SingleInstance();
            builder.RegisterType<TheTvDbRetriever>().SingleInstance();
            builder.RegisterType<TheMovieDbRetriever>().SingleInstance();
            builder.RegisterType<LastFmRetriever>().SingleInstance();
            builder.RegisterType<ImageRetrieverFactory>().SingleInstance();
            builder.RegisterType<ImageCropper>().SingleInstance();
            builder.RegisterType<ImageResizer>().SingleInstance();
            builder.RegisterType<ImageMagickIconConverter>().As<IIconConverter>().SingleInstance();
            builder.RegisterType<FolderIconChanger>().SingleInstance();
            builder.RegisterType<FolderDecorator>().SingleInstance();
            builder.RegisterType<FolderUndecorator>().SingleInstance();
            builder.RegisterType<MainViewModel>();
            return builder.Build();
        }
    }
}
