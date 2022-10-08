using Microsoft.Toolkit.Uwp.Notifications;

namespace SharpCore.Helpers
{
    public class DialogHelper
    {
        /// <summary>
        /// Show a Windows toast notification
        /// </summary>
        /// <param name="header">Text for the header</param>
        /// <param name="body">Text for the main body</param>
        /// <param name="image">URI for an image to be displayed</param>
        /// <param name="expiration">Time, in seconds, after which the notification expires</param>
        public static void ShowToast(string header, string body, Uri? image = null, int expiration = 60)
        {
            ToastContentBuilder toast = new ToastContentBuilder()
                .AddText(header)
                .AddText(body);

            if (image != null)
            {
                toast.AddInlineImage(image);
            }

            toast.Show(toast =>
            {
                toast.ExpirationTime = DateTime.Now.AddSeconds(expiration);
            });
        }
    }

}
