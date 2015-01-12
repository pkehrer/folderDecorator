using ImageMagick;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FolderDesigner.ImageEditing
{
    public class ImageMagickIconConverter
    {
        public void ConvertToIcon(Bitmap sourceImage, string iconpath)
        {
            using (var sourceStream = new MemoryStream())
            {
                sourceImage.Save(sourceStream, ImageFormat.Jpeg);
                sourceStream.Position = 0;
                using (var image = new MagickImage(sourceStream, new MagickReadSettings { Format = MagickFormat.Jpeg }))
                {
                    image.Write(iconpath);
                }
            }            
        }

    }
}
