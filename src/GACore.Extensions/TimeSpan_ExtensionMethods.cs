namespace GACore.Extensions;

public static class TimeSpan_ExtensionMethods
{
    /// <summary>
    /// Returns total duration of a sequence of timespans.
    /// </summary>
    public static TimeSpan Sum(this IEnumerable<TimeSpan> timespans)
    {
        return timespans.Aggregate(TimeSpan.Zero, (result, value) => result.Add(value));
    }
}