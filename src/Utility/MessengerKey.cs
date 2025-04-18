namespace GACore.Utility;

/// <summary>
/// Initializes a new instance of the MessengerKey class.
/// </summary>
/// <param name="recipient"></param>
/// <param name="context"></param>
internal class MessengerKey(object recipient, object? context)
{
    public object Recipient { get; private set; } = recipient;

    public object? Context { get; private set; } = context;

    /// <summary>
    /// Determines whether the specified MessengerKey is equal to the current MessengerKey.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    protected bool Equals(MessengerKey other)
    {
        return Equals(Recipient, other.Recipient) && Equals(Context, other.Context);
    }

    /// <summary>
    /// Determines whether the specified MessengerKey is equal to the current MessengerKey.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((MessengerKey)obj);
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Recipient, Context);
    }
}