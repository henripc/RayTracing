using System;

using Point3 = RayTracing.Vec3;
using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a noise <see cref="ITexture"/>.
    /// </summary>
    public class NoiseTexture : ITexture
    {
        public Perlin Noise = new Perlin();
        public double Scale;

        public NoiseTexture() { }
        public NoiseTexture(double scale) => Scale = scale;

        public Color Value(double u, double v, Point3 p)
        {
            return new Color(1, 1, 1) * 0.5 * (1 + Math.Sin(Scale * p.Z + 10 * Noise.Turbulance(p)));
        }
    }
}
