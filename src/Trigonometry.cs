namespace GACore;

public static class Trigonometry
{
    /// <summary>
    /// Minimum angle in radians between two angles
    /// </summary>
    public static double MinAngleRad(double aRad, double bRad)
    {
        aRad = PiWrap(aRad);
        bRad = PiWrap(bRad);

        double theta = Math.Abs(aRad - bRad) % (2 * Math.PI);

        if (theta > Math.PI) theta = (2 * Math.PI) - theta;

        if (Math.Abs(PiWrap(aRad + theta - bRad)) > Math.Abs(PiWrap(aRad - theta - bRad))) theta = -theta;

        return theta;
    }

    /// <summary>
    /// Wraps into range [-PI, PI]
    /// </summary>
    public static double PiWrap(double angle)
    {
        if (double.IsNaN(angle))
            return double.NaN;

        double numRevs = Math.Round(angle / (2.0d * Math.PI));

        angle -= numRevs * 2.0d * Math.PI;

        if (angle > Math.PI)
            angle -= 2.0d * Math.PI;

        if (angle <= -Math.PI)
            angle += 2.0d * Math.PI;

        return angle;
    }
}