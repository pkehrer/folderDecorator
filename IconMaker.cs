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
        private readonly IImageRetriever _imageRetriever;
        private readonly ImageCropper _imageCropper;
        private readonly ImageResizer _imageResizer;
        private readonly IconConverter _iconConverter;
        private readonly AttributeChanger _attributeChanger;

        public IconMaker(IImageRetriever imageRetriever,
                         ImageCropper imageCropper, 
                         ImageResizer imageResizer,
                         IconConverter iconConverter,
                         AttributeChanger attribChanger)
        {
            _imageRetriever = imageRetriever;
            _imageCropper = imageCropper;
            _imageResizer = imageResizer;
            _iconConverter = iconConverter;
            _attributeChanger = attribChanger;
        }

        public void MakeIconByName(string name, string destinationFilePath)
        {
            _attributeChanger.RemoveAttributes(destinationFilePath, FileAttributes.System);
            _attributeChanger.RemoveAttributes(destinationFilePath, FileAttributes.Hidden);
            _imageRetriever.FindImageByName(name, destinationFilePath);
            _imageCropper.CropImageSquare(destinationFilePath);
            _imageResizer.ResizeImage(destinationFilePath, new Size(128, 128));
            _iconConverter.ConvertToIcon(destinationFilePath);
        }

    }
}
