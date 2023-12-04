using System.Windows.Media.Media3D;

namespace GACore.Extensions;

public static class Vector3D_ExtensionMethods
{
    public static Vector3D GetVectorTo(this Point3D point3D, Point3D other)
    {
        return new Vector3D(other.X - point3D.X, other.Y - point3D.Y, other.Z - point3D.Z);
    }
}