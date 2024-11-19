using GACore.Extensions;
using NUnit.Framework;
using System.Runtime.Versioning;

namespace GACore.Test.Extensions;

[TestFixture]
[Category("ExtensionMethods")]
[SupportedOSPlatform("windows")]
public class TGeneric_ExtensionMethods
{
    [Test]
    [TestCase(1, -1, 0, 0)]
    public void Clamp(double value, double min, double max, double expected)
    {
        double actual = value.Clamp(min, max);
        Assert.That(expected, Is.EqualTo(actual));
    }
}