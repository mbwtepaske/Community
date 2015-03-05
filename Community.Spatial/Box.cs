namespace System.Spatial
{
  using Linq;

  /// <summary>
  /// Represents a box volume.
  /// </summary>
  public class Box : Volume
  {
    private readonly Lazy<Vector> _center;
    private readonly Lazy<Vector[]> _corners;

    public Vector Center
    {
      get
      {
        return _center.Value;
      }
    }

    public Vector[] Corners
    {
      get
      {
        return _corners.Value;
      }
    }

    public readonly Vector Maximum;
    public readonly Vector Minimum;

    public Box(Vector minimum, Vector maximum)
    {
      if (minimum == null)
      {
        throw new ArgumentNullException("minimum");
      }

      if (maximum == null)
      {
        throw new ArgumentNullException("maximum");
      }

      if (minimum.Count != maximum.Count)
      {
        throw new ArgumentException("minimum and maximum size must be the same");
      }

      _center = new Lazy<Vector>(GetCenter);
      _corners = new Lazy<Vector[]>(GetCorners);

      Minimum = minimum;
      Maximum = maximum;
    }

    private Vector GetCenter()
    {
      return Interpolation.Linear(Minimum, Maximum, 0.5D);
    }

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
        result[index] = new Vector(dimensions);

        for (var dimension = 0; dimension < dimensions; dimension++)
        {
          result[index][dimension] = (index / divisors[dimension]) % 2 == 0
            ? Minimum[dimension]
            : Maximum[dimension];
        }
      }

      return result;
    }

    /// <summary>
    /// Returns a box specifying a center and size.
    /// </summary>
    public static Box FromCenter(Vector center, Double size)
    {
      if (center == null)
      {
        throw new ArgumentNullException("center");
      }
      
      return FromCenter(center, new Vector(center.Count, size));
    }

    /// <summary>
    /// Returns a box specifying a center and size.
    /// </summary>
    public static Box FromCenter(Vector center, Vector size)
    {
      if (center == null)
      {
        throw new ArgumentNullException("center");
      }

      if (size == null)
      {
        throw new ArgumentNullException("size");
      }

      if (center.Count != size.Count)
      {
        throw new ArgumentException("center and radii size must be the same");
      }

      if (size.Any(value => value < 0D))
      {
        throw new ArgumentException("size elements must be zero or greater");
      }

      return new Box(center - size * 0.5, center + size * 0.5);
    }

    public override String ToString()
    {
      return String.Format("[{0}] - [{1}]", Minimum, Maximum);
    }
  }
}