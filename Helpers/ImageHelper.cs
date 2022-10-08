using System.Drawing;
using System.Drawing.Imaging;

namespace SharpCore.Helpers;

public class ImageHelper
{
    /// <summary>
    /// Load image from a local directory
    /// </summary>
    /// <param name="directory">Name of containing directory</param>
    /// <param name="filename">Name of image file with extension</param>
    /// <returns>Loaded image</returns>
    public static Image LoadImage(string directory, string filename)
    {
        return Image.FromFile(DirectoryHelper.GetPathToLocalFile(directory, filename));
    }

    /// <summary>
    /// Calculate desired width and height of an input, while maintaining the aspect ratio
    /// </summary>
    /// <param name="src">Size of original input</param>
    /// <param name="maxWidth">Maximum desired width</param>
    /// <param name="maxHeight">Maximum desired height</param>
    /// <param name="enlarge">Whether image will be enlarged</param>
    /// <returns>New Size instance with aspect ratio preserved</returns>
    public static Size CalcSizeMaintainAspect(Size src, int maxWidth, int maxHeight, bool enlarge = false)
    {
        maxWidth = enlarge ? maxWidth : Math.Min(maxWidth, src.Width);
        maxHeight = enlarge ? maxHeight : Math.Min(maxHeight, src.Height);

        decimal rnd = Math.Min(maxWidth / (decimal)src.Width, maxHeight / (decimal)src.Height);
        return new Size((int)Math.Round(src.Width * rnd), (int)Math.Round(src.Height * rnd));
    }

    /// <summary>
    /// Overlay one image ontop of another
    /// </summary>
    /// <param name="baseImage">Bottom image</param>
    /// <param name="overlayImage">Top image</param>
    /// <param name="overlayScale">Scale of the top image</param>
    /// <param name="darken">Whether the resulting image should be darkened</param>
    /// <returns>Base image with the overlay image on top</returns>
    public static Bitmap OverlayImages(Image baseImage, Image overlayImage, double overlayScale = 1.0, bool darken = false)
    {
        overlayImage = new Bitmap(overlayImage, (int)(overlayImage.Width * overlayScale), (int)(overlayImage.Height * overlayScale));

        var result = new Bitmap(baseImage.Width, baseImage.Height, PixelFormat.Format32bppPArgb);

        using var g = Graphics.FromImage(result);
        g.DrawImage(baseImage, 0, 0);
        g.DrawImage(overlayImage, baseImage.Width / 2 - overlayImage.Width / 2, baseImage.Height / 2 - overlayImage.Height / 2);

        if (darken)
        {
            using Brush darkBrush = new SolidBrush(Color.FromArgb(128, Color.Black));
            g.FillRectangle(darkBrush, new Rectangle(0, 0, baseImage.Width, baseImage.Height));
        }

        return result;
    }
}
