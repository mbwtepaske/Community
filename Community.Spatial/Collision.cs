using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  using Linq;

  public static class Collision
  {
    public static Boolean Contains(Box box, Vector point)
    {
      if (box == null)
      {
        throw new ArgumentNullException("box");
      }

      if (point == null)
      {
        throw new ArgumentNullException("point");
      }

      if (box.Maximum.Count != point.Count)
      {
        throw new DimensionMismatchException("point");
      }

      for (var index = 0; index < point.Count; index++)
      {
        var value = point[index];

        if (value <= box.Minimum[index] || value >= box.Maximum[index])
        {
          return false;
        }
      }

      return true;
    }

    /*
    public static CollisionType Test(Box left, Box right)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (left.Maximum.Count != right.Maximum.Count)
      {
        throw new DimensionMismatchException("right");
      }

      var result = CollisionType.Contains;

      for (var index = 0; index < left.Maximum.Count; index++)
      {
        if (left.Minimum[index] > right.Maximum[index] || left.Maximum[index] < right.Minimum[index])
        {
          return CollisionType.Disjoint;
        }

        if ((left.Minimum[index] < right.Minimum[index] && left.Maximum[index] < right.Maximum[index]) || (left.Minimum[index] < right.Minimum[index] && left.Maximum[index] < right.Maximum[index]))
        {
          result 
        }
      }

      return result;
    }
    
    public static Boolean Intersects(Box left, Box right)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (left.Maximum.Count != right.Maximum.Count)
      {
        throw new DimensionMismatchException("right");
      }

      for (var index = 0; index < left.Maximum.Count; index++)
      {
        if (left.Minimum[index] > right.Maximum[index] || right.Minimum[index] > left.Maximum[index])
        {
          return false;
        }
      }

      return true;
    }
    */
    
    public static PlaneIntersection Intersects(Vector plane, Box box)
    {
      if (plane == null)
      {
        throw new ArgumentNullException("plane");
      }

      if (box == null)
      {
        throw new ArgumentNullException("box");
      }

      if (box.Maximum.Count != plane.Count - 1)
      {
        throw new DimensionMismatchException("box");
      }

      var maximum = 0D;
      var minimum = 0D;

      for (var index = 0; index < plane.Count - 1; index++)
      {
        var current = plane[index];

        maximum += current * (current >= 0
          ? box.Minimum[index]
          : box.Maximum[index]);
        minimum += current * (current >= 0
          ? box.Maximum[index]
          : box.Minimum[index]);
      }

      if (minimum + plane.Last() > 0)
      {
        return PlaneIntersection.Front;
      }
      
      return maximum + plane.Last() < 0
        ? PlaneIntersection.Back
        : PlaneIntersection.Intersecting;
    }

    /// <summary>
    /// Determines whether there is an intersection between a ray and a plane.
    /// </summary>
    public static Boolean Intersects(Ray ray, Vector plane, out Double distance)
    {
      var directionDotProduct = plane.SubVector(0, ray.Direction.Count).DotProduct(ray.Direction);

      if (DoubleComparison.Nano.Equals(directionDotProduct, 0D))
      {
        distance = 0D;

        return false;
      }

      var positionDotProduct = plane.SubVector(0, ray.Position.Count).DotProduct(ray.Position);

      distance = (-plane.Last() - positionDotProduct) / directionDotProduct;

      if (distance >= 0D)
      {
        return true;
      }

      distance = 0D;

      return false;
    }

    /// <summary>
    /// Determines whether there is an intersection between a ray and a plane.
    /// </summary>
    public static Boolean Intersects(Ray ray, ref Vector plane, out Vector point)
    {
      var distance = 0D;

      if (!Intersects(ray, plane, out distance))
      {
        point = null;

        return false;
      }
      
      point = ray.Position + ray.Direction * distance;
     
      return true;
    }
  }
}