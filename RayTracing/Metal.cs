using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a metal material.
    /// </summary>
    public class Metal : IMaterial
    {
        public Color Albedo { get; set; }
        public double Fuzz { get; set; }

        public Metal(Color a, double f)
        {
            Albedo = a;
            Fuzz = f < 1 ? f : 1;
        }

        public bool Scatter(Ray rIn, HitRecord rec, Color attenuation, Ray scattered)
        {
            Vec3 reflected = Vec3.Reflect(Vec3.UnitVector(rIn.Direction), rec.normal);
            var ray = new Ray(rec.p, reflected + Fuzz * Vec3.RandomInUnitySphere());
            scattered.Origin = ray.Origin;
            scattered.Direction = ray.Direction;

            attenuation.X = Albedo.X;
            attenuation.Y = Albedo.Y;
            attenuation.Z = Albedo.Z;

            return Vec3.Dot(scattered.Direction, rec.normal) > 0;
        }
    }
}
