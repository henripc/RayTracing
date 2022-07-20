using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a rectangle object on the xy plane.
    /// </summary>
    public class RectangleXY : IHittable
    {
        public IMaterial? Material { get; set; }
        public double X0, X1, Y0, Y1, K;

        public RectangleXY() { }
        public RectangleXY(double x0, double x1, double y0, double y1, double k, IMaterial material)
        {
            X0 = x0;
            X1 = x1;
            Y0 = y0;
            Y1 = y1;
            K = k;
            Material = material;
        }

        public bool BoundingBox(double time0, double time1, AABB outputBox)
        {
            // The bounding box must have non-zero width in each dimension, so pad the Z dimension a small amount.
            var temp = new AABB(new Point3(X0, Y0, K - 0.0001), new Point3(X1, Y1, K + 0.0001));
            outputBox.Minimum = temp.Minimum;
            outputBox.Maximum = temp.Maximum;

            return true;
        }

        public bool Hit(Ray r, double tMin, double tMax, HitRecord rec)
        {
            double t = (K - r.Origin.Z) / r.Direction.Z;

            if (t < tMin || t > tMax)
                return false;

            double x = r.Origin.X + t * r.Direction.X;
            double y = r.Origin.Y + t * r.Direction.Y;

            if (x < X0 || x > X1 || y < Y0 || y > Y1)
                return false;

            rec.u = (x - X0) / (X1 - X0);
            rec.v = (y - Y0) / (Y1 - Y0);
            rec.t = t;
            var outwardNormal = new Vec3(0, 0, 1);
            rec.SetFaceNormal(r, outwardNormal);
            rec.mat = Material;
            rec.p = r.At(t);

            return true;
        }
    }

    /// <summary>
    /// Represents a rectangle object on the xz plane.
    /// </summary>
    public class RectangleXZ : IHittable
    {
        public IMaterial? Material { get; set; }
        public double X0, X1, Z0, Z1, K;

        public RectangleXZ() { }
        public RectangleXZ(double x0, double x1, double z0, double z1, double k, IMaterial material)
        {
            X0 = x0;
            X1 = x1;
            Z0 = z0;
            Z1 = z1;
            K = k;
            Material = material;
        }

        public bool BoundingBox(double time0, double time1, AABB outputBox)
        {
            // The bounding box must have non-zero width in each dimension, so pad the Y dimension a small amount.
            var temp = new AABB(new Point3(X0, K - 0.0001, Z0), new Point3(X1, K + 0.0001, Z1));
            outputBox.Minimum = temp.Minimum;
            outputBox.Maximum = temp.Maximum;

            return true;
        }

        public bool Hit(Ray r, double tMin, double tMax, HitRecord rec)
        {
            double t = (K - r.Origin.Y) / r.Direction.Y;

            if (t < tMin || t > tMax)
                return false;

            double x = r.Origin.X + t * r.Direction.X;
            double z = r.Origin.Z + t * r.Direction.Z;

            if (x < X0 || x > X1 || z < Z0 || z > Z1)
                return false;

            rec.u = (x - X0) / (X1 - X0);
            rec.v = (z - Z0) / (Z1 - Z0);
            rec.t = t;
            var outwardNormal = new Vec3(0, 1, 0);
            rec.SetFaceNormal(r, outwardNormal);
            rec.mat = Material;
            rec.p = r.At(t);

            return true;
        }
    }

    /// <summary>
    /// Represents a rectangle object on the yz plane.
    /// </summary>
    public class RectangleYZ : IHittable
    {
        public IMaterial? Material { get; set; }
        public double Y0, Y1, Z0, Z1, K;

        public RectangleYZ() { }
        public RectangleYZ(double y0, double y1, double z0, double z1, double k, IMaterial material)
        {
            Y0 = y0;
            Y1 = y1;
            Z0 = z0;
            Z1 = z1;
            K = k;
            Material = material;
        }

        public bool BoundingBox(double time0, double time1, AABB outputBox)
        {
            // The bounding box must have non-zero width in each dimension, so pad the X dimension a small amount.
            var temp = new AABB(new Point3(K - 0.0001, Y0, Z0), new Point3(K + 0.0001, Y1, Z1));
            outputBox.Minimum = temp.Minimum;
            outputBox.Maximum = temp.Maximum;

            return true;
        }

        public bool Hit(Ray r, double tMin, double tMax, HitRecord rec)
        {
            double t = (K - r.Origin.X) / r.Direction.X;

            if (t < tMin || t > tMax)
                return false;

            double y = r.Origin.Y + t * r.Direction.Y;
            double z = r.Origin.Z + t * r.Direction.Z;

            if (y < Y0 || y > Y1 || z < Z0 || z > Z1)
                return false;

            rec.u = (y - Y0) / (Y1 - Y0);
            rec.v = (z - Z0) / (Z1 - Z0);
            rec.t = t;
            var outwardNormal = new Vec3(1, 0, 0);
            rec.SetFaceNormal(r, outwardNormal);
            rec.mat = Material;
            rec.p = r.At(t);

            return true;
        }
    }
}
