namespace SharpCore;

public static class ListExtensions
{
    /// <summary>
    /// Calculate element-wise difference between list or array values
    /// </summary>
    /// <param name="data">List or array to evaluate difference of</param>
    /// <returns>Enumerable containing the different scores</returns>
    public static IEnumerable<int> Diff(this IEnumerable<int> data)
    {
        return data.Zip(data.Skip(1), (a, b) => Math.Abs(b - a));
    }

    public static IEnumerable<long> Diff(this IEnumerable<long> data)
    {
        return data.Zip(data.Skip(1), (a, b) => Math.Abs(b - a));
    }

    public static IEnumerable<decimal> Diff(this IEnumerable<decimal> data)
    {
        return data.Zip(data.Skip(1), (a, b) => Math.Abs(b - a));
    }

    public static IEnumerable<double> Diff(this IEnumerable<double> data)
    {
        return data.Zip(data.Skip(1), (a, b) => Math.Abs(b - a));
    }

    public static IEnumerable<float> Diff(this IEnumerable<float> data)
    {
        return data.Zip(data.Skip(1), (a, b) => Math.Abs(b - a));
    }
}
