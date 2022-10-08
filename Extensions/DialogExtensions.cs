using Microsoft.UI.Xaml;

namespace SharpCore;

public static class DialogExtensions
{
    /// <summary>
    /// Attach a Window handle to a picker
    /// </summary>
    /// <typeparam name="T">Picker (e.g. FileOpen, FileSave)</typeparam>
    /// <param name="picker">Picker to associate with window</param>
    /// <param name="window">Application window to attach</param>
    public static void AttachWindowToPicker<T>(this T picker, Window window)
    {
        // Get the current window's HWND by passing in the Window object
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

        // Associate the HWND with the file picker
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
    }
}
