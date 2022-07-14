namespace RayTracing
{
    /// <summary>
    /// <see langword="Interface"/> that represents a object that can be hit by a <see cref="Ray"/>.
    /// </summary>
    public interface IHittable
    {
        /// <summary>
        /// Represents a <see cref="Ray"/> hit in the object.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="tMin"></param>
        /// <param name="tMax"></param>
        /// <param name="rec"></param>
        /// <returns>A <see cref="bool"/> indicating if the object was hit.</returns>
        bool Hit(Ray r, double tMin, double tMax, HitRecord rec);
    }
}