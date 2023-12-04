using System;
using System.Text;

namespace GACore.Extensions;

public static class Byte_ExtensionMethods
{
    public static bool IsCurrentByteTickLarger(this byte current, byte other)
    {
        return (current < other && (other - current) > 128) || (current > other && (current - other) < 128);
    }

    public static string ToBitString(this byte value)
    {
        return Convert.ToString(value, 2).PadLeft(8, '0');
    }

    public static string ToHexString(this byte[] bytes)
	{
		if (bytes == null) return string.Empty;

		StringBuilder builder = new();

        foreach(byte b in bytes)
        {
            builder.AppendFormat(" {0}", b.ToHexString());
        }

		return builder.ToString();
	}

    public static string ToHexString(this byte value)
    {
        return string.Format("{0:x2}", value);
    }

    public static byte ToHighNybble(this byte value)
    {
        return (byte)(value & 0x0F);
    }

    public static byte ToLowNybble(this byte value)
    {
        return (byte)((value & 0xFF) >> 4);
    }
}