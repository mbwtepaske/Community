﻿namespace System.Spatial
{
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
      return new Vector(Size, value);
    }

    /// <summary>
    /// Returns a 2D-vector using the specified values for as components.
    /// </summary>
    public static Vector Create(Double x, Double y)
    {
      return new Vector(x, y);
    }

    /// <summary>
    /// Returns a 2D-vector transformed by a 4x4-matrix.
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


      var x = vector[0] * matrix[0, 0] + vector[1] * matrix[0, 1] + matrix[0, 3];
      var y = vector[0] * matrix[1, 0] + vector[1] * matrix[1, 1] + matrix[1, 3];
      var w = x * matrix[3, 0] + y * matrix[3, 1] + matrix[3, 3];

      return Create(x / w, y / w);
    }
  }
}