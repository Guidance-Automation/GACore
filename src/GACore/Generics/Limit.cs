namespace GACore.Generics;

public class Limit<T> where T : IComparable
{
    private readonly object _lockObject = new();

    private T? _maximum;

    private T? _minimum;

    /// <summary>
    /// Limit object, allowing clear definition of the minimum and maximum.
    /// </summary>
    /// <param name="min">instance of object defining the minimum limit.</param>
    /// <param name="max">instance of object defining the maximum limit.</param>
    public Limit(T? minimum, T? maximum)
    {
        SanityCheck(minimum, maximum);

        _minimum = minimum;
        _maximum = maximum;
    }

    public Limit() : this(default, default)
    {
    }

    public T? Maximum
    {
        get { return _maximum; }
        set
        {
            lock (_lockObject)
            {
                SanityCheck(_minimum, value);
                _maximum = value;
            }
        }
    }

    public T? Minimum
    {
        get { return _minimum; }
        set
        {
            lock (_lockObject)
            {
                SanityCheck(value, _maximum);
                _minimum = value;
            }
        }
    }

    private static void SanityCheck(T? minimum, T? maximum)
    {
        if (minimum?.CompareTo(maximum) > 0)
        {
            throw new InvalidOperationException("Minimum value cannot be greater than maximum value.");
        }
    }
}