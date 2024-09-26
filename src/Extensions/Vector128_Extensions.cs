using System.Runtime.Intrinsics;

namespace GACore.Extensions;
public static class Vector128_Extensions
{
    public static double LengthSquared(this Vector128<double> vector) => Dot(vector, vector);

    public static double Length(this Vector128<double> vector) => Math.Sqrt(Dot(vector, vector));

    public static Vector128<double> Normalize(this Vector128<double> value)
    {
        Vector128<double> norm = value / value.Length();

        // the vector division is not as safe as division with the old Rect type
        // we assume if we end up with double.NaN then one of the values was 0.
        if (double.IsNaN(norm[0]))
        {
            norm = Vector128.Create(0, norm[1]);
        }
        if (double.IsNaN(norm[1]))
        {
            norm = Vector128.Create(norm[0], 0);
        }
        return norm;
    }

    public static double ToHeadingRad(this Vector128<double> vector)
    {
        if (vector.Length() == 0) throw new ArgumentOutOfRangeException("vector.Length == 0");

        vector = vector.Normalize();

        double theta = Math.Atan(Math.Abs(vector[1]) / Math.Abs(vector[0])); // Theta in radians

        bool cPos = vector[0] >= 0;
        bool sPos = vector[1] >= 0;

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

    private static double Dot(Vector128<double> value1, Vector128<double> value2) => (value1[0] * value2[0])
             + (value1[1] * value2[1]);

    public static Vector128<double> GetVectorTo(this Vector128<double> current, Vector128<double> point) =>
        Vector128.Create(point[0] - current[0], point[1] - current[1]);

    public static Vector128<double> GetPointAt(this Vector128<double> point, Vector128<double> vector) =>
        Vector128.Create(point[0] + vector[0], point[1] + vector[1]);

    public static double GetLengthTo(this Vector128<double> current, Vector128<double> point) =>
        current.GetVectorTo(point).Length();

    public static Vector128<double> Offset(this Vector128<double> current, double offsetX, double offsetY) =>
        Vector128.Create(current[0] + offsetX, current[1] + offsetY);

    public static Vector128<double> Scale(this Vector128<double> current, double scaleFactor) =>
        Vector128.Create(current[0] * scaleFactor, current[1] * scaleFactor);

    public static Vector128<double> ToCenterpoint(this Rectangle rect) =>
        Vector128.Create(rect.Left + rect.Width / 2.0, rect.Top + rect.Height / 2.0);

    public static Rectangle ToBoundingBox(this IEnumerable<Vector128<double>> points)
    {
        Rectangle rect = Rectangle.Empty;
        foreach (Vector128<double> point in points)
        {
            rect.Union(point);
        }
        return rect;
    }

    /// <summary>
    /// Rotate a vector by an angle.
    /// </summary>
    /// <param name="vector">Vector to be rotated.</param>
    /// <param name="angle">Angle in radians.</param>
    public static Vector128<double> Rotate(this Vector128<double> vector, double angle)
    {
        double xNew = (vector[0] * Math.Cos(angle)) - (vector[1] * Math.Sin(angle));
        double yNew = (vector[0] * Math.Sin(angle)) + (vector[1] * Math.Cos(angle));

        return Vector128.Create(xNew, yNew);
    }
}