using System.Net;

namespace GACore.Architecture;

public interface IIPAddressable
{
	public IPAddress? IPAddress { get; set; }
}