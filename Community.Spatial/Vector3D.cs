using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  using Collections.Generic;
  using Linq;

  /// <summary>
  /// Operations and constants for <see cref="T:Vector"/>s that only contain 3 dimensions.
  /// </summary>
  public static class Vector3D
  {
    public const Int32 Size = 3;

    public static readonly Vector One = Create(1D);
    public static readonly Vector UnitX = Create(1D, 0D, 0D);
    public static readonly Vector UnitY = Create(0D, 1D, 0D);
    public static readonly Vector UnitZ = Create(0D, 0D, 1D);
    public static readonly Vector Zero = Create();

    /// <summary>
    /// Returns a 3D-vector using the specified <paramref name="defaultValue"/> for each component.
    /// </summary>
    public static Vector Create(Double defaultValue = 0D)
    {
      return Vector.Build.Dense(Size, defaultValue);
    }

    /// <summary>
    /// Returns a 3D-vector using the specified value for each of components.
    /// </summary>
    public static Vector Create(Double x, Double y, Double z)
    {
      return Vector.Build.Dense(new[] {x, y, z});
    }

    /// <summary>
    /// Returns a 3D-vector using the specified values for the first components and the defaultValue for the remainder.
    /// </summary>
    public static Vector Create(IEnumerable<double> values, Double defaultValue = 0D)
    {
      if (values == null)
      {
        throw new ArgumentNullException("values");
      }

      var result = Create(defaultValue);

      values.Take(Size).Invoke((value, index) => result[index] = value);

      return result;
    }

    /// <summary>
    /// Returns the cross product.
    /// </summary>
    public static Vector Cross(Vector left, Vector right)
    {
      var result = default(Vector);
      
      return Cross(left, right, ref result);
    }

    public static Vector Cross(Vector left, Vector right, ref Vector result)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (left.Count != right.Count)
      {
        throw new ArgumentException("right must be of the same size of left");
      }

      if (result == null)
      {
        result = Vector.Build.Dense(left.Count);
      }
      else
      {
        if (left.Count != result.Count)
        {
          throw new ArgumentException("result must be of the same size of left");
        }
      }

      result[0] = left[1] * right[2] - left[2] * right[1];
      result[1] = left[2] * right[0] - left[0] * right[2];
      result[2] = left[0] * right[1] - left[1] * right[0];

      return result;
    }
    
    /// <summary>
    /// Transforms a 3D-coordinate with a 4x4-matrix.
    /// </summary>
    public static Vector TransformCoordinate(Vector vector, Matrix matrix)
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }

      if (matrix == null)
      {
        throw new ArgumentNullException("matrix");
      }

      if (vector.Count != Size)
      {
        throw new ArgumentOutOfRangeException("vector");
      }

      if (matrix.ColumnCount != Matrix4D.Order || matrix.RowCount != Matrix4D.Order)
      {
        throw new ArgumentOutOfRangeException("matrix");
      }

      var values = Enumerable
        .Range(0, matrix.ColumnCount)
        .Select(index => vector.Zip(matrix.Column(index), (left, right) => left * right).Sum() + matrix[index, Size])
        .ToArray();

      return Create(values.Take(Size)).Multiply(1D / values.Last()); // (x, y, z) * (w ^ -1)
    }
  }
}