using NUnit.Framework;
using System.Net;
using System.Runtime.Versioning;

namespace GACore.Extensions.Test;

[TestFixture]
[Category("ExtensionMethods")]
[SupportedOSPlatform("windows")]
public class TIPAddress_ExtensionMethods
{
	[Test]
	[TestCase("0.0.0.0", true)]
	[TestCase("192.168.0.1", true)]
	[TestCase("255.255.255.255", true)]
	[TestCase("203.0.113.64", true)]
	[TestCase("192.100.0.69", false)]
	public void IsReserved(string ipV4string, bool isReserved)
	{
		IPAddress address = IPAddress.Parse(ipV4string);
		Assert.That(isReserved, Is.EqualTo(address.IsReserved()));
	}
}