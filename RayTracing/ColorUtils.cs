using System;

using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// Utility class for color operations.
    /// </summary>
    public static class ColorUtils
    {
        /// <summary>
        /// Function that writes the colors for each pixel.
        /// </summary>
        /// <param name="pixelColor"></param>
        public static string WriteColor(Color pixelColor, int samplesPerPixel)
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
            return $"{ (int)(256 * Utility.Clamp(r, 0, 0.999)) } { (int)(256 * Utility.Clamp(g, 0, 0.999)) } { (int)(256 * Utility.Clamp(b, 0, 0.999)) }\n";
        }
    }
}
