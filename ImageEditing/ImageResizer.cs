using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;

namespace FolderDesigner.ImageEditing
{
    public class ImageResizer
    {
        public void ResizeImage(string sourceImagePath, Size size, string folderTitle)
        {
            var resizedPath = sourceImagePath + "_RESIZED";
            using (var sourceImage = Image.FromFile(sourceImagePath))
            using (var resized = new Bitmap(sourceImage, size))
            using (var folderShape = DrawFolderShape(resized, folderTitle))
            {
                folderShape.Save(resizedPath);
            }
            File.Delete(sourceImagePath);
            File.Move(resizedPath, sourceImagePath);
        }

        private Image DrawFolderShape(Image StartImage, string folderTitle)
        {
            folderTitle = new CultureInfo("en-US", false).TextInfo.ToTitleCase(folderTitle);
            var RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
            using (var g = Graphics.FromImage(RoundedImage))
            using (var gp = new GraphicsPath())
            {
                var fontName = "Arial Black";
                var textBrush = new SolidBrush(Color.White);
                var fontSize = 11;
                Font font = null;
                int stringlength = -1;
                var tabOffset = 10;

                do
                {
                    if (fontSize == 1) continue;
                    fontSize--;
                    font = new Font(fontName, fontSize);
                    stringlength = (int)g.MeasureString(folderTitle, font).Width;
                } while (stringlength > RoundedImage.Width - tabOffset - 45);

                var textPadding = 10;
                var cornerRadius = 10;
                var tabHeight = 15;
                var tabWidth = System.Math.Max(stringlength + textPadding * 2, 90);

                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var brush = new TextureBrush(StartImage);
                gp.AddArc(0, tabHeight, cornerRadius, cornerRadius, 180, 90);
                gp.AddLine(new Point(cornerRadius, tabHeight), new Point(tabOffset, tabHeight));
                gp.AddArc(tabOffset, 0, cornerRadius, cornerRadius, 180, 90);
                gp.AddArc(tabOffset + tabWidth - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
                gp.AddLine(new Point(tabOffset + tabWidth, 0), new Point(tabOffset + tabWidth, tabHeight));
                gp.AddArc(0 + RoundedImage.Width - cornerRadius, tabHeight, cornerRadius, cornerRadius, 270, 90);
                gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                gp.AddArc(0, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                g.FillPath(brush, gp);
                g.DrawString(folderTitle, font, textBrush, new Point(tabOffset + 10, 10 - fontSize));
            }
            return RoundedImage;
        }
    }
}
