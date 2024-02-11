using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
[Category("Maths")]
public class TMaths
{
    [Test]
    [TestCase(0, 1, 2, new double[] { 0, 1 })]
    [TestCase(1, 0, 2, new double[] { 1, 0 })]
    [TestCase(0, 1, 1, new double[] { 1 })]
    [TestCase(1, 0, 1, new double[] { 0 })]
    [TestCase(1, 0, 0, new double[] { })]
    public void LinSpace_Create(double x, double y, int n, double[] expected)
    {
        Assert.That(expected, Is.EqualTo(Maths.LinSpace(x, y, n)));
    }

    [Test]
    [TestCase(0, 0, true)]
    [TestCase(3, 3, true)]
    [TestCase(3, 3.01, true)]
    [TestCase(0, 3.2, false)]
    [TestCase(3.01, 3, true)]
    [TestCase(1.57, 0, false)]
    [TestCase(0, 1.57, false)]
    [TestCase(1.5707963267948966, 0, false)]
    [TestCase(0, 1.5707963267948966, false)]
    [TestCase(Math.PI, -Math.PI, true)]
    [TestCase(-Math.PI, Math.PI, true)]
    public void AreWithinRadTol(double radA, double radB, bool expected)
    {
        Assert.That(expected, Is.EqualTo(Maths.AreWithinRadTol(radA, radB)));
    }
}