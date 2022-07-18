using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a diffuse material.
    /// </summary>
    public class Lambertian : IMaterial
    {
        public ITexture Albedo { get; set; }

        public Lambertian(Color color) => Albedo = new SolidColor(color);
        public Lambertian(ITexture a) => Albedo = a;

        public bool Scatter(Ray rIn, HitRecord rec, Color attenuation, Ray scattered)
        {
            Vec3 scatterDirection = rec.normal! + Vec3.RandomUnityVector();

            // Catch degenerate scatter direction
            if (scatterDirection.NearZero()) scatterDirection = rec.normal!;

            var ray = new Ray(rec.p!, scatterDirection, rIn.Time);
            scattered.Origin = ray.Origin;
            scattered.Direction = ray.Direction;
            scattered.Time = ray.Time;

            Color tempColor = Albedo.Value(rec.u, rec.v, rec.p!);
            attenuation.X = tempColor.X;
            attenuation.Y = tempColor.Y;
            attenuation.Z = tempColor.Z;

            return true;
        }
    }
}
