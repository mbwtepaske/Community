using System;
using System.Numerics;

namespace MathNet.Numerics.LinearAlgebra
{
  public static class MatrixExtensions
  {
    /// <summary>
    /// Returns whether this matrix is a square matrix, where the column and row count are the same.
    /// </summary>
    public static Boolean IsSquare<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable
    {
      return matrix.ColumnCount == matrix.RowCount;
    }

    public static Matrix3x2 ToMatrix3x2(this Matrix<double> matrix)
    {
      var result = default(Matrix3x2);

      ToMatrix3x2(matrix, out result);

      return result;
    }

    public static void ToMatrix3x2(this Matrix<double> matrix, out Matrix3x2 result)
    {
      result.M11 = Convert.ToSingle(matrix[0, 0]);  result.M12 = Convert.ToSingle(matrix[0, 1]);
      result.M21 = Convert.ToSingle(matrix[1, 0]);  result.M22 = Convert.ToSingle(matrix[1, 1]);
      result.M31 = Convert.ToSingle(matrix[2, 0]);  result.M32 = Convert.ToSingle(matrix[2, 1]);
    }

    public static Matrix4x4 ToMatrix4x4(this Matrix<double> matrix)
    {
      var result = default(Matrix4x4);

      ToMatrix4x4(matrix, out result);

      return result;
    }

    public static void ToMatrix4x4(this Matrix<double> matrix, out Matrix4x4 result)
    {
      result.M11 = Convert.ToSingle(matrix[0, 0]);  result.M12 = Convert.ToSingle(matrix[0, 1]);  result.M13 = Convert.ToSingle(matrix[0, 2]);  result.M14 = Convert.ToSingle(matrix[0, 3]);
      result.M21 = Convert.ToSingle(matrix[1, 0]);  result.M22 = Convert.ToSingle(matrix[1, 1]);  result.M23 = Convert.ToSingle(matrix[1, 2]);  result.M24 = Convert.ToSingle(matrix[1, 3]);
      result.M31 = Convert.ToSingle(matrix[2, 0]);  result.M32 = Convert.ToSingle(matrix[2, 1]);  result.M33 = Convert.ToSingle(matrix[2, 2]);  result.M34 = Convert.ToSingle(matrix[2, 3]);
      result.M41 = Convert.ToSingle(matrix[3, 0]);  result.M42 = Convert.ToSingle(matrix[3, 1]);  result.M43 = Convert.ToSingle(matrix[3, 2]);  result.M44 = Convert.ToSingle(matrix[3, 3]);
    }
  }
  //public static class MatrixExtensions
  //{
  //  /// <summary>
  //  /// Returns whether this matrix is a square matrix, where the column and row count are the same.
  //  /// </summary>
  //  public static Boolean IsSquare<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable
  //  {
  //    return matrix.ColumnCount == matrix.RowCount;
  //  }
  //}
}