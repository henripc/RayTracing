using System.Collections.Generic;

namespace RayTracing
{
    /// <summary>
    /// <see langword="class"/> that represents a list of <see cref="IHittable"/> objects.
    /// </summary>
    public class HittableList : IHittable
    {
        /// <summary>
        /// The List of <see cref="IHittable"/> objects.
        /// </summary>
        public ICollection<IHittable> Objects { get; set; }

        /// <summary>
        /// Instanciate a new empty list of <see cref="IHittable"/> objects.
        /// </summary>
        public HittableList() => Objects = new List<IHittable>();

        /// <summary>
        /// Instanciate a new list of <see cref="IHittable"/> objects with the given object.
        /// </summary>
        public HittableList(IHittable obj) => Objects = new List<IHittable>() { obj };

        /// <summary>
        /// Remove all objects from the list.
        /// </summary>
        public void Clear() => Objects.Clear();

        /// <summary>
        /// Add the object to the list.
        /// </summary>
        /// <param name="obj"></param>
        public void Add(IHittable obj) => Objects.Add(obj);

        public bool Hit(Ray r, double tMin, double tMax, HitRecord rec)
        {
            var tempRec = new HitRecord();
            bool hitAnything = false;
            var closestSoFar = tMax;

            foreach (IHittable obj in Objects)
            {
                if (obj.Hit(r, tMin, closestSoFar, tempRec))
                {
                    hitAnything = true;
                    closestSoFar = tempRec.t;

                    rec.p = tempRec.p;
                    rec.normal = tempRec.normal;
                    rec.mat = tempRec.mat;
                    rec.t = tempRec.t;
                    rec.frontFace = tempRec.frontFace;
                }
            }

            return hitAnything;
        }

        public bool BoundingBox(double time0, double time1, AABB outputBox)
        {
            if (Objects.Count == 0)
                return false;

            var tempBox = new AABB();
            var firstBox = true;

            foreach (IHittable obj in Objects)
            {
                if (!obj.BoundingBox(time0, time1, tempBox))
                    return false;

                if (firstBox)
                {
                    outputBox.Minimum = tempBox.Minimum;
                    outputBox.Maximum = tempBox.Maximum;
                }
                else
                {
                    AABB localBox = AABB.SurroundingBox(outputBox, tempBox);
                    outputBox.Minimum = localBox.Minimum;
                    outputBox.Maximum = localBox.Maximum;
                }

                firstBox = false;
            }

            return true;
        }
    }
}
