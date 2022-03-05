namespace RayTracing
{
    /// <summary>
    /// Represents a 3D Point, derived from <see cref="Vec3"/>
    /// </summary>
    public class Point3 : Vec3
    {
        public Point3() : base() { }
        public Point3(double e0, double e1, double e2) : base(e0, e1, e2) { }

        // Defining operators for Point3 Class
        public static Point3 operator -(Point3 p) => new Point3(-p.X, -p.Y, -p.Z);
        public static Point3 operator +(Point3 p, Vec3 v) => new Point3(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        public static Point3 operator +(Point3 u, Point3 v) => new Point3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        public static Vec3 operator -(Point3 u, Point3 v) => new Vec3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        public static Point3 operator *(Point3 u, Point3 v) => new Point3(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        public static Point3 operator *(double t, Point3 v) => new Point3(t * v.X, t * v.Y, t * v.Z);
        public static Point3 operator *(Point3 v, double t) => t * v;
        public static Point3 operator /(Point3 v, double t) => (1 / t) * v;
    }
}
