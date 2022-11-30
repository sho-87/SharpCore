using Microsoft.UI.Xaml.Data;
using System.Drawing;

namespace SharpCore.Converters;

public class BitmapToBitmapImage : IValueConverter
{
    /// <summary>
    /// Returns a BitmapImage
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return ((Bitmap)value).ToBitmapImage();
    }

    /// <summary>
    /// Returns a bool
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
