using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a ray of light.
    /// </summary>
    public class Ray
    {
        /// <summary>
        /// Point of origin of the <see cref="Ray"/>
        /// </summary>
        public Point3 Origin { get; set; }

        /// <summary>
        /// Direction of the <see cref="Ray"/>
        /// </summary>
        public Vec3 Direction { get; set; }

        /// <summary>
        /// Time that the <see cref="Ray"/> exists.
        /// </summary>
        public double Time { get; set; }

        public Ray()
        {
            Origin = new Point3();
            Direction = new Vec3();
        }

        public Ray(Point3 origin, Vec3 direction, double time = 0d)
        {
            Origin = origin;
            Direction = direction;
            Time = time;
        }

        public Point3 At(double t) => Origin + t * Direction;
    }
}
