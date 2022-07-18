using System;

using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a sphere object.
    /// </summary>
    public class Sphere : IHittable
    {
        public Point3 Center { get; set; }
        public double Radius { get; set; }
        public IMaterial? Material { get; set; }

        public Sphere()
        {
            Center = new Point3(0, 0, 0);
            Material = null;
        }

        public Sphere(Point3 center, double radius, IMaterial material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public bool Hit(Ray r, double tMin, double tMax, HitRecord rec)
        {
            Vec3 oc = r.Origin - Center;
            double a = r.Direction.LengthSquared();
            double halfB = Vec3.Dot(oc, r.Direction);
            double c = oc.LengthSquared() - Radius * Radius;

            double discriminant = halfB * halfB - a * c;
            if (discriminant < 0) return false;
            double sqrtd = Math.Sqrt(discriminant);

            // Find the nearest root that lies in the acceptable range.
            double root = (-halfB - sqrtd) / a;
            if (root < tMin || tMax < root)
            {
                root = (-halfB + sqrtd) / a;
                if (root < tMin || tMax < root) return false;
            }

            rec.t = root;
            rec.p = r.At(rec.t);
            Vec3 outwardNormal = (rec.p - Center) / Radius;
            rec.SetFaceNormal(r, outwardNormal);
            GetSphereUV(outwardNormal, out rec.u, out rec.v);
            rec.mat = Material;

            return true;
        }

        public bool BoundingBox(double time0, double time1, AABB outputBox)
        {
            var tempBox = new AABB(Center - new Vec3(Radius, Radius, Radius),
                                   Center + new Vec3(Radius, Radius, Radius));
            outputBox.Minimum = tempBox.Minimum;
            outputBox.Maximum = tempBox.Maximum;

            return true;
        }

        /// <summary>
        /// Utility function that takes points on the unit sphere centered at the origin,
        /// and computes the spherical coordinates <paramref name="u"/> and <paramref name="v"/>.
        /// </summary>
        private static void GetSphereUV(Point3 p, out double u, out double v)
        {
            // p: a given point on the sphere of radius one, centered at the origin.
            // u: returned value [0,1] of angle around the Y axis from X=-1.
            // v: returned value [0,1] of angle from Y=-1 to Y=+1.
            // <1 0 0> yields <0.50 0.50> <-1 0 0> yields <0.00 0.50>
            // <0 1 0> yields <0.50 1.00> < 0 -1 0> yields <0.50 0.00>
            // <0 0 1> yields <0.25 0.50> < 0 0 -1> yields <0.75 0.50>

            double theta = Math.Acos(-p.Y);
            double phi = Math.Atan2(-p.Z, p.X) + Utility.PI;

            u = phi / (2 * Utility.PI);
            v = theta / Utility.PI;
        }
    }
}
