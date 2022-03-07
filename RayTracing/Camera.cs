using System;

namespace RayTracing
{
    public class Camera
    {
        private readonly Point3 _origin;
        private readonly Vec3 _lowerLeftCorner;
        private readonly Vec3 _horizontal;
        private readonly Vec3 _vertical;
        private readonly Vec3 u;
        private readonly Vec3 v;
        private readonly Vec3 w;
        private readonly double lensRadius;

        public Camera(Point3 lookFrom, Point3 lookAt, Vec3 vUp, double vfov,
                      double aspectRatio, double aperture, double focusDist)  // vfov = vertical field-of-view in degrees
        {
            double theta          = Utility.DegreesToRadians(vfov);
            double h              = Math.Tan(theta / 2);
            double viewportHeight = 2.0 * h;
            double viewportWidth  = aspectRatio * viewportHeight;

            w = Vec3.UnitVector(lookFrom - lookAt);
            u = Vec3.UnitVector(Vec3.Cross(vUp, w));
            v = Vec3.Cross(w, u);

            _origin               = lookFrom;
            _horizontal           = focusDist * viewportWidth * u;
            _vertical             = focusDist * viewportHeight * v;
            _lowerLeftCorner      = _origin - _horizontal/2 - _vertical/2 - focusDist * w;

            lensRadius = aperture / 2;
        }

        public Ray GetRay(double s, double t)
        {
            Vec3 rd     = lensRadius * Vec3.RandomInUnitDisk();
            Vec3 offSet = u * rd.X + v * rd.Y;

            return new Ray(_origin + offSet, _lowerLeftCorner + s * _horizontal + t * _vertical - _origin - offSet);
        }
    }
}
