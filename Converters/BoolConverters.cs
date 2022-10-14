using Microsoft.UI.Xaml.Data;

namespace SharpCore.Converters;

public class BoolToVisibility : IValueConverter
{
    /// <summary>
    /// Returns a visibility state
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if ((bool)value)
        {
            return Microsoft.UI.Xaml.Visibility.Visible;
        } else
        {
            return Microsoft.UI.Xaml.Visibility.Collapsed;
        }
    }

    /// <summary>
    /// Returns a bool
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
