using System;

using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// <see langword="class"/> that represents a solid <see cref="Color"/> texture.
    /// </summary>
    public class SolidColor : ITexture
    {
        private readonly Color _colorValue;

        public SolidColor() : this(new Color(0, 0, 0)) { }
        public SolidColor(Color color) => _colorValue = color;
        public SolidColor(double red, double green, double blue) : this(new Color(red, green, blue)) { }
        
        public Color Value(double u, double v, Color p)
        {
            return _colorValue;
        }
    }
}
