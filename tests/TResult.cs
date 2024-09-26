using GACore.Architecture;
using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
[Description("Result object")]
public class TResult
{
    [Test]
    public void Success()
    {
        IResult result = Result.FromSuccess();

        Assert.That(result.IsSuccessful);
        Assert.That(string.Equals(string.Empty, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public void Failure_Unspecified()
    {
        IResult result = Result.FromFailure();

        Assert.That(!result.IsSuccessful);
        Assert.That(string.Equals("Unknown", result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public void FromException()
    {
        string message = "OHES NOES";
        Exception ex = new(message);

        IResult result = Result.FromException(ex);

        Assert.That(!result.IsSuccessful);
        Assert.That(string.Equals(message, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }
}