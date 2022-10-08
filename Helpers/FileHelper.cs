namespace SharpCore;

public class FileHelper
{
    /// <summary>
    /// Get the path to a file in a local directory, relative to the application root
    /// </summary>
    /// <param name="directory">Name of the directory containing the target file</param>
    /// <param name="filename">Name of the file with extension</param>
    /// <returns>Full path to the file</returns>
    public static string GetPathToLocalFile(string directory, string filename)
    {
        return Path.Combine(AppContext.BaseDirectory, directory, filename);
    }
}
