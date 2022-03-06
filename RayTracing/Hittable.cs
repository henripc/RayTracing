namespace RayTracing
{
    /// <summary>
    /// Abstract <see langword="class"/> that represents a hittable object.
    /// </summary>
    public abstract class Hittable
    {
        /// <summary>
        /// Represents a <see cref="Ray"/> hit in the object. Must be overrided in derived classes.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="tMin"></param>
        /// <param name="tMax"></param>
        /// <param name="rec"></param>
        /// <returns>A <see cref="bool"/> indicating if the object was hit.</returns>
        public virtual bool Hit(Ray r, double tMin, double tMax, HitRecord rec) => false;

        /// <summary>
        /// Represents a <see cref="Ray"/> hit in the object. Must be overrided in derived classes.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="tMin"></param>
        /// <param name="tMax"></param>
        /// <param name="rec"></param>
        /// <returns>A <see cref="bool"/> indicating if the object was hit.</returns>
        public virtual bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec) => false;
    }
}
