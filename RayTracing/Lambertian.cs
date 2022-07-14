using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a diffuse material.
    /// </summary>
    public class Lambertian : IMaterial
    {
        public Color Albedo { get; set; }

        public Lambertian(Color a)
        {
            Albedo = a;
        }

        public bool Scatter(Ray rIn, HitRecord rec, Color attenuation, Ray scattered)
        {
            Vec3 scatterDirection = rec.normal + Vec3.RandomUnityVector();

            // Catch degenerate scatter direction
            if (scatterDirection.NearZero()) scatterDirection = rec.normal;

            var ray = new Ray(rec.p, scatterDirection);
            scattered.Origin = ray.Origin;
            scattered.Direction = ray.Direction;

            attenuation.X = Albedo.X;
            attenuation.Y = Albedo.Y;
            attenuation.Z = Albedo.Z;

            return true;
        }
    }
}
