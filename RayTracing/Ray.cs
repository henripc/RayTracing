namespace RayTracing
{
    public class Ray
    {
        public Point3 Origin { get; set; }
        public Vec3 Direction { get; set; }

        public Ray()
        {
            Origin = new Point3();
            Direction = new Vec3();
        }

        public Ray(Point3 origin, Vec3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point3 At(double t) => Origin + t * Direction;
    }
}
