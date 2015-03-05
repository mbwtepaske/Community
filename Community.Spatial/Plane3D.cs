namespace System.Spatial
{
  using Linq;

  public static class Plane3D
  {
    /// <summary>
    /// Returns the dot-product of the 3D-plane with a 3D-coordinate.
    /// </summary>
    public static Double DotCoordinate(Vector plane, Vector coordinate)
    {
      return DotNormal(plane, coordinate) + plane.Last();
    }

    /// <summary>
    /// Returns the dot-product of the 3D-plane with a 3D-normal.
    /// </summary>
    public static Double DotNormal(Vector plane, Vector coordinate)
    {
      if (plane == null)
      {
        throw new ArgumentNullException("plane");
      }

      if (plane.Count != Vector4D.Size)
      {
        throw new ArgumentOutOfRangeException("plane");
      }

      if (coordinate == null)
      {
        throw new ArgumentNullException("coordinate");
      }

      if (coordinate.Count != Vector3D.Size)
      {
        throw new ArgumentOutOfRangeException("coordinate");
      }

      return plane.Zip(coordinate, Multiply).Sum();
    }

    /// <summary>
    /// Returns a vector representing a plane using a point and a normal.
    /// </summary>
    public static Vector FromPointAndNormal(Vector point, Vector normal)
    {
      if (point == null)
      {
        throw new ArgumentNullException("point");
      }

      if (normal == null)
      {
        throw new ArgumentNullException("normal");
      }

      if (point.Count != normal.Count)
      {
        throw new InvalidOperationException("point and normal are not equal in size");
      }

      return new Vector(normal.Append(-normal.DotProduct(point)).ToArray());
    }

    private static Double Multiply(Double left, Double right)
    {
      return left * right;
    }
  }
}