using NUnit.Framework;
using System.Runtime.Versioning;

namespace GACore.Extensions.Test;

[TestFixture]
[Category("ExtensionMethods")]
[SupportedOSPlatform("windows")]
public class TDouble_ExtensionMethods
{
    [TestCase(0, 1, 0)]
    [TestCase(0, 0.5, 0)]
    [TestCase(0.4, 0.5, 0.5)]
    [TestCase(1.2, 2, 2)]
    [TestCase(12, 5, 10)]
    [TestCase(14, 3, 15)]
    public void Quantize(double value, double quantizeStep, double expected)
    {
        double actual = value.Quantize(quantizeStep);
        Assert.That(expected, Is.EqualTo(actual));
    }

    [TestCase(0, 0)]
    [TestCase(Math.PI, Math.PI)]
    [TestCase(3 * Math.PI, Math.PI)]
    public void PiWrap(double value, double expected)
    {
        Assert.That(expected, Is.EqualTo(value.PiWrap()));
    }

    [Test]
    public void PiWrap_DoubleMinValue()
    {
        double value = double.MinValue;
        double wrapped = value.PiWrap();

        Assert.That(wrapped, Is.EqualTo(0).Within(Math.PI));
        Assert.That(wrapped, Is.EqualTo(0));
    }

    [Test]
    public void PiWrap_DoubleMaxValue()
    {
        double value = double.MaxValue;
        double wrapped = value.PiWrap();

        Assert.That(wrapped, Is.EqualTo(0).Within(Math.PI));
        Assert.That(wrapped, Is.EqualTo(0));
    }

    [TestCase(1e300)]
    [TestCase(-1e300)]
    public void PiWrap_LargeValues(double value)
    {
        double wrapped = value.PiWrap();

        Assert.That(wrapped, Is.EqualTo(0).Within(Math.PI));
    }

    [TestCase(1e10)]
    [TestCase(-1e10)]
    public void PiWrap_MidValues(double value)
    {
        double wrapped = value.PiWrap();

        Assert.That(wrapped, Is.EqualTo(0).Within(Math.PI));
    }
}