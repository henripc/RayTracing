namespace RayTracing
{
    /// <summary>
    /// Abstract <see langword="class"/> that represents a material.
    /// </summary>
    public abstract class Material
    {
        /// <summary>
        /// Method that verify if a <see cref="Ray"/> is scaterred when hitting a object.
        /// </summary>
        /// <param name="rIn"></param>
        /// <param name="rec"></param>
        /// <param name="attenuation"></param>
        /// <param name="scattered"></param>
        /// <returns>A <see langword="bool"/> indicating if the <see cref="Ray"/> was scattered.</returns>
        public virtual bool Scatter(Ray rIn, HitRecord rec, Color attenuation, Ray scattered) => false;

        /// <summary>
        /// Method that verify if a <see cref="Ray"/> is scaterred when hitting a object.
        /// </summary>
        /// <param name="rIn"></param>
        /// <param name="rec"></param>
        /// <param name="attenuation"></param>
        /// <param name="scattered"></param>
        /// <returns>A <see langword="bool"/> indicating if the <see cref="Ray"/> was scattered.</returns>
        public virtual bool Scatter(Ray rIn, HitRecord rec, ref Color attenuation, ref Ray scattered) => false;
    }
}
