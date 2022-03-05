using System;

namespace RayTracing
{
    /// <summary>
    /// Represents a RGB color
    /// </summary>
    public class Color : Vec3
    {
        public Color() : base() { }
        public Color(double e0, double e1, double e2) : base(e0, e1, e2) { }

        /// <summary>
        /// Function that writes the colors for each pixel.
        /// </summary>
        /// <param name="pixelColor"></param>
        public static void WriteColor(Color pixelColor, int samplesPerPixel)
        {
            double r = pixelColor.X;
            double g = pixelColor.Y;
            double b = pixelColor.Z;

            // Divide the color by the number of samples and gamma-correct for gamma = 2.0.
            double scale = 1.0 / samplesPerPixel;
            r = Math.Sqrt(scale * r);
            g = Math.Sqrt(scale * g);
            b = Math.Sqrt(scale * b);

            // Write the translated [0,255] value of each color component.
            Console.WriteLine($"{ (int)(256 * Utility.Clamp(r, 0, 0.999)) } { (int)(256 * Utility.Clamp(g, 0, 0.999)) } { (int)(256 * Utility.Clamp(b, 0, 0.999)) }");
        }

        // Defining operators for Color Class
        public static Color operator +(Color u, Color v) => new Color(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        public static Color operator +(Vec3 u, Color v) => new Color(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        public static Color operator -(Color u, Color v) => new Color(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        public static Color operator *(Color u, Color v) => new Color(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        public static Color operator *(double t, Color v) => new Color(t * v.X, t * v.Y, t * v.Z);
        public static Color operator *(Color v, double t) => t * v;
        public static Color operator /(Color v, double t) => (1 / t) * v;
    }
}
