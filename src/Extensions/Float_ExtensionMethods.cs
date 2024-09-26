using System.Runtime.Intrinsics;

namespace GACore.Extensions;

public static class Float_ExtensionMethods
{
    private const float _pi = (float)Math.PI;
    public static float DegToRad(this float value)
    {
        return value * _pi / 180.0f;
    }

    public static float RadToDeg(this float value)
    {
        return value * 180.0f / _pi;
    }

    public static float Quantize(this float value, float quantizeStep)
    {
        if (quantizeStep <= 0) return 0;

        float multiple = value * (1 / quantizeStep);
        double rounded = Math.Round(multiple, MidpointRounding.AwayFromZero);
        return (float)(rounded / (1 / quantizeStep));
    }

    /// <summary>
    /// Converts an angular vector to a vector.
    /// </summary>
    /// <param name="angle">In radians.</param>
    /// <param name="length">Vector length.</param>
    public static Vector128<double> AngleToVector(this float angle, float length = 1)
    {
        return Vector128.Create(angle.ToXComponent(length), angle.ToYComponent(length));
    }

    /// <summary>
    /// Gets the x component of an angular vector.
    /// </summary>
    /// <param name="angle">In radians.</param>
    /// <param name="length">Vector length.</param>
    public static float ToXComponent(this float angle, float length = 1)
    {
        return (float)(Math.Cos(angle) * length);
    }

    /// <summary>
    /// Gets the y component of an angular vector.
    /// </summary>
    /// <param name="angle">In radians.</param>
    /// <param name="length">Vector length.</param>
    public static float ToYComponent(this float angle, float length = 1)
    {
        return (float)(Math.Sin(angle) * length);
    }

    /// <summary>
    /// Compare to doubles within a given precision.
    /// </summary>
    /// <param name="refFloat">Reference double.</param>
    /// <param name="testFloat">Test double we are comparing against.</param>
    /// <param name="precision">Decimal precision to compare to (default 10 DP).</param>
    /// <returns>Boolean on outcome.</returns>
    public static bool EqualsToPrecision(this float refFloat, float testFloat, int precision = 10)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(precision);

        refFloat = (float)Math.Round(refFloat, precision);
        testFloat = (float)Math.Round(testFloat, precision);
        return refFloat.Equals(testFloat);
    }

    /// <summary>
    /// Wraps into range [-PI, PI]
    /// </summary>
    public static float PiWrap(this float angle)
    {
        return (float)Trigonometry.PiWrap(angle);
    }
}