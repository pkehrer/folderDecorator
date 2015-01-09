using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    public class IconMaker
    {
        private readonly ImageRetrieverFactory _imageRetriever;
        private readonly ImageCropper _imageCropper;
        private readonly ImageResizer _imageResizer;
        private readonly IIconConverter _iconConverter;
        private readonly AttributeChanger _attributeChanger;
        private readonly DirectoryNameSanitizer _sanitizer;

        public IconMaker(ImageRetrieverFactory imageRetriever,
                         ImageCropper imageCropper, 
                         ImageResizer imageResizer,
                         IIconConverter iconConverter,
                         AttributeChanger attribChanger,
                         DirectoryNameSanitizer nameSanitizer)
        {
            _imageRetriever = imageRetriever;
            _imageCropper = imageCropper;
            _imageResizer = imageResizer;
            _iconConverter = iconConverter;
            _attributeChanger = attribChanger;
            _sanitizer = nameSanitizer;
        }

        public void MakeIconByName(MediaType mediaType, string name, string destinationFilePath)
        {
            name = _sanitizer.SanitizeDirectoryName(name);
            _attributeChanger.RemoveAttributes(destinationFilePath, FileAttributes.System);
            _attributeChanger.RemoveAttributes(destinationFilePath, FileAttributes.Hidden);
            _imageRetriever.GetImageRetriever(mediaType).FindImageByName(name, destinationFilePath);
            _imageCropper.CropImageSquare(destinationFilePath);
            _imageResizer.ResizeImage(destinationFilePath, new Size(Config.IconSize, Config.IconSize), name);
            _iconConverter.ConvertToIcon(destinationFilePath);
        }

    }
}
