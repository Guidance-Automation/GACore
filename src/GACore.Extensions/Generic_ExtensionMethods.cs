namespace GACore.Extensions;

public static class Generic_ExtensionMethods
{
	/// <summary>
	/// Clamps a value into the range min, max.
	/// </summary>
	public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
	{
		if (min.CompareTo(max) > 0) throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be greater than maximum value.");

		if (val.CompareTo(min) < 0) return min;
		else if (val.CompareTo(max) > 0) return max;
		else return val;
	}
}