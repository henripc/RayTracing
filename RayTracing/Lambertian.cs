namespace RayTracing
{
    public class Lambertian : Material
    {
        public Color Albedo { get; set; }

        public Lambertian(Color a)
        {
            Albedo = a;
        }

        public override bool Scatter(Ray rIn, HitRecord rec, ref Color attenuation, ref Ray scattered)
        {
            Vec3 scatterDirection = rec.normal + Vec3.RandomUnityVector();

            // Catch degenerate scatter direction
            if (scatterDirection.NearZero()) scatterDirection = rec.normal;

            scattered = new Ray(rec.p, scatterDirection);
            attenuation = Albedo;

            return true;
        }
    }
}
