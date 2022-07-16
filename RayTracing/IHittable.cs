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

        /// <summary>
        /// Computes the Bounding Box of the <see cref="IHittable"/> object.
        /// </summary>
        /// <param name="time0"></param>
        /// <param name="time1"></param>
        /// <param name="outputBox"></param>
        /// <returns></returns>
        bool BoundingBox(double time0, double time1, AABB outputBox);
    }
}