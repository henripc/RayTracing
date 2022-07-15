using System;

namespace RayTracing
{
    /// <summary>
    /// Represents a 3D Vector
    /// </summary>
    public class Vec3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vec3() => X = Y = Z = 0;
        public Vec3(double e0, double e1, double e2)
        {
            X = e0;
            Y = e1;
            Z = e2;
        }

        public double LengthSquared() => X * X + Y * Y + Z * Z;

        /// <summary>
        /// Length (norm-2) of the vector.
        /// </summary>
        public double Length() => Math.Sqrt(LengthSquared());

        public override string ToString() => $"{ X } { Y } { Z }";

        // Defining operators for Vec3 Class
        public static Vec3 operator -(Vec3 v) => new Vec3(-v.X, -v.Y, -v.Z);
        //{
        //    v.X = -v.X;
        //    v.Y = -v.Y;
        //    v.Z = -v.Z;

        //    return v;
        //}

        // Define the indexer to allow Vec3 to use [] notation => Vec3[i] = e[i]
        //public double this[int i] => e[i];

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
        /// Creates a random <see cref="Vec3"/> that is inside of a unity sphere.
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
        /// Returns <see langword="true"/> if the <see cref="Vec3"/> is close to zero in all dimensions, <see langword="false"/> otherwise.
        /// </summary>
        /// <returns></returns>
        public bool NearZero()
        {
            double s = 1E-8;
            return (Math.Abs(X) < s) && (Math.Abs(Y) < s) && (Math.Abs(Z) < s);
        }

        /// <summary>
        /// Returns a reflected <see cref="Vec3"/> based on a given normal <see cref="Vec3"/> <paramref name="n"/>.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Vec3 Reflect(Vec3 v, Vec3 n) => v - 2 * Dot(v, n) * n;

        /// <summary>
        /// Returns a refracted <see cref="Vec3"/>.
        /// </summary>
        /// <param name="incident"></param>
        /// <param name="n"></param>
        /// <param name="etaIOverEtat"></param>
        /// <returns></returns>
        public static Vec3 Refract(Vec3 incident, Vec3 n, double etaIOverEtat)
        {
            double cosTheta = Math.Min(Dot(-incident, n), 1);
            Vec3 rOutPerp = etaIOverEtat * (incident + cosTheta * n);
            Vec3 rOutParallel = -Math.Sqrt(Math.Abs(1 - rOutPerp.LengthSquared())) * n;

            return rOutPerp + rOutParallel;
        }

        /// <summary>
        /// Creates a random <see cref="Vec3"/> that is inside of a unity disk.
        /// </summary>
        /// <returns></returns>
        public static Vec3 RandomInUnitDisk()
        {
            while (true)
            {
                var p = new Vec3(Utility.RandomDouble(-1, 1), Utility.RandomDouble(-1, 1), 0);
                if (p.LengthSquared() >= 1) continue;
                return p;
            }
        }
    }
}
