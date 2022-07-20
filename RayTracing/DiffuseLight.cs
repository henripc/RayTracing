using Color = RayTracing.Vec3;

namespace RayTracing
{
    /// <summary>
    /// <see cref="IMaterial"/> that emits light.
    /// </summary>
    public class DiffuseLight : IMaterial
    {
        public ITexture Emit;

        public DiffuseLight(ITexture texture) => Emit = texture;
        public DiffuseLight(Color color) => Emit = new SolidColor(color);

        public bool Scatter(Ray rIn, HitRecord rec, Vec3 attenuation, Ray scattered)
        {
            return false;
        }

        public Color Emitted(double u, double v, Color p)
        {
            return Emit.Value(u, v, p);
        }
    }
}
