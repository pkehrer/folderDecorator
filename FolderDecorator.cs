using Autofac.Features.Indexed;
using FolderDesigner.ImageEditing;
using FolderDesigner.ImageRetrieval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FolderDesigner
{
    public class FolderDecorator
    {
        readonly DecorationParametersFactory _parametersFactory;
        readonly IIndex<MediaType, IImageRetriever> _imageRetrieverIndex;
        readonly IReadOnlyCollection<IImageProcessor> _imageProcessors;
        readonly ImageMagickIconConverter _iconConverter;
        readonly FolderIconChanger _iconChanger;
        readonly FolderUndecorator _undecorator;
        
        public FolderDecorator(
            DecorationParametersFactory paramFactory,
            IIndex<MediaType, IImageRetriever> imageRetrieverIndex,
            IReadOnlyCollection<IImageProcessor> imageProcessors,
            ImageMagickIconConverter iconConverter,
            FolderIconChanger iconChanger,
            FolderUndecorator undecorator)
        {
            _parametersFactory = paramFactory;
            _undecorator = undecorator;
            _imageRetrieverIndex = imageRetrieverIndex;
            _imageProcessors = imageProcessors;
            _iconConverter = iconConverter;
            _iconChanger = iconChanger;
        }

        public FolderDecorationResult DecorateFolder(MediaType mediaType, string directoryPath)
        {
            try
            {
                var parameters = _parametersFactory.GetParameters(mediaType, directoryPath);
                CreateIcon(parameters);
                _iconChanger.SetFileAsFolderIcon(parameters.FolderIconPath);
                return FolderDecorationResult.Successful(directoryPath);
            } catch (Exception e) {
                try { _undecorator.UndecorateFolder(directoryPath); } catch { }
                return FolderDecorationResult.Failed(directoryPath, e.Message);
            }
        }

        private void CreateIcon(DecorationParameters parameters)
        {
            var iconPath = Path.Combine(parameters.DirectoryPath, Config.IconFilename);
            File.Delete(iconPath);
            var image = _imageRetrieverIndex[parameters.MediaType].FindImageByName(parameters.MediaName);
            
            foreach (var processor in _imageProcessors)
            {
                var temp = processor.ProcessImage(image, parameters);
                image.Dispose();
                image = temp;
            }

            _iconConverter.ConvertToIcon(image, parameters.FolderIconPath);
            image.Dispose();
        }
    }
}
