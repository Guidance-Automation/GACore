using GACore.Architecture;
using NUnit.Framework;
using System;

namespace GACore.Test;

[TestFixture]
[Description("Result object")]
public class TGenericResult
{
    [TestCase(69)]
    public void Success<T>(T instance)
    {
        IResult<T> result = Result<T>.FromSuccess(instance);

        Assert.That(result.IsSuccessful);
        Assert.That(instance, Is.EqualTo(result.Value));
        Assert.That(string.Equals(string.Empty, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [TestCase(66)]
    [TestCase("Horse")]
    public void FromException<T>(T _)
    {
        string message = "OHES NOES";
        Exception ex = new(message);

        IResult<T> result = Result<T>.FromException(ex);

        Assert.That(!result.IsSuccessful);
        Assert.That(string.Equals(result.FailureReason, message, StringComparison.OrdinalIgnoreCase));

        Assert.That(default(T), Is.EqualTo(result.Value));
    }
}