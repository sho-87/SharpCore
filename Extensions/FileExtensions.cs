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
        StorageItemThumbnail IconTmb;
        var ImgType = new[] { "bmp", "gif", "jpeg", "jpg", "png" }.FirstOrDefault(Ext => file.Path.ToLower().EndsWith(Ext));

        if (ImgType != null)
        {
            StorageFile Dummy = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("dummy." + ImgType, CreationCollisionOption.ReplaceExisting); //may overwrite existing
            IconTmb = await Dummy.GetThumbnailAsync(ThumbnailMode.SingleItem, size);
        }
        else
        {
            IconTmb = await file.GetThumbnailAsync(ThumbnailMode.SingleItem, size);
        }
        return IconTmb;
    }
}
