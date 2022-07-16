using System;

using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    public class AABB
    {
        public Point3 Minimum { get; set; }
        public Point3 Maximum { get; set; }

        public AABB()
        {
            Minimum = new Point3();
            Maximum = new Point3();
        }

        public AABB(Point3 a, Point3 b)
        {
            Minimum = a;
            Maximum = b;
        }

        public bool Hit(Ray r, double tMin, double tMax)
        {
            for (int a = 0; a < 3; a++)
            {
                double invD = 1d / r.Direction[a];
                double t0 = (Minimum[a] - r.Origin[a]) * invD;
                double t1 = (Maximum[a] - r.Origin[a]) * invD;

                if (invD < 0d)
                    (t0, t1) = (t1, t0);

                tMin = t0 > tMin ? t0 : tMin;
                tMax = t1 < tMax ? t1: tMax;

                if (tMax <= tMin)
                    return false;
            }

            return true;
        }

        public static AABB SurroundingBox(AABB box0, AABB box1)
        {
            var small = new Point3(Math.Min(box0.Minimum.X, box1.Minimum.X),
                                   Math.Min(box0.Minimum.Y, box1.Minimum.Y),
                                   Math.Min(box0.Minimum.Z, box1.Minimum.Z));
            var big = new Point3(Math.Max(box0.Maximum.X, box1.Maximum.X),
                                 Math.Max(box0.Maximum.Y, box1.Maximum.Y),
                                 Math.Max(box0.Maximum.Z, box1.Maximum.Z));

            return new AABB(small, big);
        }
    }
}
