namespace System.Spatial
{
  public class Plane<TVector> where TVector : Vector
  {
    /// <summary>
    /// Gets the distance of the plane from the origin.
    /// </summary>
    public Double Distance
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets the normal of the plane.
    /// </summary>
    public TVector Normal
    {
      get;
      private set;
    }

    /// <summary>
    /// Initializes a plane.
    /// </summary>
    /// <param name="direction">The direction of the plane normal.</param>
    /// <param name="distance">The offset from the origin along the normal axis.</param>
    public Plane(TVector direction, Double distance = 0D)
    {
      if (direction == null)
      {
        throw new ArgumentNullException("direction");
      }

      Distance = distance;
      Normal = (TVector)Vector.Normalize(direction);
    }

    /// <summary>
    /// Initializes a plane.
    /// </summary>
    /// <param name="point">A point on the plane.</param>
    /// <param name="direction">The direction of the plane normal.</param>
    public Plane(Vector point, Vector direction)
    {
      if (point == null)
      {
        throw new ArgumentNullException("point");
      }

      if (direction == null)
      {
        throw new ArgumentNullException("direction");
      }

      if (point.Count != direction.Count)
      {
        throw new ArgumentException("point and direction are different in size");
      }

      Distance = -Vector.Dot(direction, point);
      Normal = (TVector)Vector.Normalize(direction);
    }

    public Double Dot(Vector vector)
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }

      if (Normal.Count != vector.Count)
      {
        throw new ArgumentException("vector must be the same dimension as the plane");
      }

      return vector.IsNormal
        ? Vector.Dot(Normal, vector) + Distance
        : Vector.Dot(Normal, vector);
    }
  }

  public class Plane : Plane<Vector>
  {
    public Plane(Vector direction, double distance = 0)
      : base(direction, distance)
    {
    }

    public Plane(Vector point, Vector direction)
      : base(point, direction)
    {
    }
  }
}