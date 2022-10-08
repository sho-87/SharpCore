using Microsoft.UI.Xaml.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace SharpCore;

public static class BitmapExtensions
{
    /// <summary>
    /// Convert Bitmap to BitmapImage (source)
    /// </summary>
    /// <param name="bitmap">Bitmap to be converted</param>
    /// <returns>Converted BitmapImage</returns>
    public static BitmapImage ToBitmapImage(this Bitmap bitmap)
    {
        // FIXME: The application called an interface that was marshalled for a different thread. 
        BitmapImage bitmapImage = new BitmapImage();
        using (MemoryStream stream = new MemoryStream())
        {
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;
            bitmapImage.SetSource(stream.AsRandomAccessStream());
        }
        return bitmapImage;
    }

    /// <summary>
    /// Resize a bitmap
    /// </summary>
    /// <param name="source">Bitmap to be resized</param>
    /// <param name="new_width">Desired width</param>
    /// <param name="new_height">Desired height</param>
    /// <returns>Resized bitmap</returns>
    public static Bitmap Resize(this Bitmap source, int new_width, int new_height)
    {
        float w_scale = (float)new_width / source.Width;
        float h_scale = (float)new_height / source.Height;

        float min_scale = Math.Min(w_scale, h_scale);

        var nw = (int)(source.Width * min_scale);
        var nh = (int)(source.Height * min_scale);

        var pad_dims_w = (new_width - nw) / 2;
        var pad_dims_h = (new_height - nh) / 2;

        var new_bitmap = new Bitmap(new_width, new_height, PixelFormat.Format24bppRgb);

        using (var g = Graphics.FromImage(new_bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            g.DrawImage(source, new Rectangle(pad_dims_w, pad_dims_h, nw, nh),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel);
        }

        return new_bitmap;
    }

    /// <summary>
    /// Resize a bitmap without edge padding
    /// </summary>
    /// <param name="source">Bitmap to be resized</param>
    /// <param name="new_width">Desired width</param>
    /// <param name="new_height">Desired height</param>
    /// <returns>Resized bitmap</returns>
    public static Bitmap ResizeWithoutPadding(this Bitmap source, int new_width, int new_height)
    {
        var new_bitmap = new Bitmap(new_width, new_height, PixelFormat.Format24bppRgb);

        using (var g = Graphics.FromImage(new_bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            g.DrawImage(source, new Rectangle(0, 0, new_width, new_height),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel);
        }

        return new_bitmap;
    }

    /// <summary>
    /// Resize bitmap while keeping the aspect ratio
    /// </summary>
    /// <param name="image">Bitmap to be resized</param>
    /// <param name="width">Maximum target width</param>
    /// <param name="height">Maximum target height</param>
    /// <returns>Resized bitmap</returns>
    public static Bitmap ResizeKeepAspect(this Bitmap image, int width, int height)
    {
        Size DesiredSize = ImageHelper.CalcSizeMaintainAspect(new Size(image.Width, image.Height), width, height);
        return image.ResizeWithoutPadding(DesiredSize.Width, DesiredSize.Height);
    }

    /// <summary>
    /// Save bitmap, with a suffix, to the My Pictures directory on Windows
    /// </summary>
    /// <param name="image">Image to be saved</param>
    /// <param name="suffix">Suffix for the filename</param>
    public static void SaveToMyPictures(this Bitmap image, string suffix)
    {
        var root = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        var filename = string.Format("{0}_{1}.png", DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss"), suffix);
        var filePath = Path.Combine(root, filename);

        image.Save(filePath);
    }
}
