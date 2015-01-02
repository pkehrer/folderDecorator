using System.Drawing;
using System.IO;

namespace FolderDesigner
{
    class ImageCropper
    {

        public void CropImageSquare(string imagePath)
        {
            var croppedImagePath = imagePath + "_CROPPED";
            using (var sourceImage = Image.FromFile(imagePath) as Bitmap)
            {
                var squareEdge = sourceImage.Height > sourceImage.Width
                    ? sourceImage.Width
                    : sourceImage.Height;

                var cropRect = new Rectangle(50, 0, squareEdge, squareEdge);
                var cropped = sourceImage.Clone(cropRect, sourceImage.PixelFormat);
                cropped.Save(croppedImagePath);
            }
            File.Delete(imagePath);
            File.Move(croppedImagePath, imagePath);
        }

    }
}
