using System;

namespace GACore;

public class Maths
{
    /// <summary>
    /// Threshold for if two radian values are considered equal.
    /// </summary>
    public static double RadTol { get; } = 2 * Math.PI / 180;

    /// <summary>
    /// True if two radian values are within threshold to be considered equal.
    /// </summary>
    public static bool AreWithinRadTol(double aRad, double bRad)
    {
        double headingDelta = Trigonometry.MinAngleRad(aRad, bRad);
        return Math.Abs(headingDelta) <= RadTol;
    }

    /// <summary>
    /// Creates array of linearly spaced elements.
    /// </summary>
    public static double[] LinSpace(double x, double y, int n = 100)
    {
        if (n <= 0) return [];
        else if (n == 1) return [y];
        else
        {
            double[] linArray = new double[n];
            double stepSize = (y - x) / (n - 1);

            for (int i = 0; i < n; i++)
            {
                linArray[i] = x + (i * stepSize);
            }

            linArray[0] = x;
            linArray[n - 1] = y;

            return linArray;
        }
    }
}