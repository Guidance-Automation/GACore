using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;

namespace GACore.Extensions;

[SupportedOSPlatform("windows")]
public static class IPAddress_ExtensionMethods
{
	public static bool IsPrivateNetwork(this IPAddress ipAddress)
	{
        ArgumentNullException.ThrowIfNull(ipAddress);

        return ReservedIPV4PrivateNetworkRanges.Any(e => e.IsInRange(ipAddress));
	}

	public static bool IsReserved(this IPAddress ipAddress)
	{
        ArgumentNullException.ThrowIfNull(ipAddress);

        return ReservedIPV4Ranges.Any(e => e.IsInRange(ipAddress));
	}

	private static readonly HashSet<IPAddressRange> _reservedIPV4PrivateNetworkRanges =
    [
        new IPAddressRange("10.0.0.0", "10.255.255.255"),
		new IPAddressRange("100.64.0.0", "100.127.255.255"),
		new IPAddressRange("172.16.0.0", "172.31.255.255"),
		new IPAddressRange("192.0.0.0", "192.0.0.255"),
		new IPAddressRange("192.168.0.0", "192.168.255.255"),
		new IPAddressRange("198.18.0.0", "198.19.255.255")
	];

    public static IEnumerable<IPAddressRange> ReservedIPV4PrivateNetworkRanges
    {
        get
        {
            return _reservedIPV4PrivateNetworkRanges;
        }
    }

    public static IEnumerable<IPAddressRange> ReservedIPV4Ranges
    {
        get
        {
            return _reservedIPV4Ranges;
        }
    }

    private static readonly HashSet<IPAddressRange> _reservedIPV4Ranges =
    [
        new IPAddressRange("0.0.0.0", "0.255.255.255"),
		new IPAddressRange("127.0.0.0", "127.255.255.255"),
		new IPAddressRange("169.254.0.0", "169.254.255.255"),
		new IPAddressRange("192.0.2.0", "192.0.2.255"),
		new IPAddressRange("192.88.99.0", "192.88.99.255"),
		new IPAddressRange("198.51.100.0", "198.51.100.255"),
		new IPAddressRange("203.0.113.0", "203.0.113.255"),
		new IPAddressRange("224.0.0.0", "239.255.255.255"),
		new IPAddressRange("240.0.0.0", "255.255.255.254"),
		new IPAddressRange("255.255.255.255", "255.255.255.255"),
		new IPAddressRange("10.0.0.0", "10.255.255.255"),
		new IPAddressRange("100.64.0.0", "100.127.255.255"),
		new IPAddressRange("172.16.0.0", "172.31.255.255"),
		new IPAddressRange("192.0.0.0", "192.0.0.255"),
		new IPAddressRange("192.168.0.0", "192.168.255.255"),
		new IPAddressRange("198.18.0.0", "198.19.255.255")
	];
}