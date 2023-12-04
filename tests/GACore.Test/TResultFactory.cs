using GACore.Architecture;
using NUnit.Framework;
using System;

namespace GACore.Test;

[TestFixture]
[Description("Result factory")]
public class TResultFactory
{
    [Test]
    public void StringFormatting()
    {
        int[] args = [1, 2, 3];
        string expected = string.Format("ohes noes: {0}, {1}, {2}", args[0], args[1], args[2]);

        IResult<int> result = ResultFactory.FromFailure<int>("ohes noes: {0}, {1}, {2}", args[0], args[1], args[2]);

        Assert.That(string.Equals(expected, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public void ResultFactoryUnkownFailure()
    {
        IResult result = ResultFactory.FromUnknownFailure();

        Assert.That(!result.IsSuccessful);
        Assert.That(string.Equals("unknown", result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public void ResultFactoryGenericUnkownFailure()
    {
        IResult<int> result = ResultFactory.FromUnknownFailure<int>();

        Assert.That(!result.IsSuccessful);
        Assert.That(default(int), Is.EqualTo(result.Value));
        Assert.That(string.Equals("unknown", result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    [TestCase("epic fails")]
    public void ResultFactoryFailure(string failureReason)
    {
        IResult result = ResultFactory.FromFailure(failureReason);

        Assert.That(!result.IsSuccessful);
        Assert.That(string.Equals(failureReason, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    [TestCase("epic fails")]
    public void ResultFactoryGenericFailure(string failureReason)
    {
        IResult<int> result = ResultFactory.FromFailure<int>(failureReason);

        Assert.That(!result.IsSuccessful);
        Assert.That(default(int), Is.EqualTo(result.Value));
        Assert.That(string.Equals(failureReason, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public void ResultFactorySuccess()
    {
        IResult result = ResultFactory.FromSuccess();

        Assert.That(result.IsSuccessful);
        Assert.That(string.Equals(string.Empty, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public void ResultFactoryGenericSuccess()
    {
        IResult<int> result = ResultFactory.FromSuccess(69);

        Assert.That(result.IsSuccessful);
        Assert.That(result.Value, Is.EqualTo(69));
        Assert.That(string.Equals(string.Empty, result.FailureReason, StringComparison.OrdinalIgnoreCase));
    }
}