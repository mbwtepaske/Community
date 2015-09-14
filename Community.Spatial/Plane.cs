namespace System.Spatial
{
  using Linq;

  public class Plane
  {
    public readonly Double Distance;
    public readonly Vector Normal;

    public Plane(params Double[] values)
    {
      if (values.Length < 2)
      {
        throw new ArgumentOutOfRangeException(nameof(values));
      }

      Normal = Vector.Build.Dense(values.Length - 1, index => values[index]);
      Distance = values.Last();
    }

    public Plane(Vector normal, Double distance = 0D)
    {
      if (normal == null)
      {
        throw new ArgumentNullException(nameof(normal));
      }

      Normal = normal;
      Distance = distance;
    }

    public Plane(Vector point, Vector normal)
    {
      if (point == null)
      {
        throw new ArgumentNullException(nameof(point));
      }

      if (normal == null)
      {
        throw new ArgumentNullException(nameof(normal));
      }

      Normal = normal;
      Distance = -normal.DotProduct(point);
    }

    public virtual Double DotCoordinate(Vector coordinate)
    {
      if (coordinate == null)
      {
        throw new ArgumentNullException(nameof(coordinate));
      }
      
      return Normal.DotProduct(coordinate) + Distance;
    }

    public virtual Double DotNormal(Vector normal)
    {
      if (normal == null)
      {
        throw new ArgumentNullException(nameof(normal));
      }

      return Normal.DotProduct(normal);
    }

    public Plane Normalize()
    {
      var magnitude = Normal.Magnitude();

      return new Plane(Normal.Divide(magnitude), Distance / magnitude);
    }

    public static Double operator *(Vector vector, Plane plane)
    {
      return plane.DotCoordinate(vector);
    }
  }
}