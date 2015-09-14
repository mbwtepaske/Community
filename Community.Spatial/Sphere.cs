using MathNet.Numerics.LinearAlgebra;

using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  /// <summary>
  /// Represents a spherical volume.
  /// </summary>
  public sealed class Sphere : Volume, ICollidable<Sphere>
  {
    public readonly Vector Center;
    public readonly Double Radius;

    public Sphere(Vector center, Double radius = 1D) : base(center != null ? center.Count : 0)
    {
      if (center == null)
      {
        throw new ArgumentNullException(nameof(center));
      }

      if (radius < 0)
      {
        throw new ArgumentException("radius");
      }

      Center = center;
      Radius = radius;
    }

    public override CollisionType Test(Vector point)
    {
      if (point == null)
      {
        throw new ArgumentNullException(nameof(point));
      }

      return (point - Center).MagnitudeSquare() <= Radius * Radius 
        ? CollisionType.Contains 
        : CollisionType.Disjoint;
    }

    public CollisionType Test(Sphere other)
    {
      var distance = (Center - other.Center).Magnitude();

      if (Radius + other.Radius < distance)
      {
        return CollisionType.Disjoint;
      }

      return Radius - other.Radius < distance 
        ? CollisionType.Intersects 
        : CollisionType.Contains;
    }
  }
}