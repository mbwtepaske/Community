namespace System.Spatial
{
  using Collections.Generic;
  using Linq;

  /// <summary>
  /// Represents a frustum volume, that is defined by a collection of half-spaces (plane). 
  /// </summary>
  public class Frustum : Volume
  {
    public readonly Vector[] Planes;

    public Frustum(params Vector[] planes)
    {
      Planes = planes;
    }

    public static Boolean Contains(IEnumerable<Vector> planes, Vector vector)
    {
      if (planes == null)
      {
        throw new ArgumentNullException("planes");
      }

      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }
      
      return planes.All(plane => plane.DotProduct(vector) >= 0);
    }
  }
}