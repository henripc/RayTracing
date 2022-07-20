using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RayTracing;

using Point3 = RayTracing.Vec3;
using Color = RayTracing.Vec3;

static HittableList RandomScene()
{
    var world = new HittableList();

    var checker = new CheckerTexture(new Color(0.2, 0.3, 0.1), new Color(0.9, 0.9, 0.9));
    world.Add(new Sphere(new Point3(0, -1000, 0), 1000, new Lambertian(checker)));

    for (int a = -11; a < 11; a++)
    {
        for (int b = -11; b < 11; b++)
        {
            double chooseMat = Utility.RandomDouble();
            var center = new Point3(a + 0.9 * Utility.RandomDouble(), 0.2, b + 0.9 * Utility.RandomDouble());

            if ((center - new Point3(4, 0.2, 0)).Length() > 0.9)
            {
                IMaterial sphereMaterial;

                if (chooseMat < 0.8)
                {
                    // diffuse
                    var albedo = Color.Random() * Color.Random();
                    sphereMaterial = new Lambertian(albedo);
                    var center2 = center + new Vec3(0, Utility.RandomDouble(0, 0.5), 0);
                    world.Add(new MovingSphere(center, center2, 0d, 1d, 0.2, sphereMaterial));
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

static HittableList TwoSpheres()
{
    var objects = new HittableList();

    var checker = new CheckerTexture(new Color(0.2, 0.3, 0.1), new Color(0.9, 0.9, 0.9));

    objects.Add(new Sphere(new Point3(0, -10, 0), 10, new Lambertian(checker)));
    objects.Add(new Sphere(new Point3(0, 10, 0), 10, new Lambertian(checker)));

    return objects;
}

static HittableList TwoPerlinSpheres()
{
    var objects = new HittableList();

    var perText = new NoiseTexture(4);

    objects.Add(new Sphere(new Point3(0, -1000, 0), 1000, new Lambertian(perText)));
    objects.Add(new Sphere(new Point3(0, 2, 0), 2, new Lambertian(perText)));

    return objects;
}

static HittableList SimpleLight()
{
    var objects = new HittableList();

    var perText = new NoiseTexture(4);
    objects.Add(new Sphere(new Point3(0, -1000, 0), 1000, new Lambertian(perText)));
    objects.Add(new Sphere(new Point3(0, 2, 0), 2, new Lambertian(perText)));

    var diffLight = new DiffuseLight(new Color(4, 4, 4));
    objects.Add(new RectangleXY(3, 5, 1, 3, -2, diffLight));

    return objects;
}

static HittableList CornerllBox()
{
    var objects = new HittableList();

    var red = new Lambertian(new Color(0.65, 0.05, 0.05));
    var white = new Lambertian(new Color(0.73, 0.73, 0.73));
    var green = new Lambertian(new Color(0.12, 0.45, 0.15));
    var light = new DiffuseLight(new Color(15, 15, 15));

    objects.Add(new RectangleYZ(0, 555, 0, 555, 555, green));
    objects.Add(new RectangleYZ(0, 555, 0, 555, 0, red));
    objects.Add(new RectangleXZ(213, 343, 227, 332, 554, light));
    objects.Add(new RectangleXZ(0, 555, 0, 555, 0, white));
    objects.Add(new RectangleXZ(0, 555, 0, 555, 555, white));
    objects.Add(new RectangleXY(0, 555, 0, 555, 555, white));

    return objects;
}

static Color RayColor(Ray r, Color background, HittableList world, int depth)
{
    var rec = new HitRecord();

    // If we've exceeded the ray bounce limit, no more light is gathered.
    if (depth <= 0)
        return new Color(0, 0, 0);

    // If the ray hits nothing, return the background color.
    if (!world.Hit(r, 0.001, Utility.INFINITY, rec))
        return background;

    var scattered = new Ray();
    var attenuation = new Color();
    Color emitted = rec!.mat!.Emitted(rec.u, rec.v, rec.p!);

    if (!rec.mat.Scatter(r, rec, attenuation, scattered))
        return emitted;

    return emitted + attenuation * RayColor(scattered, background, world, depth - 1);
}

// Image

double aspectRatio  = 16.0 / 9.0;
int imageWidth      = 400;
int imageHeight     = (int)(imageWidth / aspectRatio);
int samplesPerPixel = 100;
const int maxDepth  = 50;

// World
HittableList world;

Point3 lookFrom;
Point3 lookAt;
double vFov     = 40.0;
double aperture = 0;
var background = new Color(0, 0, 0);

switch (0)
{
    case 1:
        world = RandomScene();
        background = new Color(0.7, 0.8, 1);
        lookFrom = new Point3(13, 2, 3);
        lookAt = new Point3(0, 0, 0);
        vFov = 20.0;
        aperture = 0.1;
        break;

    case 2:
        world = TwoSpheres();
        background = new Color(0.7, 0.8, 1);
        lookFrom = new Point3(13, 2, 3);
        lookAt = new Point3(0, 0, 0);
        vFov = 20.0;
        break;

    case 3:
        world = TwoPerlinSpheres();
        background = new Color(0.7, 0.8, 1);
        lookFrom = new Point3(13, 2, 3);
        lookAt = new Point3(0, 0, 0);
        vFov = 20.0;
        break;

    case 5:
        world = SimpleLight();
        samplesPerPixel = 400;
        background = new Color(0, 0, 0);
        lookFrom = new Point3(26, 3, 6);
        lookAt = new Point3(0, 2, 0);
        vFov = 20.0;
        break;

    default:
    case 6:
        world = CornerllBox();
        aspectRatio = 1d;
        imageWidth = 600;
        samplesPerPixel = 200;
        background = new Color(0, 0, 0);
        lookFrom = new Point3(278, 278, -800);
        lookAt = new Point3(278, 278, 0);
        vFov = 40.0;
        break;
}


// Camera
var vUp            = new Vec3(0, 1, 0);
double distToFocus = 10;

var cam = new Camera(lookFrom, lookAt, vUp, vFov, aspectRatio, aperture, distToFocus, 0d, 1d);

// Render
var stringBuilder = new StringBuilder();
stringBuilder.Append($"P3\n{ imageWidth } { imageHeight }\n255\n");

for (int j = imageHeight - 1; j >= 0; j--)
{
    Console.Error.WriteLine($"Scanlines remaining: {j}");
    for (int i = 0; i < imageWidth; i++)
    {
        var taskResult = await Task.WhenAll(Enumerable.Range(0, samplesPerPixel).Select(_ =>
        {
            return Task.Run(() =>
            {
                double u = (i + Utility.RandomDouble()) / (imageWidth - 1);
                double v = (j + Utility.RandomDouble()) / (imageHeight - 1);
                Ray r = cam.GetRay(u, v);

                return RayColor(r, background, world, maxDepth);
            });
        }));

        Color pixelColor = taskResult.Aggregate((acc, color) => acc + color);
        stringBuilder.Append(ColorUtils.WriteColor(pixelColor, samplesPerPixel));
    }
}

File.WriteAllText("./images/book2/image_12.ppm", stringBuilder.ToString());

Console.Error.WriteLine("Done.");