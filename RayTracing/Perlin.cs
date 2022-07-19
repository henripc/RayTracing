using System;
using Point3 = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Represents a Perlin noise to be used as a <see cref="ITexture"/>.
    /// </summary>
    public class Perlin
    {
        private static readonly int _pointCount = 256;
        private readonly Vec3[] _randomVec;
        private readonly int[] _permX;
        private readonly int[] _permY;
        private readonly int[] _permZ;

        public Perlin()
        {
            _randomVec = new Vec3[_pointCount];

            for (int i = 0; i < _pointCount; i++)
                _randomVec[i] = Vec3.UnitVector(Vec3.Random(-1, 1));

            _permX = PerlinGeneratePerm();
            _permY = PerlinGeneratePerm();
            _permZ = PerlinGeneratePerm();
        }

        //~Perlin() { }

        /// <summary>
        /// Returns the same random number if the same <see cref="Point3"/> is given.
        /// </summary>
        /// <param name="p"></param>
        /// <remarks>Uses trilinear interpolation and cubic Hermite spline (for smoothing).</remarks>
        public double Noise(Point3 p)
        {
            double u = p.X - Math.Floor(p.X);
            double v = p.Y - Math.Floor(p.Y);
            double w = p.Z - Math.Floor(p.Z);

            int i = (int)Math.Floor(p.X);
            int j = (int)Math.Floor(p.Y);
            int k = (int)Math.Floor(p.Z);

            var c = new Vec3[2, 2, 2];

            for (int di = 0; di < 2; di++)
                for (int dj = 0; dj < 2; dj++)
                    for (int dk = 0; dk < 2; dk++)
                        c[di, dj, dk] = _randomVec[_permX[(i + di) & 255] ^ _permY[(j + dj) & 255] ^ _permZ[(k + dk) & 255]];

            return PerlinInterpolation(c, u, v, w); 
        }

        /// <summary>
        /// A composite noise with multiple frequencies. A sum of repeated calls to <see cref="Noise(Point3)"/>.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public double Turbulance(Point3 p, int depth = 7)
        {
            double accum = 0d;
            Point3 tempPoint = p;
            double weight = 1d;

            for (int i = 0; i < depth; i++)
            {
                accum += weight * Noise(tempPoint);
                weight *= 0.5;
                tempPoint *= 2;
            }

            return Math.Abs(accum);
        }

        private static int[] PerlinGeneratePerm()
        {
            var p = new int[_pointCount];

            for (int i = 0; i < _pointCount; i++)
                p[i] = i;

            Permute(p, _pointCount);

            return p;
        }

        /// <summary>
        /// Randomly permutes the elements of the given <see langword="int"/>[].
        /// </summary>
        /// <param name="p"></param>
        /// <param name="n"></param>
        private static void Permute(int[] p, int n)
        {
            for (int i = n - 1; i > 0; i--)
            {
                int target = Utility.RandomInt(0, i);
                (p[target], p[i]) = (p[i], p[target]);
            }
        }

        private static double PerlinInterpolation(Vec3[,,] c, double u, double v, double w)
        {
            var uu = u * u * (3 - 2 * u);
            var vv = v * v * (3 - 2 * v);
            var ww = w * w * (3 - 2 * w);

            double accum = 0d;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    for (int k = 0; k < 2; k++)
                    {
                        var weightV = new Vec3(u - i, v - j, w - k);
                        accum += (i * uu + (1 - i) * (1 - uu)) *
                                 (j * vv + (1 - j) * (1 - vv)) *
                                 (k * ww + (1 - k) * (1 - ww)) *
                                 Vec3.Dot(c[i, j, k], weightV);
                    }

            return accum;
        }
    }
}
