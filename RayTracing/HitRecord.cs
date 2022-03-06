namespace RayTracing
{
    public class HitRecord
    {
        public Point3 p;
        public Vec3 normal;
        public Material mat;
        public double t;
        public bool frontFace;

        public HitRecord()
        {
            p = new Point3();
            normal = new Vec3();
            mat = new Metal(new Color(), 0);   // Initializes material to Metal (no reason)
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
