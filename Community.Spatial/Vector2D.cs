using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  using Linq;

  /// <summary>
  /// Operations and constants for <see cref="T:Vector"/>s that only contain 2 dimensions.
  /// </summary>
  public static class Vector2D
  {
    public const Int32 Size = 2;

    public static readonly Vector UnitX = Create(1D, 0D);
    public static readonly Vector UnitY = Create(0D, 1D);
    public static readonly Vector Zero = Create();

    /// <summary>
    /// Returns a 2D-vector using the specified value for each component.
    /// </summary>
    public static Vector Create(Double value = 0D)
    {
      return Vector.Build.DenseOfEnumerable(Enumerable.Repeat(value, Size));
    }

    /// <summary>
    /// Returns a 2D-vector using the specified values for as components.
    /// </summary>
    public static Vector Create(Double x, Double y)
    {
      return Vector.Build.Dense(new [] { x, y });
    }
  }
}