using FolderDesigner.ImageEditing;
using FolderDesigner.ImageRetrieval;
using System.Drawing;
using System.IO;

namespace FolderDesigner
{
    public class IconMaker
    {
        readonly ImageRetrieverFactory _imageRetriever;
        readonly ImageCropper _imageCropper;
        readonly ImageResizer _imageResizer;
        readonly IIconConverter _iconConverter;
        readonly AttributeChanger _attributeChanger;
        readonly DirectoryNameSanitizer _sanitizer;

        public IconMaker(
            ImageRetrieverFactory imageRetriever,
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
            name = _sanitizer.SanitizeDirectoryName(mediaType, name);
            _attributeChanger.RemoveAttributes(destinationFilePath, FileAttributes.System);
            _attributeChanger.RemoveAttributes(destinationFilePath, FileAttributes.Hidden);
            _imageRetriever.GetImageRetriever(mediaType).FindImageByName(name, destinationFilePath);
            _imageCropper.CropImageSquare(destinationFilePath);
            _imageResizer.ResizeImage(destinationFilePath, new Size(Config.IconSize, Config.IconSize), name);
            _iconConverter.ConvertToIcon(destinationFilePath);
        }
    }
}
