using System.Drawing;
using System.Drawing.Imaging;

namespace SharpCore;

public static class ImageExtensions
{
    /// <summary>
    /// Overlay one image ontop of another
    /// </summary>
    /// <param name="baseImage">Bottom image</param>
    /// <param name="overlayImage">Top image</param>
    /// <param name="overlayScale">Scale of the top image</param>
    /// <param name="darken">Whether the resulting image should be darkened</param>
    /// <returns>Base image with the overlay image on top</returns>
    public static Image AddOverlayImage(this Image baseImage, Image overlayImage, double overlayScale = 1.0, bool darken = false)
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

        return (Image)result;
    }
}
