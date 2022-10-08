namespace SharpCore;

public class DatetimeHelper
{
    /// <summary>
    /// Get the current Unix time in milliseconds
    /// </summary>
    /// <returns>Unix time in milliseconds</returns>
    public static long GetCurrentUnixTime()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        return now.ToUnixTimeMilliseconds();
    }
}
