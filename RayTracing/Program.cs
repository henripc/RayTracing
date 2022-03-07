using System;
using RayTracing;

static HittableList RandomScene()
{
    var world = new HittableList();

    var materialGround = new Lambertian(new Color(0.5, 0.5, 0.5));
    world.Add(new Sphere(new Point3(0, -1000, 0), 1000, materialGround));

    for (int a = -11; a < 11; a++)
    {
        for (int b = -11; b < 11; b++)
        {
            double chooseMat = Utility.RandomDouble();
            var center = new Point3(a + 0.9 * Utility.RandomDouble(), 0.2, b + 0.9 * Utility.RandomDouble());

            if ((center - new Point3(4, 0.2, 0)).Length() > 0.9)
            {
                Material sphereMaterial;

                if (chooseMat < 0.8)
                {
                    // diffuse
                    var albedo = Color.Random() * Color.Random();
                    sphereMaterial = new Lambertian(albedo);
                    world.Add(new Sphere(center, 0.2, sphereMaterial));
                }
                else if (chooseMat < 0.95)
                {
                    // metal
                    var albedo = Color.Random(0.5, 1);
                    double fuzz = Utility.RandomDouble(0, 0.5);
                    sphereMaterial = new Metal(albedo, fuzz);
                    world.Add(new Sphere(center, 0.2, sphereMaterial));
                }
                else
                {
                    // glass
                    sphereMaterial = new Dielectric(1.5);
                    world.Add(new Sphere(center, 0.2, sphereMaterial));
                }
            }
        }
    }

    var material1 = new Dielectric(1.5);
    world.Add(new Sphere(new Point3(0, 1, 0), 1, material1));

    var material2 = new Lambertian(new Color(0.4, 0.2, 0.1));
    world.Add(new Sphere(new Point3(-4, 1, 0), 1, material2));

    var material3 = new Metal(new Color(0.7, 0.6, 0.5), 0);
    world.Add(new Sphere(new Point3(4, 1, 0), 1, material3));

    return world;
}

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

const double aspectRatio  = 3.0 / 2.0;
const int imageWidth      = 1200;
const int imageHeight     = (int)(imageWidth / aspectRatio);
const int samplesPerPixel = 100;
const int maxDepth        = 50;

// World
var world = RandomScene();

// Camera
var lookFrom       = new Point3(13, 2, 3);
var lookAt         = new Point3(0, 0, 0);
var vUp            = new Vec3(0, 1, 0);
double distToFocus = 10;
double aperture    = 0.1;

var cam = new Camera(lookFrom, lookAt, vUp, 20, aspectRatio, aperture, distToFocus);

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