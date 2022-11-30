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
        BitmapImage Image = new();
        using (MemoryStream Stream = new())
        {
            bitmap.Save(Stream, ImageFormat.Png);
            Stream.Position = 0;
            Image.SetSource(Stream.AsRandomAccessStream());
        }
        return Image;
    }

    /// <summary>
    /// Resize a bitmap
    /// </summary>
    /// <param name="source">Bitmap to be resized</param>
    /// <param name="targetWidth">Desired width</param>
    /// <param name="targetHeight">Desired height</param>
    /// <returns>Resized bitmap</returns>
    public static Bitmap Resize(this Bitmap source, int targetWidth, int targetHeight)
    {
        var ScaleW = (float)targetWidth / source.Width;
        var ScaleH = (float)targetHeight / source.Height;
        float MinScale = Math.Min(ScaleW, ScaleH);

        var NewWidth = (int)(source.Width * MinScale);
        var NewHeight = (int)(source.Height * MinScale);

        int PadW = (targetWidth - NewWidth) / 2;
        int PadH = (targetHeight - NewHeight) / 2;

        Bitmap NewBitmap = new(targetWidth, targetHeight, PixelFormat.Format24bppRgb);

        using (Graphics Composite = Graphics.FromImage(NewBitmap))
        {
            Composite.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            Composite.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            Composite.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            Composite.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            Composite.DrawImage(source, new Rectangle(PadW, PadH, NewWidth, NewHeight),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel);
        }

        return NewBitmap;
    }

    /// <summary>
    /// Resize a bitmap without edge padding
    /// </summary>
    /// <param name="source">Bitmap to be resized</param>
    /// <param name="targetWidth">Desired width</param>
    /// <param name="targetHeight">Desired height</param>
    /// <returns>Resized bitmap</returns>
    public static Bitmap ResizeWithoutPadding(this Bitmap source, int targetWidth, int targetHeight)
    {
        Bitmap NewBitmap = new(targetWidth, targetHeight, PixelFormat.Format24bppRgb);

        using (Graphics Composite = Graphics.FromImage(NewBitmap))
        {
            Composite.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            Composite.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            Composite.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            Composite.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            Composite.DrawImage(source, new Rectangle(0, 0, targetWidth, targetHeight),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel);
        }

        return NewBitmap;
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
        string Root = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string Filename = string.Format("{0}_{1}.png", DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss"), suffix);
        string FilePath = Path.Combine(Root, Filename);

        image.Save(FilePath);
    }
}
