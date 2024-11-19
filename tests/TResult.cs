using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
[Description("Result object")]
public class TResult
{
    [Test]
    public void Success()
    {
        Result result = Result.FromSuccess();
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful);
            Assert.That(string.Equals(string.Empty, result.FailureReason, StringComparison.OrdinalIgnoreCase));
        });
    }

    [Test]
    public void Failure_Unspecified()
    {
        Result result = Result.FromFailure();
        Assert.Multiple(() =>
        {
            Assert.That(!result.IsSuccessful);
            Assert.That(string.Equals("Unknown", result.FailureReason, StringComparison.OrdinalIgnoreCase));
        });
    }

    [Test]
    public void FromException()
    {
        string message = "OHES NOES";
        Exception ex = new(message);

        Result result = Result.FromException(ex);
        Assert.Multiple(() =>
        {
            Assert.That(!result.IsSuccessful);
            Assert.That(string.Equals(message, result.FailureReason, StringComparison.OrdinalIgnoreCase));
        });
    }
}