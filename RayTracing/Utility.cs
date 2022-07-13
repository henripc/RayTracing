using System;

namespace RayTracing
{
    /// <summary>
    /// Helper class that expose some utility functions and constants.
    /// </summary>
    public static class Utility
    {
        private static Random? randNumber = null;

        // Constants

        public const double INFINITY = double.PositiveInfinity;
        public const double PI       = Math.PI;

        // Random number Singleton
        private static Random RandomNumber
        {
            get
            {   
                randNumber ??= new Random();
                return randNumber;
            }
        }

        // Functions

        /// <summary>
        /// Convert a given angle in degrees to radians.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double DegreesToRadians(double degrees) => degrees * PI / 180.0;

        /// <summary>
        /// Returns a random floating-point number in [0, 1).
        /// </summary>
        /// <returns></returns>
        public static double RandomDouble() => RandomNumber.NextDouble();

        /// <summary>
        /// Returns a random floating-point number in [<paramref name="min"/>, <paramref name="max"/>).
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double RandomDouble(double min, double max) => RandomNumber.NextDouble() * (max - min) + min;

        /// <summary>
        /// Clamps the <paramref name="x"/> value to the range [<paramref name="min"/>, <paramref name="max"/>].
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double Clamp(double x, double min, double max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }
    }
}
