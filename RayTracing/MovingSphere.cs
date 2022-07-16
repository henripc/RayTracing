using System;

using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a moving sphere object.
    /// </summary>
    public class MovingSphere : IHittable
    {
        public Point3 Center0 { get; set; }
        public Point3 Center1 { get; set; }
        public double Time0 { get; set; }
        public double Time1 { get; set; }
        public double Radius { get; set; }
        public IMaterial? Material { get; set; }

        public MovingSphere()
        {
            Center0 = new Point3();
            Center1 = new Point3();
            Material = null;
        }

        public MovingSphere(Point3 center0, Point3 center1, double time0, double time1, double radius, IMaterial material)
        {
            Center0 = center0;
            Center1 = center1;
            Time0 = time0;
            Time1 = time1;
            Radius = radius;
            Material = material;
        }

        public Point3 Center(double time) => Center0 + ((time - Time0) / (Time1 - Time0)) * (Center1 - Center0);

        public bool Hit(Ray r, double tMin, double tMax, HitRecord rec)
        {
            Vec3 oc = r.Origin - Center(r.Time);
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
            Vec3 outwardNormal = (rec.p - Center(r.Time)) / Radius;
            rec.SetFaceNormal(r, outwardNormal);
            rec.mat = Material;

            return true;
        }

        public bool BoundingBox(double time0, double time1, AABB outputBox)
        {
            var box0 = new AABB(Center(time0) - new Vec3(Radius, Radius, Radius),
                                Center(time0) + new Vec3(Radius, Radius, Radius));
            var box1 = new AABB(Center(time1) - new Vec3(Radius, Radius, Radius),
                                Center(time1) + new Vec3(Radius, Radius, Radius));

            AABB tempBox = AABB.SurroundingBox(box0, box1);
            outputBox.Minimum = tempBox.Minimum;
            outputBox.Maximum = tempBox.Maximum;

            return true;
        }
    }
}
