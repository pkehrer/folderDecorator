using System.Drawing;
using System.IO;

namespace FolderDesigner.ImageEditing
{
    public class ImageCropper : IImageProcessor
    {
        public Bitmap ProcessImage(Bitmap sourceImage, DecorationParameters parameters)
        {
            var squareEdge = sourceImage.Height > sourceImage.Width
                   ? sourceImage.Width
                   : sourceImage.Height;

            var cropRect = new Rectangle(0, 0, squareEdge, squareEdge);
            return sourceImage.Clone(cropRect, sourceImage.PixelFormat);
        }
    }
}
