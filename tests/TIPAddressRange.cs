using NUnit.Framework;
using System.Net;

namespace GACore.Test;

[TestFixture]
[Category("GACore")]
[Description("IPAddressRange")]
public class TIPAddressRange
{
    [Test]
    [TestCase("192.168.4.9", false)]
    [TestCase("192.168.4.10", true)]
    [TestCase("192.168.4.15", true)]
    [TestCase("192.168.4.20", true)]
    [TestCase("192.168.4.21", false)]
    public void InRange(string ipV4string, bool isExpectedInRange)
    {
        IPAddress lower = IPAddress.Parse("192.168.4.10");
        IPAddress upper = IPAddress.Parse("192.168.4.20");

        IPAddressRange range = new(lower, upper);

        IPAddress testCase = IPAddress.Parse(ipV4string);

        Assert.That(isExpectedInRange, Is.EqualTo(range.IsInRange(testCase)));
    }

    [Test]
    public void AreNotEqual()
    {
        IPAddress lowerA = IPAddress.Parse("192.168.4.10");
        IPAddress upperA = IPAddress.Parse("192.168.4.20");

        IPAddress lowerB = IPAddress.Parse("192.168.5.10");
        IPAddress upperB = IPAddress.Parse("192.168.5.20");

        IPAddressRange rangeA = new(lowerA, upperA);
        IPAddressRange rangeB = new(lowerB, upperB);

        Assert.That(rangeA, Is.Not.EqualTo(rangeB));
    }

    [Test]
    public void AreEqual()
    {
        IPAddress lower = IPAddress.Parse("192.168.4.10");
        IPAddress upper = IPAddress.Parse("192.168.4.20");

        IPAddressRange range = new(lower, upper);
        IPAddressRange rangeCopy = new(lower, upper);

        Assert.That(range, Is.EqualTo(rangeCopy));
    }
}