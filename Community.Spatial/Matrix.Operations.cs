using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public partial class Matrix
  {
    #region Arithmetics

    #region Addition

    public static Matrix Add(Matrix left, Double right)
    {
      var result = default(Matrix);

      return Add(left, right, ref result);
    }

    public static Matrix Add(Matrix matrix, Double right, ref Matrix result)
    {
      VerifyMatrix(matrix);

      return result = new Matrix(matrix.ColumnCount, matrix.RowCount, matrix.Select(value => value + right).ToArray());
    }

    public static Matrix Add(Matrix left, Matrix right)
    {
      var result = default(Matrix);

      return Add(left, right, ref result);
    }

    public static Matrix Add(Matrix left, Matrix right, ref Matrix result)
    {
      VerifyLeftAndRightAndResult(left, right, ref result, true);
      
      for (var index = 0; index < result.Storage.Length; index++)
      {
        result[index] = left[index] + right[index];
      }

      return result;
    }

    #endregion

    #region Division

    public static Matrix Divide(Matrix left, Double right)
    {
      var result = default(Matrix);

      return Divide(left, right, ref result);
    }

    public static Matrix Divide(Matrix matrix, Double right, ref Matrix result)
    {
      VerifyMatrix(matrix);

      return result = new Matrix(matrix.ColumnCount, matrix.RowCount, matrix.Select(value => value / right).ToArray());
    }

    public static Matrix Divide(Matrix left, Matrix right)
    {
      var result = default(Matrix);

      return Divide(left, right, ref result);
    }

    public static Matrix Divide(Matrix left, Matrix right, ref Matrix result)
    {
      VerifyLeftAndRightAndResult(left, right, ref result, true);

      for (var index = 0; index < result.Storage.Length; index++)
      {
        result[index] = left[index] / right[index];
      }

      return result;
    }

    #endregion

    #region Multiplication

    private static Double Multiply(Double left, Double right)
    {
      return left * right;
    }

    public static Matrix Multiply(Matrix left, Double right)
    {
      var result = default(Matrix);

      return Multiply(left, right, ref result);
    }

    public static Matrix Multiply(Matrix matrix, Double right, ref Matrix result)
    {
      VerifyMatrix(matrix);

      return result = new Matrix(matrix.ColumnCount, matrix.RowCount, matrix.Select(value => value * right).ToArray());
    }

    public static Matrix Multiply(Matrix left, Matrix right)
    {
      var result = default(Matrix);

      return Multiply(left, right, ref result);
    }

    public static Matrix Multiply(Matrix left, Matrix right, ref Matrix result)
    {
      VerifyLeftAndRight(left, right);

      if (left.RowCount != right.ColumnCount)
      {
        throw new ArgumentException("left-side matrix' row count must be equal to the right-side matrix' column count");
      }

      if (result != null)
      {
        if (result.ColumnCount != right.ColumnCount || result.RowCount != left.RowCount)
        {
          throw new ArgumentException("result matrix must have the right-side matrix' column count and left-side matrix' row count");
        }
      }
      else
      {
        result = new Matrix(right.ColumnCount, left.RowCount);
      }

      for (var rowIndex = 0; rowIndex < result.RowCount; rowIndex++)
      {
        for (var columnIndex = 0; columnIndex < result.ColumnCount; columnIndex++)
        {
          result[columnIndex, rowIndex] = Enumerable
            .Zip(left.GetRow(rowIndex), right.GetColumn(columnIndex), Multiply)
            .Sum();
        }
      }

      return result;
    }

    #endregion

    #region Modulation

    public static Matrix Modulo(Matrix left, Double right)
    {
      var result = default(Matrix);

      return Subtract(left, right, ref result);
    }

    public static Matrix Modulo(Matrix matrix, Double right, ref Matrix result)
    {
      VerifyMatrix(matrix);

      return result = new Matrix(matrix.ColumnCount, matrix.RowCount, matrix.Select(value => value % right).ToArray());
    }

    public static Matrix Modulo(Matrix left, Matrix right)
    {
      var result = default(Matrix);

      return Modulo(left, right, ref result);
    }

    public static Matrix Modulo(Matrix left, Matrix right, ref Matrix result)
    {
      VerifyLeftAndRightAndResult(left, right, ref result, true);

      for (var index = 0; index < result.Storage.Length; index++)
      {
        result[index] = left[index] % right[index];
      }

      return result;
    }

    #endregion

    #region Subtraction

    public static Matrix Subtract(Matrix left, Double right)
    {
      var result = default(Matrix);

      return Subtract(left, right, ref result);
    }

    public static Matrix Subtract(Matrix matrix, Double right, ref Matrix result)
    {
      VerifyMatrix(matrix);

      return result = new Matrix(matrix.ColumnCount, matrix.RowCount, matrix.Select(value => value - right).ToArray());
    }

    public static Matrix Subtract(Matrix left, Matrix right)
    {
      var result = default(Matrix);

      return Subtract(left, right, ref result);
    }

    public static Matrix Subtract(Matrix left, Matrix right, ref Matrix result)
    {
      VerifyLeftAndRightAndResult(left, right, ref result, true);

      for (var index = 0; index < result.Storage.Length; index++)
      {
        result[index] = left[index] - right[index];
      }

      return result;
    }

    #endregion

    #endregion
    
    private static void VerifyMatrix(IMatrix matrix)
    {
      if (matrix == null)
      {
        throw new ArgumentNullException("matrix");
      }
    }

    private static void VerifyLeftAndRight(Matrix left, Matrix right, Boolean sameSize = false)
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

    private static void VerifyLeftAndRightAndResult(Matrix left, Matrix right, ref Matrix result, Boolean sameSize = false)
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

        if (result != null)
        {
          if (left.ColumnCount != result.ColumnCount)
          {
            throw new ArgumentException("left.ColumnCount != right.ColumnCount");
          }

          if (left.RowCount != result.RowCount)
          {
            throw new ArgumentException("left.RowCount != right.RowCount");
          }
        }
        else
        {
          result = new Matrix(left.ColumnCount, left.RowCount);
        }
      }
    }
  }
}
