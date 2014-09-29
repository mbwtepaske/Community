using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public partial class Matrix
  {
    public static IEnumerable<Double> GetColumn(Matrix matrix, Int32 columnIndex)
    {
      for (var rowIndex = 0; rowIndex < matrix.RowCount; rowIndex++)
      {
        yield return matrix[columnIndex, rowIndex];
      }
    }

    public static IEnumerable<Double> GetRow(Matrix matrix, Int32 rowIndex)
    {
      for (var columnIndex = 0; columnIndex < matrix.ColumnCount; columnIndex++)
      {
        yield return matrix[columnIndex, rowIndex];
      }
    }

    private static Double Multiply(Double left, Double right)
    {
      return left * right;
    }

    public static Matrix Multiply(Matrix left, Double right)
    {
      var result = default(Matrix);

      return Multiply(left, right, out result);
    }

    public static Matrix Multiply(Matrix matrix, Double right, out Matrix result)
    {
      VerifyMatrix(matrix);

      return result = new Matrix(matrix.ColumnCount, matrix.RowCount, matrix.Select(value => value * right).ToArray());
    }

    public static Matrix Multiply(Matrix left, Matrix right)
    {
      var result = default(Matrix);

      return Multiply(left, right, out result);
    }

    public static Matrix Multiply(Matrix left, Matrix right, out Matrix result)
    {
      VerifyLeftAndRight(left, right);

      if (left.RowCount != right.ColumnCount)
      {
        throw new ArgumentException("left.ColumnCount <> right.RowCount");
      }

      result = new Matrix(right.ColumnCount, left.RowCount);

      for (var rowIndex = 0; rowIndex < result.RowCount; rowIndex++)
      {
        for (var columnIndex = 0; columnIndex < result.ColumnCount; columnIndex++)
        {
          result[columnIndex, rowIndex] = GetColumn(left, columnIndex).Zip(GetRow(right, rowIndex), Multiply).Sum();
        }
      }

      // result.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41;
      // result.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42;
      // result.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43;
      // result.M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44;
      // result.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41;
      // result.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42;
      // result.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43;
      // result.M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44;
      // result.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41;
      // result.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42;
      // result.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43;
      // result.M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44;
      // result.M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41;
      // result.M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42;
      // result.M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43;
      // result.M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44;

      return result;
    }

    private static void VerifyMatrix(IMatrix matrix)
    {
      if (matrix == null)
      {
        throw new ArgumentNullException("matrix");
      }
    }

    private static void VerifyLeftAndRight(IMatrix left, IMatrix right, Boolean sameSize = false)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (sameSize)
      {
        if (left.ColumnCount != right.ColumnCount)
        {
          throw new ArgumentException("left.ColumnCount != right.ColumnCount");
        }
        if (left.RowCount != right.RowCount)
        {
          throw new ArgumentException("left.RowCount != right.RowCount");
        }
      }
    }

    public static Matrix operator *(Matrix left, Double right)
    {
      return Multiply(left, right);
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
      return Multiply(left, right);
    }

    public static implicit operator Double[] (Matrix matrix)
    {
      return matrix.ToArray();
    }

    public static implicit operator Single[] (Matrix matrix)
    {
      return matrix.Select(Convert.ToSingle).ToArray();
    }
  }
}
