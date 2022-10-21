using Microsoft.UI.Xaml.Data;
using System.Drawing;
using System.Globalization;

namespace SharpCore.Converters;

public class ColorToHex : IValueConverter
{
    /// <summary>
    /// Returns a hex color string
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return default(Color);

        var Color = (Color)value;
        return Windows.UI.Color.FromArgb(Color.A, Color.R, Color.G, Color.B);
    }

    /// <summary>
    /// Returns a color object
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return new Color();

        var Color = (Windows.UI.Color)value;
        return System.Drawing.Color.FromArgb(int.Parse(Color.ToString().Replace("#", ""), NumberStyles.HexNumber));
    }
}
