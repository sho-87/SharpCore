using Microsoft.UI.Xaml.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace SharpCore;

public static class ImageExtensions
{
    /// <summary>
    /// Convert Image to a Bitmap
    /// </summary>
    /// <param name="img">Image to be converted</param>
    /// <returns>Converted bitmap</returns>
    public static Bitmap ToBitmap(this Image img)
    {
        return new Bitmap(img);
    }

    /// <summary>
    /// Overlay one image ontop of another
    /// </summary>
    /// <param name="baseImage">Bottom image</param>
    /// <param name="overlayImage">Top image</param>
    /// <param name="overlayScale">Scale of the top image</param>
    /// <param name="darken">Whether the resulting image should be darkened</param>
    /// <returns>Base image with the overlay image on top</returns>
    public static Image AddOverlayImage(this Image baseImage, Image overlayImage, double overlayScale = 0.5, bool darken = false)
    {
        Bitmap OverlayImage = new(overlayImage, (int)(overlayImage.Width * overlayScale), (int)(overlayImage.Height * overlayScale));

        Bitmap Result = new(baseImage.Width, baseImage.Height, PixelFormat.Format32bppPArgb);

        using Graphics Composite = Graphics.FromImage(Result);
        Composite.DrawImage(baseImage, 0, 0);
        Composite.DrawImage(OverlayImage, baseImage.Width / 2 - OverlayImage.Width / 2, baseImage.Height / 2 - OverlayImage.Height / 2);

        if (darken)
        {
            using Brush DarkBrush = new SolidBrush(Color.FromArgb(128, Color.Black));
            Composite.FillRectangle(DarkBrush, new Rectangle(0, 0, baseImage.Width, baseImage.Height));
        }

        return (Image)Result;
    }
}
