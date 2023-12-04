using System;
using System.Windows;

namespace GACore.Extensions;

public static class Vector_ExtensionMethods
{
	public static double ToHeadingRad(this Vector vector)
	{
		if (vector.Length == 0) throw new ArgumentOutOfRangeException("vector.Length == 0");

		Vector normVec = vector;
		normVec.Normalize();
		double theta = Math.Atan(Math.Abs(normVec.Y) / Math.Abs(normVec.X)); // Theta in radians

		bool cPos = normVec.X >= 0;
		bool sPos = normVec.Y >= 0;

		// For Sin    // For Cos
		// + | +      // - | +
		// -----      // -----
		// - | -      // - | +

		double headingRad;

		if (sPos && cPos) headingRad = theta; // First quadradnt
		else if (sPos && !cPos) headingRad = Math.PI - theta; // Second quadrant
		else if (!sPos && !cPos) headingRad = Math.PI + theta; // Third quadrant
		else headingRad = -theta; // Fourth quadrant

		return headingRad.PiWrap();
	}
}