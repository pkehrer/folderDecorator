using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    class IconMaker
    {
        private readonly IImageRetriever _imageRetriever;
        private readonly ImageCropper _imageCropper;
        private readonly ImageResizer _imageResizer;
        private readonly IconConverter _iconConverter;

        public IconMaker(IImageRetriever imageRetriever,
                         ImageCropper imageCropper, 
                         ImageResizer imageResizer,
                         IconConverter iconConverter)
        {
            _imageRetriever = imageRetriever;
            _imageCropper = imageCropper;
            _imageResizer = imageResizer;
            _iconConverter = iconConverter;
        }

        public void MakeIconByName(string name, string destinationFilePath)
        {
            _imageRetriever.FindImageByName(name, destinationFilePath);
            _imageCropper.CropImageSquare(destinationFilePath);
            _imageResizer.ResizeImage(destinationFilePath, new Size(128, 128));
            _iconConverter.ConvertToIcon(destinationFilePath);
        }

    }
}
