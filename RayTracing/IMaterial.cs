using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// <see langword="interface"/> that represents a material.
    /// </summary>
    public interface IMaterial
    {
        /// <summary>
        /// Method that verify if a <see cref="Ray"/> is scaterred when hitting a object.
        /// </summary>
        /// <param name="rIn"></param>
        /// <param name="rec"></param>
        /// <param name="attenuation"></param>
        /// <param name="scattered"></param>
        /// <returns>A <see langword="bool"/> indicating if the <see cref="Ray"/> was scattered.</returns>
        bool Scatter(Ray rIn, HitRecord rec, Color attenuation, Ray scattered);
    }
}