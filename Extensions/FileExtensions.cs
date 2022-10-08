using Windows.Storage;
using Windows.Storage.FileProperties;

namespace SharpCore;

public static class FileExtensions
{
    /// <summary>
    /// Get file thumbnail icon
    /// </summary>
    /// <param name="file">File to get icon for</param>
    /// <param name="size">Desired size</param>
    /// <returns>Thumbnail icon for the file</returns>
    public static async Task<StorageItemThumbnail> GetFileIcon(this StorageFile file, uint size = 32)
    {
        StorageItemThumbnail iconTmb;
        var imgType = new[] { "bmp", "gif", "jpeg", "jpg", "png" }.FirstOrDefault(ext => file.Path.ToLower().EndsWith(ext));
        if (imgType != null)
        {
            var dummy = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("dummy." + imgType, CreationCollisionOption.ReplaceExisting); //may overwrite existing
            iconTmb = await dummy.GetThumbnailAsync(ThumbnailMode.SingleItem, size);
        }
        else
        {
            iconTmb = await file.GetThumbnailAsync(ThumbnailMode.SingleItem, size);
        }
        return iconTmb;
    }
}
