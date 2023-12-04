using GACore.Architecture;
using System;

namespace GACore;

public static class ResultFactory
{
    public static IResult FromException(Exception ex)
    {
        return Result.FromException(ex);
    }

    public static IResult<T> FromException<T>(Exception ex)
    {
        return Result<T>.FromException(ex);
    }

    public static IResult FromSuccess()
    {
        return Result.FromSuccess();
    }

    public static IResult<T> FromSuccess<T>(T value)
    {
        return Result<T>.FromSuccess(value);
    }

    public static IResult FromFailure(string failureReason)
    {
        return Result.FromFailure(failureReason);
    }

    public static IResult FromUnknownFailure()
    {
        return Result.FromFailure();
    }

    public static IResult FromFailure(Exception ex)
    {
        return Result.FromException(ex);
    }

    public static IResult<T> FromFailure<T>(string failureReason)
    {
        return Result<T>.FromFailure(failureReason);
    }

    public static IResult<T> FromUnknownFailure<T>()
    {
        return Result<T>.FromFailure();
    }

    public static IResult FromFailure(string format, object arg0)
    {
        return FromFailure(string.Format(format, arg0));
    }

    public static IResult FromFailure(string format, params object[] args)
    {
        return FromFailure(string.Format(format, args));
    }

    public static IResult<T> FromFailure<T>(string format, object arg0)
    {
        return FromFailure<T>(string.Format(format, arg0));
    }

    public static IResult<T> FromFailure<T>(string format, params object[] args)
    {
        return FromFailure<T>(string.Format(format, args));
    }

    public static IResult FromFailure<T>(Exception ex)
    {
        return Result.FromException(ex);
    }
}