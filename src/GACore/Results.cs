using GACore.Architecture;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GACore.Test")]
namespace GACore;

public class Result : IResult
{
    public Exception? Exception { get; }

    public string FailureReason { get; } = string.Empty;

    public bool IsSuccessful { get; } = false;

    public static Result FromSuccess()
    {
        return new Result();
    }

    public static Result FromFailure(string? failureReason = default)
    {
        return new Result(failureReason);
    }

    public static Result FromException(Exception exception)
    {
        return new Result(exception);
    }

    protected Result()
    {
        IsSuccessful = true;
        FailureReason = string.Empty;
        Exception = null;
    }

    protected Result(string? failureReason)
    {
        if (string.IsNullOrEmpty(failureReason))
            failureReason = "Unknown";

        IsSuccessful = false;
        FailureReason = failureReason;
        Exception = null;
    }

    protected Result(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        IsSuccessful = false;
        FailureReason = exception.Message;
        Exception = exception;
    }

    public virtual string ToResultString()
    {
        return IsSuccessful ? "Success"
                : string.Format("Failed: {0}", FailureReason);
    }

    public override string ToString()
    {
        return ToResultString();
    }
}

public class Result<T> : Result, IResult<T>
{
    public T? Value { get; } = default;

    public static Result<T> FromSuccess(T value)
    {
        return new Result<T>(value);
    }

    public static new Result<T> FromFailure(string? failureReason = default)
    {
        return new Result<T>(failureReason);
    }

    public static new Result<T> FromException(Exception exception)
    {
        return new Result<T>(exception);
    }

    protected Result(T value)
        : base()
    {
        Value = value;
    }

    protected Result(string? failureReason)
        : base(failureReason)
    {
        Value = default;
    }

    protected Result(Exception exception)
        : base(exception)
    {
        Value = default;
    }

    public override string ToResultString()
    {
        return IsSuccessful ? string.Format("Success: {0}", Value)
        : string.Format("Failed: {0}", FailureReason);
    }
}