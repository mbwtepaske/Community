using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  using Linq;

  /// <summary>
  /// Represents a N-Dimensional axis-aligned bounding-box volume.
  /// </summary>
  public class Box : Volume, ICollidable<Box>, ICollidable<Sphere>
  {
    private readonly Lazy<Vector> _center;
    private readonly Lazy<Vector[]> _corners;

    public Vector Center => _center.Value;

    public Vector[] Corners => _corners.Value;

    public readonly Vector Maximum;
    public readonly Vector Minimum;
    public readonly Matrix Transform;

    public Box(Vector minimum, Vector maximum, Matrix transform = null) : base(minimum.Count)
    {
      if (minimum == null)
      {
        throw new ArgumentNullException(nameof(minimum));
      }

      if (maximum == null)
      {
        throw new ArgumentNullException(nameof(maximum));
      }

      if (minimum.Count != maximum.Count)
      {
        throw new ArgumentException("minimum and maximum size must be the same");
      }

      _center = new Lazy<Vector>(GetCenter);
      _corners = new Lazy<Vector[]>(GetCorners);

      Minimum = minimum;
      Maximum = maximum;
      Transform = transform;
    }

    private Vector GetCenter() => Interpolation.Linear(Minimum, Maximum, 0.5D);

    private Vector[] GetCorners()
    {
      var dimensions = Maximum.Count;
      var divisors = new Int32[dimensions];

      for (var index = 0; index < dimensions; index++)
      {
        divisors[index] = index > 0
          ? divisors[index - 1] * 2
          : 1;
      }

      var result = new Vector[(Int32)Math.Pow(2, dimensions)];

      for (var index = 0; index < result.Length; index++)
      {
        result[index] = Vector.Build.Dense(dimensions);

        for (var dimension = 0; dimension < dimensions; dimension++)
        {
          result[index][dimension] = (index / divisors[dimension]) % 2 == 0
            ? Minimum[dimension]
            : Maximum[dimension];
        }
      }

      return result;
    }

    public override CollisionType Test(Vector point)
    {
      if (point == null)
      {
        throw new ArgumentNullException(nameof(point));
      }

      if (point.Count != Maximum.Count)
      {
        throw new DimensionMismatchException(nameof(point));
      }

      return point.Where((value, index) => value > Maximum[index] && value < Minimum[index]).Any() 
        ? CollisionType.Disjoint 
        : CollisionType.Contains;
    }

    public CollisionType Test(Sphere other)
    {
      throw new NotImplementedException();
    }

    public CollisionType Test(Box other)
    {
      if (other == null)
      {
        throw new ArgumentNullException(nameof(other));
      }

      if (Maximum.Count != other.Maximum.Count)
      {
        throw new DimensionMismatchException("other");
      }

      if (Maximum.Where((value, index) => value < other.Minimum[index] || Minimum[index] > other.Maximum[index]).Any())
      {
        return CollisionType.Disjoint;
      }

      return Maximum.Where((value, index) => Minimum[index] > other.Minimum[index] || value < other.Maximum[index]).Any() 
        ? CollisionType.Intersects 
        : CollisionType.Contains;
    }

    public override String ToString() => $"[{Minimum}] - [{Maximum}]";

    /// <summary>
    /// Returns a box specifying a center and size.
    /// </summary>
    public static Box FromCenter(Vector center, Double size)
    {
      if (center == null)
      {
        throw new ArgumentNullException(nameof(center));
      }

      return FromCenter(center, Vector.Build.Dense(center.Count, size));
    }

    /// <summary>
    /// Returns a box specifying a center and size.
    /// </summary>
    public static Box FromCenter(Vector center, Vector size)
    {
      if (center == null)
      {
        throw new ArgumentNullException(nameof(center));
      }

      if (size == null)
      {
        throw new ArgumentNullException(nameof(size));
      }

      if (center.Count != size.Count)
      {
        throw new ArgumentException("center and radii size must be the same");
      }

      if (size.Any(value => value < 0D))
      {
        throw new ArgumentException("size elements must be zero or greater");
      }

      return new Box(center - size, center + size);
    }
  }
}