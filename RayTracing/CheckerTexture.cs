using System;

using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// <see langword="class"/> that represents a Checker texture.
    /// </summary>
    public class CheckerTexture : ITexture
    {
        public ITexture Even;
        public ITexture Odd;

        public CheckerTexture(ITexture even, ITexture odd)
        {
            Even = even;
            Odd = odd;
        }

        public CheckerTexture(Color c1, Color c2)
        {
            Even = new SolidColor(c1);
            Odd = new SolidColor(c2);
        }

        public Color Value(double u, double v, Color p)
        {
            double sines = Math.Sin(10 * p.X) * Math.Sin(10 * p.Y) * Math.Sin(10 * p.Z);

            if (sines < 0)
                return Odd.Value(u, v, p);
            else
                return Even.Value(u, v, p);
        }
    }
}
