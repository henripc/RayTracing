using System.Collections.Generic;

namespace RayTracing
{
    /// <summary>
    /// Stores a list of <see cref="Hittable"/> objects.
    /// </summary>
    public class HittableList : Hittable
    {
        /// <summary>
        /// The List of <see cref="Hittable"/> objects.
        /// </summary>
        public List<Hittable> Objects { get; set; }

        public HittableList() => Objects = new List<Hittable>();
        public HittableList(Hittable obj) => Objects = new List<Hittable>() { obj };

        public void Clear() => Objects.Clear();
        public void Add(Hittable obj) => Objects.Add(obj);

        public override bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec)
        {
            var tempRec = new HitRecord();
            bool hitAnything = false;
            var closestSoFar = tMax;

            foreach (var obj in Objects)
            {
                if (obj.Hit(r, tMin, closestSoFar, tempRec))
                {
                    hitAnything = true;
                    closestSoFar = tempRec.t;
                    rec = tempRec;
                }
            }

            return hitAnything;
        }
    }
}
