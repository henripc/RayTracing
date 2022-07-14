using System;

using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a "clear" material (glass, diamonds etc...).
    /// </summary>
    public class Dielectric : IMaterial
    {
        /// <summary>
        /// Represents the index of refraction.
        /// </summary>
        public double IndexOfRefraction { get; set; }

        public Dielectric(double indexOfRefraction) => IndexOfRefraction = indexOfRefraction;

        public bool Scatter(Ray rIn, HitRecord rec, Color attenuation, Ray scattered)
        {
            attenuation.X = 1;
            attenuation.Y = 1;
            attenuation.Z = 1;

            double refractionRatio = rec.frontFace ? (1 / IndexOfRefraction) : IndexOfRefraction;

            Vec3 unitDirection = Vec3.UnitVector(rIn.Direction);
            double cosTheta = Math.Min(Vec3.Dot(-unitDirection, rec.normal), 1);
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            bool cannotRefract = refractionRatio * sinTheta > 1;
            Vec3 direction;

            if (cannotRefract || Reflectance(cosTheta, refractionRatio) > Utility.RandomDouble())
            {
                direction = Vec3.Reflect(unitDirection, rec.normal);
            }
            else
            {
                direction = Vec3.Refract(unitDirection, rec.normal, refractionRatio);
            }

            var ray = new Ray(rec.p, direction);
            scattered.Origin = ray.Origin;
            scattered.Direction = ray.Direction;

            return true;
        }

        /// <summary>
        /// Calculates the reflectance of the material using Schlick's approximation.
        /// </summary>
        /// <param name="cosine"></param>
        /// <param name="refIdx"></param>
        /// <returns></returns>
        private static double Reflectance(double cosine, double refIdx)
        {
            // Use Schlick's approximation for reflectance.
            double r0 = (1 - refIdx) / (1 + refIdx);
            r0 *= r0;

            return r0 + (1 - r0) * Math.Pow(1 - cosine, 5);
        }
    }
}
