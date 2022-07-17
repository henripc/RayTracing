using System;
using System.Collections.Generic;

namespace RayTracing
{
    /// <summary>
    /// Represents the Bounding Volume Hierarchies container.
    /// </summary>
    public class BVHNode : IHittable
    {
        public AABB box;
        public IHittable left;
        public IHittable right;

        public BVHNode(HittableList list, double time0, double time1) : this(list.Objects, 0, list.Objects.Count, time0, time1) { }

        public BVHNode(List<IHittable> sourceObjects, int start, int end, double time0, double time1)
        {
            List<IHittable> objects = sourceObjects;     // Create a modifiable list of the source scene objects

            int axis = Utility.RandomInt(0, 2);
            var comparator = axis == 0 ? new BoxCompare(0)
                           : axis == 1 ? new BoxCompare(1)
                                       : new BoxCompare(2);

            int objectSpan = end - start;

            if (objectSpan == 1)
                left = right = objects[start];
            else if (objectSpan == 2)
            {
                if (comparator.Compare(objects[start], objects[start + 1]) <= 0)
                {
                    left = objects[start];
                    right = objects[start + 1];
                }
                else
                {
                    left = objects[start + 1];
                    right = objects[start];
                }
            }
            else
            {
                objects.Sort(start, end, comparator);

                var mid = start + objectSpan / 2;
                left = new BVHNode(objects, start, mid, time0, time1);
                right = new BVHNode(objects, mid, end, time0, time1);
            }

            var boxLeft = new AABB();
            var boxRight = new AABB();

            if (!left.BoundingBox(time0, time1, boxLeft) || !right.BoundingBox(time0, time1, boxRight))
                Console.Error.WriteLine("No bounding box in BVHNode constructor.");

            box = AABB.SurroundingBox(boxLeft, boxRight);
        }

        public bool Hit(Ray r, double tMin, double tMax, HitRecord rec)
        {
            if (!box.Hit(r, tMin, tMax))
                return false;

            bool hitLeft = left.Hit(r, tMin, tMax, rec);
            bool hitRight = right.Hit(r, tMin, hitLeft ? rec.t : tMax, rec);

            return hitLeft || hitRight;
        }

        public bool BoundingBox(double time0, double time1, AABB outputBox)
        {
            outputBox.Minimum = box.Minimum;
            outputBox.Maximum = box.Maximum;

            return true;
        }



    }

    /// <summary>
    /// <see langword="class"/> to compare <see cref="IHittable"/> objects based on the axis of choice.
    /// </summary>
    public class BoxCompare : Comparer<IHittable>
    {
        private readonly int _axis;

        /// <summary>
        /// Instantiate a <see cref="BoxCompare"/> object with the specified axis to be used in the comparison.
        /// </summary>
        /// <param name="axis"></param>
        public BoxCompare(int axis) => _axis = axis;

        public override int Compare(IHittable? x, IHittable? y)
        {
            var boxA = new AABB();
            var boxB = new AABB();

            if (!x!.BoundingBox(0, 0, boxA) || !y!.BoundingBox(0, 0, boxB))
                Console.Error.WriteLine("No bounding box in BVHNode constructor.");

            return boxA.Minimum[_axis].CompareTo(boxB.Minimum[_axis]);
        }
    }
}
