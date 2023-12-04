using System.Net;
using System.Runtime.Versioning;
using GACore.Architecture;

namespace GACore.DemoApp;

[SupportedOSPlatform("windows")]
public class FooIPAddressable : IIPAddressable
{
	public IPAddress? IPAddress { get; set; } = IPAddress.Loopback;

	public void Randomize()
	{
		byte[] bytes = new byte[4];
		Tools.Random.NextBytes(bytes);

		IPAddress = new IPAddress(bytes);
	}
}