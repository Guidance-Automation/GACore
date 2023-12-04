using NUnit.Framework;
using System.Runtime.Versioning;
using System.Windows;

namespace GACore.Extensions.Test;

[TestFixture]
[SupportedOSPlatform("windows")]
public class TSize_ExtensionMethods
{
    [Test]
    public void Subtract()
    {
        Size tenTwenty = new(10, 20);
        Size threeSix = new(3, 6);

        Size result = tenTwenty.Subtract(threeSix);

        Assert.That(result.Width, Is.EqualTo(10 - 3));
        Assert.That(result.Height, Is.EqualTo(20 - 6));
    }

    [Test]
    public void Max()
    {
        Size threeFive = new(3, 5);
        Size fourTwo = new(4, 2);

        Size max = threeFive.Max(fourTwo);

        Assert.That(max.Width, Is.EqualTo(4));
        Assert.That(max.Height, Is.EqualTo(5));
    }

    [Test]
    public void Scale()
    {
        Size tenTwenty = new(10, 20);
        Size scaled = tenTwenty.Scale(0.5);

        Assert.That(scaled.Width, Is.EqualTo(5));
        Assert.That(scaled.Height, Is.EqualTo(10));
    }

    [Test]
    public void SubtractNegative()
    {
        Size threeFour = new(3, 4);
        Size tenTwenty = new(10, 20);

        Assert.Throws<ArgumentException>(() =>
        {
            threeFour.Subtract(tenTwenty);
        });
    }
}