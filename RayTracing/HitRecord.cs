using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a record of a <see cref="Ray"/> hitting a object.
    /// </summary>
    public class HitRecord
    {
        public Point3? p;
        public Vec3? normal;
        public IMaterial? mat;
        public double t;
        public bool frontFace;

        public HitRecord() { }

        public void SetFaceNormal(Ray r, Vec3 outwardNormal)
        {
            frontFace = Vec3.Dot(r.Direction, outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }
}
