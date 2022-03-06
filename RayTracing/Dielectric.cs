using System;

namespace RayTracing
{
    /// <summary>
    /// Class that represents a <see cref="Dielectric"/> material.
    /// </summary>
    public class Dielectric : Material
    {
        /// <summary>
        /// Represents the index of refraction.
        /// </summary>
        public double IndexOfRefraction { get; set; }

        public Dielectric(double indexOfRefraction)
        {
            IndexOfRefraction = indexOfRefraction;
        }

        public override bool Scatter(Ray rIn, HitRecord rec, ref Color attenuation, ref Ray scattered)
        {
            attenuation = new Color(1, 1, 1);
            double refractionRatio = rec.frontFace ? (1 / IndexOfRefraction) : IndexOfRefraction;

            Vec3 unitDirection = Vec3.UnitVector(rIn.Direction);
            double cosTheta = Math.Min(Vec3.Dot(-unitDirection, rec.normal), 1);
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            bool cannotRefract = refractionRatio * sinTheta > 1;
            Vec3 direction;

            if (cannotRefract || Reflectance(cosTheta, refractionRatio) > Utility.RandomDouble())
                direction = Vec3.Reflect(unitDirection, rec.normal);
            else
                direction = Vec3.Refract(unitDirection, rec.normal, refractionRatio);

            scattered = new Ray(rec.p, direction);

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
