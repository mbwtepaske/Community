using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
namespace System.Spatial
{
  using Collections.Generic;
  using Linq;

  /// <summary>
  /// Represents a frustum volume, that is defined by a collection of half-spaces (plane). 
  /// </summary>
  public class Frustum : Volume, ICollidable<Box>, ICollidable<Sphere>, ICollidable<Frustum>
  {
    private readonly Lazy<Vector[]> _corners;

    public Vector[] Corners
    {
      get
      {
        return _corners.Value;
      }
    }

    public readonly Plane[] Planes;
    
    public Frustum(params Plane[] planes) : base(planes.Select(plane => plane.Normal.Count).Distinct().SingleOrDefault())
    {
      _corners = new Lazy<Vector[]>(GetCorners);

      Planes = planes;
    }

    public Vector[] GetCorners()
    {
      if (Dimensions > 3)
      {
        throw new NotSupportedException();
      }

      throw new NotImplementedException();

      //var planeCombinations = new List<Plane[]>();
      
      //Enumerable.Range(0, Dimensions).Select()

      //return null;
    }

    public override CollisionType Test(Vector point)
    {
      if (point == null)
      {
        throw new ArgumentNullException(nameof(point));
      }

      var result = CollisionType.Contains;

      foreach (var plane in Planes)
      {
        switch (Collision.Intersects(plane, point))
        {
          case PlaneIntersection.Back:
            return CollisionType.Disjoint;

          case PlaneIntersection.Intersecting:
            result = CollisionType.Intersects;
            break;

          case PlaneIntersection.Front:
            continue;
        }
      }

      return result;
    }

    private void GetBoxToPlanePVertexNVertex(Box box, Plane plane, out Vector position, out Vector normal)
    {
      if (box == null)
      {
        throw new ArgumentNullException(nameof(box));
      }

      if (plane == null)
      {
        throw new ArgumentNullException(nameof(plane));
      }

      position = box.Minimum;
      normal = box.Maximum;

      for (var index = 0; index < plane.Normal.Count; index++)
      {
        if (plane.Normal[index] >= 0)
        {
          position[index] = box.Maximum[index];
        }

        if (plane.Normal[index] >= 0)
        {
          normal[index] = box.Minimum[index];
        }
      }
    }

    public CollisionType Test(Box other)
    {
      var containmentType = CollisionType.Contains;

      foreach (var plane in Planes)
      {
        Vector position;
        Vector normal;
        
        GetBoxToPlanePVertexNVertex(other, plane, out position, out normal);

        if (Collision.Intersects(plane, position) == PlaneIntersection.Back)
        {
          return CollisionType.Disjoint;
        }

        if (Collision.Intersects(plane, normal) == PlaneIntersection.Back)
        {
          containmentType = CollisionType.Intersects;
        }
      }

      return containmentType;
    }

    public CollisionType Test(Sphere other)
    {
      var result = CollisionType.Contains;

      foreach (var plane in Planes)
      {
        switch (Collision.Intersects(plane, other))
        {
          case PlaneIntersection.Back:
            return CollisionType.Disjoint;

          case PlaneIntersection.Intersecting:
            result = CollisionType.Intersects;
            break;

          case PlaneIntersection.Front:
            continue;
        }
      }

      return result;
    }

    public CollisionType Test(Frustum other)
    {
      throw new NotImplementedException();
    }
  }
}