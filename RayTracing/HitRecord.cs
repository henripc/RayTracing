namespace RayTracing
{
    public class HitRecord
    {
        public Point3 p;
        public Vec3 normal;
        public double t;
        public bool frontFace;

        public HitRecord()
        {
            p = new Point3();
            normal = new Vec3();
            t = 0;
            frontFace = false;
        }

        public void SetFaceNormal(Ray r, Vec3 outwardNormal)
        {
            frontFace = Vec3.Dot(r.Direction, outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }
}
