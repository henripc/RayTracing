using System;

using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// <see langword="class"/> that represents the camera point of view.
    /// </summary>
    public class Camera
    {
        private readonly Point3 _origin;
        private readonly Vec3 _lowerLeftCorner;
        private readonly Vec3 _horizontal;
        private readonly Vec3 _vertical;
        private readonly Vec3 _u;
        private readonly Vec3 _v;
        private readonly Vec3 _w;
        private readonly double _lensRadius;

        /// <summary>
        /// Instanciate a new <see cref="Camera"/> object.
        /// </summary>
        /// <param name="lookFrom"></param>
        /// <param name="lookAt"></param>
        /// <param name="vUp"></param>
        /// <param name="vfov"></param>
        /// <param name="aspectRatio"></param>
        /// <param name="aperture"></param>
        /// <param name="focusDist"></param>
        public Camera(
            Point3 lookFrom, 
            Point3 lookAt, 
            Vec3 vUp, 
            double vfov,        // vfov = vertical field-of-view in degrees
            double aspectRatio, 
            double aperture, 
            double focusDist
        )
        {
            double theta          = Utility.DegreesToRadians(vfov);
            double h              = Math.Tan(theta / 2);
            double viewportHeight = 2 * h;
            double viewportWidth  = aspectRatio * viewportHeight;

            _w = Vec3.UnitVector(lookFrom - lookAt);
            _u = Vec3.UnitVector(Vec3.Cross(vUp, _w));
            _v = Vec3.Cross(_w, _u);

            _origin          = lookFrom;
            _horizontal      = focusDist * viewportWidth * _u;
            _vertical        = focusDist * viewportHeight * _v;
            _lowerLeftCorner = _origin - _horizontal/2 - _vertical/2 - focusDist * _w;

            _lensRadius = aperture / 2;
        }

        public Ray GetRay(double s, double t)
        {
            Vec3 rd     = _lensRadius * Vec3.RandomInUnitDisk();
            Vec3 offSet = _u * rd.X + _v * rd.Y;

            return new Ray(_origin + offSet, _lowerLeftCorner + s * _horizontal + t * _vertical - _origin - offSet);
        }
    }
}
