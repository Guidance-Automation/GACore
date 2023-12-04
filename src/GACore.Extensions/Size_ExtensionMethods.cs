using System;
using System.Windows;

namespace GACore.Extensions;

public static class Size_ExtensionMethods
{
	/// <summary>
	/// Returns a new size as if other was subtracted from size.
	/// I.e. A-B = A.Width - B.Width etc
	/// </summary>
	public static Size Subtract(this Size size, Size other)
	{
		return new Size(size.Width - other.Width, size.Height - other.Height);
	}

	/// <summary>
	/// Returns a size based on the max value of two other sizes. 
	/// E.g. max(Size(1,3),Size(4,2)) returns Size(4,3);
	/// </summary>
	/// <param name="size"></param>
	/// <param name="other"></param>
	/// <returns></returns>
	public static Size Max(this Size size, Size other)
	{
		return new Size(Math.Max(size.Width, other.Width), Math.Max(size.Height, other.Height));
	}

	/// <summary>
	/// Scales a size by a scalar value
	/// </summary>
	public static Size Scale(this Size size, double scale)
	{
            ArgumentOutOfRangeException.ThrowIfNegative(scale);
            return new Size(size.Width * scale, size.Height * scale);
	}
}