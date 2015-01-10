using System.Drawing;
using System.IO;

namespace FolderDesigner.ImageEditing
{
    public class ImageCropper
    {
        public void CropImageSquare(string imagePath)
        {
            File.Copy(imagePath, Path.Combine(Path.GetDirectoryName(imagePath), "sourceImage.jpg"), true);
            var croppedImagePath = imagePath + "_CROPPED";
            using (var sourceImage = Image.FromFile(imagePath) as Bitmap)
            {
                var squareEdge = sourceImage.Height > sourceImage.Width
                    ? sourceImage.Width
                    : sourceImage.Height;

                var cropRect = new Rectangle(0, 0, squareEdge, squareEdge);
                using (var cropped = sourceImage.Clone(cropRect, sourceImage.PixelFormat))
                {
                    cropped.Save(croppedImagePath);
                }
                
            }
            File.Delete(imagePath);
            File.Move(croppedImagePath, imagePath);
        }
    }
}
