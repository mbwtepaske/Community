﻿namespace System.Spatial
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
      return new Vector(Size, defaultValue);
    }

    /// <summary>
    /// Returns a 3D-vector using the specified value for each of components.
    /// </summary>
    public static Vector Create(Double x, Double y, Double z)
    {
      return new Vector(new[] {x, y, z});
    }

    /// <summary>
    /// Returns a 3D-vector using the specified values for the first components and the defaultValue for the remainder.
    /// </summary>
    public static Vector Create(IEnumerable<Double> values, Double defaultValue = 0D)
    {
      if (values == null)
      {
        throw new ArgumentNullException(nameof(values));
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
        throw new ArgumentNullException(nameof(left));
      }

      if (right == null)
      {
        throw new ArgumentNullException(nameof(right));
      }

      if (left.Count != right.Count)
      {
        throw new ArgumentException("right must be of the same size of left");
      }

      if (result == null)
      {
        result = new Vector(left.Count);
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
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    public static Double Distance(Vector left, Vector right)
    {
      return Math.Sqrt(DistanceSquared(left, right));
    }

    /// <summary>
    /// Returns the Euclidean distance squared between the two given points. This method is faster than <see cref="T:Vector3D.Distance"/>.
    /// </summary>
    public static Double DistanceSquared(Vector left, Vector right)
    {
      var x = left[0] - right[0];
      var y = left[1] - right[1];
      var z = left[2] - right[2];

      return x * x + y * y + z * z;
    }

    /// <summary>
    /// Transforms a 3D-coordinate with a 4x4-matrix.
    /// </summary>
    public static Vector TransformCoordinate(Vector vector, Matrix matrix)
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

      var x = vector[0] * matrix[0, 0] + vector[1] * matrix[0, 1] + vector[2] * matrix[0, 2] + matrix[0, 3];
      var y = vector[0] * matrix[1, 0] + vector[1] * matrix[1, 1] + vector[2] * matrix[1, 2] + matrix[1, 3];
      var z = vector[0] * matrix[2, 0] + vector[1] * matrix[2, 1] + vector[2] * matrix[2, 2] + matrix[2, 3];
      var w = x * matrix[0, 3] + y * matrix[1, 3] + z * matrix[2, 3] + matrix[3, 3];

      return Create(x / w, y / w, z / w);
    }
  }
}