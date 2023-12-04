using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
[Category("Trigonometry")]
public class TTrigonometry
{
    private static readonly double _tol = 1e-3;

    [Test]
    [TestCase(0, 0)]
    [TestCase(Math.PI, Math.PI)]
    [TestCase(3 * Math.PI, Math.PI)]
    public void PiWrap(double value, double expected)
    {
        Assert.That(expected, Is.EqualTo(Trigonometry.PiWrap(value)));
    }

    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, Math.PI / 2, Math.PI / 2)]
    [TestCase(0, -Math.PI / 2, -Math.PI / 2)]
    public void MinAngleRad(double angleA, double angleB, double expected)
    {
        double actual = Trigonometry.MinAngleRad(angleA, angleB);
        Assert.That(expected, Is.EqualTo(actual).Within(_tol));
    }
}