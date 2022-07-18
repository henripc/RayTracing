using Color = RayTracing.Vec3;
using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// <see langword="interface"/> that represents a texture.
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Returns the <see cref="Color"/> for the given coordinates.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        Color Value(double u, double v, Point3 p);
    }
}
