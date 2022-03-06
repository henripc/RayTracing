namespace RayTracing
{
    public class Metal : Material
    {
        public Color Albedo { get; set; }
        public double Fuzz { get; set; }

        public Metal(Color a, double f)
        {
            Albedo = a;
            Fuzz = f < 1 ? f : 1;
        }

        public override bool Scatter(Ray rIn, HitRecord rec, ref Color attenuation, ref Ray scattered)
        {
            Vec3 reflected = Vec3.Reflect(Vec3.UnitVector(rIn.Direction), rec.normal);
            scattered = new Ray(rec.p, reflected + Fuzz*Vec3.RandomInUnitySphere());
            attenuation = Albedo;

            return Vec3.Dot(scattered.Direction, rec.normal) > 0;
        }
    }
}
