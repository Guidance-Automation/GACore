namespace GACore.Architecture;

/// <summary>
/// Lightweight structure to return a failure reason when an operation fails for traceability.
/// </summary>
public interface IResult
{
	public string FailureReason { get; }

    public bool IsSuccessful { get; }

    public Exception? Exception { get; }
}

/// <summary>
/// Expands IResult to include a result object
/// </summary>
/// <typeparam name="T">Default(T) if unsuccessful, cannot be null if successful.</typeparam>
public interface IResult<T> : IResult
{
    public T? Value { get; }
}