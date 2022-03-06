using System;
using RayTracing;

static Color RayColor(Ray r, Hittable world, int depth)
{
    var rec = new HitRecord();

    // If we've exceeded the ray bounce limit, no more light is gathered.
    if (depth <= 0) return new Color(0, 0, 0);

    if (world.Hit(r, 0.001, Utility.INFINITY, ref rec))
    {
        var scattered = new Ray();
        var attenuation = new Color();

        if (rec.mat.Scatter(r, rec, ref attenuation, ref scattered)) return attenuation * RayColor(scattered, world, depth - 1);

        return new Color(0, 0, 0);
    }

    Vec3 unitDirection = Vec3.UnitVector(r.Direction);
    double t = 0.5 * (unitDirection.Y + 1.0);

    return (1.0 - t) * new Color(1.0, 1.0, 1.0) + t * new Color(0.5, 0.7, 1.0);
}

// Image

const double aspectRatio = 16.0 / 9.0;
const int imageWidth  = 400;
const int imageHeight = (int)(imageWidth / aspectRatio);
const int samplesPerPixel = 100;
const int maxDepth = 50;

// World

var world = new HittableList();

var materialGround = new Lambertian(new Color(0.8, 0.8, 0));
var materialCenter = new Lambertian(new Color(0.1, 0.2, 0.5));
var materialLeft   = new Dielectric(1.5);
var materialRight  = new Metal(new Color(0.8, 0.6, 0.2), 0);

world.Add(new Sphere(new Point3(0, -100.5, -1), 100, materialGround));
world.Add(new Sphere(new Point3(0, 0, -1), 0.5, materialCenter));
world.Add(new Sphere(new Point3(-1, 0, -1), 0.5, materialLeft));
world.Add(new Sphere(new Point3(-1, 0, -1), -0.4, materialLeft));
world.Add(new Sphere(new Point3(1, 0, -1), 0.5, materialRight));

// Camera
var cam = new Camera();

// Render

Console.WriteLine($"P3\n{ imageWidth } { imageHeight }\n255");

for (int j = imageHeight - 1; j >= 0; j--)
{
    for (int i = 0; i < imageWidth; i++)
    {
        var pixelColor = new Color(0, 0, 0);
        for (int s = 0; s < samplesPerPixel; s++)
        {
            double u = (i + Utility.RandomDouble()) / (imageWidth - 1);
            double v = (j + Utility.RandomDouble()) / (imageHeight - 1);
            Ray r = cam.GetRay(u, v);
            pixelColor += RayColor(r, world, maxDepth);
        }
        Color.WriteColor(pixelColor, samplesPerPixel);
    }
}