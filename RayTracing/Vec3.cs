using System;

namespace RayTracing
{
    /// <summary>
    /// Represents a 3D Vector
    /// </summary>
    public class Vec3
    {
        public double[] e;

        public Vec3() => e = new double[3] { 0, 0, 0 };
        public Vec3(double e0, double e1, double e2) => e = new double[3] { e0, e1, e2 };

        public double X => e[0];
        public double Y => e[1];
        public double Z => e[2];

        public double LengthSquared() => X * X + Y * Y + Z * Z;

        /// <summary>
        /// Length (norm-2) of the vector.
        /// </summary>
        /// <returns>The length of the calling <see cref="Vec3"/></returns>
        public double Length() => Math.Sqrt(LengthSquared());

        public override string ToString() => $"{ X } { Y } { Z }";

        // Defining operators for Vec3 Class
        public static Vec3 operator -(Vec3 v) => new Vec3(-v.X, -v.Y, -v.Z);

        // Define the indexer to allow Vec3 to use [] notation => Vec3[i] = e[i]
        public double this[int i] => e[i];

        public static Vec3 operator +(Vec3 u, Vec3 v) => new Vec3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        public static Vec3 operator -(Vec3 u, Vec3 v) => new Vec3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        public static Vec3 operator *(Vec3 u, Vec3 v) => new Vec3(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        public static Vec3 operator *(double t, Vec3 v) => new Vec3(t * v.X, t * v.Y, t * v.Z);
        public static Vec3 operator *(Vec3 v, double t) => t * v;
        public static Vec3 operator /(Vec3 v, double t) => (1 / t) * v;

        /// <summary>
        /// Calculates the dot product of the given vectors.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns>The dot product between <paramref name="u"/> and <paramref name="v"/></returns>
        public static double Dot(Vec3 u, Vec3 v) => u.X * v.X + u.Y * v.Y + u.Z * v.Z;

        /// <summary>
        /// Calculates the cross product of the given vectors.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns>The resulting vector of the cross product between <paramref name="u"/> and <paramref name="v"/></returns>
        public static Vec3 Cross(Vec3 u, Vec3 v) => new Vec3(u.Y * v.Z - u.Z * v.Y,
                                                             u.Z * v.X - u.X * v.Z,
                                                             u.X * v.Y - u.Y * v.X);

        /// <summary>
        /// Normalizes the given vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns>A new <see cref="Vec3"/> equal to <paramref name="v"/> normalized.</returns>
        public static Vec3 UnitVector(Vec3 v) => v / v.Length();

        // Utility Methods

        /// <summary>
        /// Creates a <see cref="Vec3"/> with random entries in the range [0, 1).
        /// </summary>
        /// <returns></returns>
        public static Vec3 Random() => new Vec3(Utility.RandomDouble(), Utility.RandomDouble(), Utility.RandomDouble());

        /// <summary>
        /// Creates a <see cref="Vec3"/> with random entries in the range [<paramref name="min"/>, <paramref name="max"/>).
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vec3 Random(double min, double max)
        {
            return new Vec3(Utility.RandomDouble(min, max), Utility.RandomDouble(min, max), Utility.RandomDouble(min, max));
        }

        /// <summary>
        /// Static method that return a random <see cref="Vec3"/> that is inside of a unity sphere.
        /// </summary>
        /// <returns></returns>
        public static Vec3 RandomInUnitySphere()
        {
            while (true)
            {
                Vec3 p = Random(-1, 1);
                if (p.LengthSquared() >= 1) continue;
                return p;
            }
        }

        /// <summary>
        /// Returns a random unity vector.
        /// </summary>
        /// <returns></returns>
        public static Vec3 RandomUnityVector() => UnitVector(RandomInUnitySphere());

        /// <summary>
        /// Returns a random <see cref="Vec3"/> away from the hit point, with no dependence on the angle from the normal.
        /// </summary>
        /// <param name="normal"></param>
        /// <returns></returns>
        public static Vec3 RandomInHemisphere(Vec3 normal)
        {
            Vec3 inUnitySphere = RandomInUnitySphere();
            if (Dot(inUnitySphere, normal) > 0.0) return inUnitySphere;    // In the same hemisphere as the normal
            else return -inUnitySphere;
        }
    }
}

// Type aliases for vec3
//using point3 = vec3;	    // 3D point
//using color = vec3;		// RGB color
