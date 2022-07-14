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
            Radius = 0;
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
            rec.mat = Material;

            return true;
        }
    }
}
