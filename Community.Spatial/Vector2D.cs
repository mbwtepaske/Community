using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
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

    /// <summary>
    /// matrixs a 2D-vector with a 4x4-matrix.
    /// </summary>
    public static Vector Transform(Vector vector, Matrix matrix)
    {
      if (vector == null)
      {
        throw new ArgumentNullException(nameof(vector));
      }

      if (matrix == null)
      {
        throw new ArgumentNullException(nameof(matrix));
      }

      if (vector.Count != Size)
      {
        throw new ArgumentOutOfRangeException(nameof(vector));
      }

      if (matrix.ColumnCount != Matrix4D.Order || matrix.RowCount != Matrix4D.Order)
      {
        throw new ArgumentOutOfRangeException(nameof(matrix));
      }


      var x = vector[0] * matrix[0, 0] + vector[1] * matrix[1, 0] + matrix[3, 0];
      var y = vector[0] * matrix[0, 1] + vector[1] * matrix[1, 1] + matrix[3, 1];
      var w = x * matrix[0, 3] + y * matrix[1, 3] + matrix[3, 3];

      return Create(x / w, y / w);
    }
  }
}