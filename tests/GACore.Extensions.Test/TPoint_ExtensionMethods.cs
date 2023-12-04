using NUnit.Framework;
using System;
using System.Runtime.Versioning;
using System.Windows;

namespace GACore.Extensions.Test;

[TestFixture]
[Category("ExtensionMethods")]
[SupportedOSPlatform("windows")]
public class TPoint_ExtensionMethods
{
    [Test]
    public void Quantize_NaN()
    {
        Point point = new(double.NaN, 2.5);
        Point quantized = point.Quantize(2);

        Assert.That(double.NaN, Is.EqualTo(quantized.X));
        Assert.That(2, Is.EqualTo(quantized.Y));
    }

    [Test]
    public void QuantizePI()
    {
        Point point = new(Math.PI, Math.PI);

        Point quantized = point.Quantize(0.001);

        Assert.That(3.142, Is.EqualTo(quantized.X));
        Assert.That(3.142, Is.EqualTo(quantized.Y));
    }

    [Test]
    public void Quantize()
    {
        Point point = new(1.6, 2.7);
        Point quantized = point.Quantize(2);

        Assert.That(2, Is.EqualTo(quantized.X));
        Assert.That(2, Is.EqualTo(quantized.Y));
    }

    [Test]
    [TestCase(0, 0, 1, 1)]
    public void GetVectorTo(double x1, double y1, double x2, double y2)
    {
        Point source = new(x1, y1);
        Point destination = new(x2, y2);

        Vector expected = new(x2 - x1, y2 - y1);
        Vector actual = source.GetVectorTo(destination);

        Assert.That(expected, Is.EqualTo(actual));
    }
}