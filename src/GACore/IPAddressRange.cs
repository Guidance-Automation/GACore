using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace GACore;

public struct IPAddressRange
{
	private readonly AddressFamily _addressFamily;

	private readonly byte[] _lowerBytes;

	private readonly byte[] _upperBytes;

	public IPAddressRange(string lowerIPV4string, string upperIPV4string)
		: this(IPAddress.Parse(lowerIPV4string), IPAddress.Parse(upperIPV4string))
	{
	}

	public override readonly int GetHashCode()
    {
        return HashCode.Combine(_addressFamily, _lowerBytes, _upperBytes);
    }

    public static bool operator !=(IPAddressRange rangeA, IPAddressRange rangeB)
    {
        return !rangeA.Equals(rangeB);
    }

    public static bool operator ==(IPAddressRange rangeA, IPAddressRange rangeB)
    {
        return rangeA.Equals(rangeB);
    }

    public override readonly bool Equals(object obj)
	{
		if (obj is not IPAddressRange) return false;

		IPAddressRange other = (IPAddressRange)obj;

		return _addressFamily == other._addressFamily
			&& _lowerBytes.SequenceEqual(other._lowerBytes)
			&& _upperBytes.SequenceEqual(other._upperBytes);
	}

	public IPAddressRange(IPAddress lower, IPAddress upper)
	{
		if (lower == null) throw new ArgumentOutOfRangeException(nameof(lower));
		if (upper == null) throw new ArgumentOutOfRangeException(nameof(upper));

		if (lower.AddressFamily != upper.AddressFamily) throw new InvalidOperationException("Inconsistent address families");

		_addressFamily = lower.AddressFamily;
		_lowerBytes = lower.GetAddressBytes();
		_upperBytes = upper.GetAddressBytes();
	}

    public readonly IPAddress Lower
    {
        get
        {
            return new IPAddress(_lowerBytes);
        }
    }

    public readonly IPAddress Upper
    {
        get
        {
            return new IPAddress(_upperBytes);
        }
    }

    public readonly string ToSummaryString()
    {
        return string.Format("IPAddressRange lower:{0}, upper:{1}", Lower, Upper);
    }

    public override readonly string ToString()
    {
        return ToSummaryString();
    }

    public readonly bool IsInRange(IPAddress ipaddress)
    {
        ArgumentNullException.ThrowIfNull(ipaddress);

        if (ipaddress.AddressFamily != _addressFamily) return false;

		byte[] addressBytes = ipaddress.GetAddressBytes();

		bool lowerBoundary = true;
		bool upperBoundary = true;

		for (int i = 0; i < _lowerBytes.Length && (lowerBoundary || upperBoundary); i++)
		{
			if ((lowerBoundary && addressBytes[i] < _lowerBytes[i]) || (upperBoundary && addressBytes[i] > _upperBytes[i])) return false;

			lowerBoundary &= (addressBytes[i] == _lowerBytes[i]);
			upperBoundary &= (addressBytes[i] == _upperBytes[i]);
		}

		return true;
	}
}