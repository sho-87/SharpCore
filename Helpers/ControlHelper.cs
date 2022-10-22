using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace SharpCore;

public class ControlHelper
{
    /// <summary>
    /// Pan a scroll viewer control based on manipulation
    /// </summary>
    /// <param name="sv">Target scrollviewer</param>
    /// <param name="e">Manipulation move event</param>
    public static void PanScrollViewer(ScrollViewer sv, ManipulationDeltaRoutedEventArgs e)
    {
        double TranX = -1 * e.Delta.Translation.X;
        double TranY = -1 * e.Delta.Translation.Y;

        double OffsetX = TranX + sv.HorizontalOffset;
        double OffsetY = TranY + sv.VerticalOffset;

        sv.ChangeView(OffsetX, OffsetY, null, true);
    }
}
