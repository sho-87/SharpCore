﻿using Microsoft.UI.Xaml.Data;
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

        var c = (Color)value;
        return Windows.UI.Color.FromArgb(c.A, c.R, c.G, c.B);
    }

    /// <summary>
    /// Returns a color object
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return new Color();

        var c = (Windows.UI.Color)value;
        return Color.FromArgb(Int32.Parse(c.ToString().Replace("#", ""), NumberStyles.HexNumber));
    }
}
