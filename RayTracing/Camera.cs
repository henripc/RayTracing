namespace RayTracing
{
    public class Camera
    {
        private readonly Point3 _origin;
        private readonly Vec3 _lowerLeftCorner;
        private readonly Vec3 _horizontal;
        private readonly Vec3 _vertical;

        public Camera()
        {
            double aspectRatio    = 16.0 / 9.0;
            double viewportHeight = 2.0;
            double viewportWidth  = aspectRatio * viewportHeight;
            double focalLength    = 1.0;

            _origin               = new Point3(0, 0, 0);
            _horizontal           = new Vec3(viewportWidth, 0, 0);
            _vertical             = new Vec3(0, viewportHeight, 0);
            _lowerLeftCorner      = _origin - _horizontal/2 - _vertical/2 - new Vec3(0, 0, focalLength);
        }

        public Ray GetRay(double u, double v) => new Ray(_origin, _lowerLeftCorner + u*_horizontal + v*_vertical - _origin);
    }
}
