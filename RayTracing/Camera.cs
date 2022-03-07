using System;

namespace RayTracing
{
    public class Camera
    {
        private readonly Point3 _origin;
        private readonly Vec3 _lowerLeftCorner;
        private readonly Vec3 _horizontal;
        private readonly Vec3 _vertical;

        public Camera(Point3 lookFrom, Point3 lookAt, Vec3 vUp, double vfov, double aspectRatio)  // vfov = vertical field-of-view in degrees
        {
            double theta          = Utility.DegreesToRadians(vfov);
            double h              = Math.Tan(theta / 2);
            double viewportHeight = 2.0 * h;
            double viewportWidth  = aspectRatio * viewportHeight;

            Vec3 w = Vec3.UnitVector(lookFrom - lookAt);
            Vec3 u = Vec3.UnitVector(Vec3.Cross(vUp, w));
            Vec3 v = Vec3.Cross(w, u);

            _origin               = lookFrom;
            _horizontal           = viewportWidth * u;
            _vertical             = viewportHeight * v;
            _lowerLeftCorner      = _origin - _horizontal/2 - _vertical/2 - w;
        }

        public Ray GetRay(double u, double v) => new Ray(_origin, _lowerLeftCorner + u*_horizontal + v*_vertical - _origin);
    }
}
