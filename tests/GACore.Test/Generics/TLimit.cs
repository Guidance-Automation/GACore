using GACore.Generics;
using NUnit.Framework;

namespace GACore.Test.Generics;

[TestFixture]
[Category("Generics")]
public class TLimit
{
    [Test]
    public void Parameterless()
    {
        Limit<double> limit = new();

        Assert.That(limit.Minimum, Is.EqualTo(0));
        Assert.That(limit.Maximum, Is.EqualTo(0));

        limit.Maximum = 100;
        Assert.That(limit.Minimum, Is.EqualTo(0));
        Assert.That(limit.Maximum, Is.EqualTo(100));
    }

    [Test]
    [TestCase(0, 1)]
    public void Limit_Init(int min, int max)
    {
        Limit<int> limit = new(min, max);

        Assert.That(limit.Minimum, Is.EqualTo(min));
        Assert.That(limit.Maximum, Is.EqualTo(max));
    }

    [Test]
    public void Limit_Set_Maximum()
    {
        int maximum = 0;
        Limit<int> limit = new(int.MinValue, maximum);
        Assert.That(limit.Minimum, Is.EqualTo(int.MinValue));
        Assert.That(limit.Maximum, Is.EqualTo(maximum));

        maximum = int.MaxValue;
        limit.Maximum = maximum;
        Assert.That(limit.Minimum, Is.EqualTo(int.MinValue));
        Assert.That(limit.Maximum, Is.EqualTo(maximum));
    }

    [Test]
    public void Limit_Set_Minimum()
    {
        int minimum = int.MinValue;
        Limit<int> limit = new(minimum, int.MaxValue);
        Assert.That(limit.Minimum, Is.EqualTo(minimum));
        Assert.That(limit.Maximum, Is.EqualTo(int.MaxValue));

        minimum = 0;
        limit.Minimum = minimum;
        Assert.That(limit.Minimum, Is.EqualTo(minimum));
        Assert.That(limit.Maximum, Is.EqualTo(int.MaxValue));
    }
}