using System.Runtime.Intrinsics;

namespace GACore.Extensions;

public static class Double_ExtensionMethods
{
    public static double DegToRad(this double value)
    {
        return value * Math.PI / 180.0d;
    }

    public static double RadToDeg(this double value)
    {
        return value * 180.0d / Math.PI;
    }

    public static double Quantize(this double value, double quantizeStep)
    {
        if (quantizeStep <= 0) return 0;

        double multiple = value * (1 / quantizeStep);
        double rounded = Math.Round(multiple, MidpointRounding.AwayFromZero);
        return rounded / (1 / quantizeStep);
    }

    /// <summary>
    /// Converts an angular vector to a vector.
    /// </summary>
    /// <param name="angle">In radians.</param>
    /// <param name="length">Vector length.</param>
    public static Vector128<double> AngleToVector(this double angle, double length = 1)
    {
        return Vector128.Create(angle.ToXComponent(length), angle.ToYComponent(length));
    }

    /// <summary>
    /// Gets the x component of an angular vector.
    /// </summary>
    /// <param name="angle">In radians.</param>
    /// <param name="length">Vector length.</param>
    public static double ToXComponent(this double angle, double length = 1)
    {
        return Math.Cos(angle) * length;
    }

    /// <summary>
    /// Gets the y component of an angular vector.
    /// </summary>
    /// <param name="angle">In radians.</param>
    /// <param name="length">Vector length.</param>
    public static double ToYComponent(this double angle, double length = 1)
    {
        return Math.Sin(angle) * length;
    }

    /// <summary>
    /// Compare to doubles within a given precision.
    /// </summary>
    /// <param name="refDouble">Reference double.</param>
    /// <param name="testDouble">Test double we are comparing against.</param>
    /// <param name="precision">Decimal precision to compare to (default 10 DP).</param>
    /// <returns>Boolean on outcome.</returns>
    public static bool EqualsToPrecision(this double refDouble, double testDouble, int precision = 10)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(precision);

        refDouble = Math.Round(refDouble, precision);
        testDouble = Math.Round(testDouble, precision);
        return refDouble.Equals(testDouble);
    }

    /// <summary>
    /// Wraps into range [-PI, PI]
    /// </summary>
    public static double PiWrap(this double angle)
    {
        return Trigonometry.PiWrap(angle);
    }
}