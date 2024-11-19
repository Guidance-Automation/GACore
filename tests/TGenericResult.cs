using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
[Description("Result object")]
public class TGenericResult
{
    [TestCase(69)]
    public void Success<T>(T instance)
    {
        Result<T> result = Result<T>.FromSuccess(instance);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful);
            Assert.That(instance, Is.EqualTo(result.Value));
            Assert.That(string.Equals(string.Empty, result.FailureReason, StringComparison.OrdinalIgnoreCase));
        });
    }

    [TestCase(66)]
    [TestCase("Horse")]
    public void FromException<T>(T _)
    {
        string message = "OHES NOES";
        Exception ex = new(message);

        Result<T> result = Result<T>.FromException(ex);

        Assert.Multiple(() =>
        {
            Assert.That(!result.IsSuccessful);
            Assert.That(string.Equals(result.FailureReason, message, StringComparison.OrdinalIgnoreCase));
            Assert.That(default(T), Is.EqualTo(result.Value));
        });
    }
}