using System.Drawing;

namespace SharpCore;

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
        return Image.FromFile(FileHelper.GetPathToLocalFile(directory, filename));
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
        int MaxWidth = enlarge ? maxWidth : Math.Min(maxWidth, src.Width);
        int MaxHeight = enlarge ? maxHeight : Math.Min(maxHeight, src.Height);

        decimal Rnd = Math.Min(MaxWidth / (decimal)src.Width, MaxHeight / (decimal)src.Height);
        return new Size((int)Math.Round(src.Width * Rnd), (int)Math.Round(src.Height * Rnd));
    }
}
