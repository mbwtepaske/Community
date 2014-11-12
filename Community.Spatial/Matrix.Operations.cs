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

    #region Operations

    public static Matrix Copy(Matrix matrix)
    {
      var result = default(Matrix);

      return Copy(matrix, ref result);
    }

    public static Matrix Copy(Matrix matrix, ref Matrix result)
    {
      VerifyResult(matrix, ref result);

      Array.Copy(matrix.Storage, result.Storage, matrix.Storage.Length);

      return result;
    }

    public static Boolean Decompose(Matrix matrix, ref Matrix lower, ref Matrix upper, out Int32[] indices)
    {
      var result = true;

      VerifyMatrix(matrix);

      if (!matrix.IsSquare)
      {
        throw new ArgumentException("matrix is not a square matrix");
      }

      Identity(matrix.ColumnCount, matrix.RowCount, ref lower);
      Copy(matrix, ref upper);

      indices = new Int32[matrix.RowCount];

      var k0 = 0;

      for (var k = 0; k < matrix.ColumnCount - 1; k++)
      {
        var p = 0D;

        // find the row with the biggest pivot
        for (var i = k; i < matrix.RowCount; i++)      
        {
          if (Math.Abs(upper[i, k]) > p)
          {
            p = Math.Abs(upper[i, k]);
            k0 = i;
          }
        }

        if (Math.Abs(p) <= Double.Epsilon)
        {
          throw new InvalidOperationException("matrix is singular");
        }

        var pom1 = indices[k];

        indices[k] = indices[k0];
        indices[k0] = pom1;    // switch two rows in permutation matrix

        var pom2 = 0D;

        for (var i = 0; i < k; i++)
        {
          pom2 = lower[k, i];
          lower[k, i] = lower[k0, i];
          lower[k0, i] = pom2;
        }

        if (k != k0)
        {
          result = !result;
        }

        // Switch rows in U
        for (var i = 0; i < matrix.ColumnCount; i++)                  
        {
          pom2 = upper[k, i];
          upper[k, i] = upper[k0, i];
          upper[k0, i] = pom2;
        }

        for (var i = k + 1; i < matrix.RowCount; i++)
        {
          lower[i, k] = upper[i, k] / upper[k, k];

          for (var j = k; j < matrix.ColumnCount; j++)
          {
            upper[i, j] = upper[i, j] - lower[i, k] * upper[k, j];
          }
        }
      }

      return result;
    }

    public static Double Determinant(Matrix matrix)
    {
      var lower = default(Matrix);
      var upper = default(Matrix);
      var indices = default(Int32[]);

      var result = Decompose(matrix, ref lower, ref upper, out indices) 
        ? +1D 
        : -1D;

      for (var index = 0; index < matrix.RowCount; index++)
      {
        result *= upper[index, index];
      }

      return result;
    }

    public static Matrix Identity(Int32 size)
    {
      var result = default(Matrix);

      return Identity(size, size, ref result);
    }

    public static Matrix Identity(Int32 size, ref Matrix result)
    {
      return Identity(size, size, ref result);
    }

    public static Matrix Identity(Int32 columnCount, Int32 rowCount)
    {
      var result = default(Matrix);

      return Identity(columnCount, rowCount, ref result);
    }

    public static Matrix Identity(Int32 columnCount, Int32 rowCount, ref Matrix result)
    {
      Verify(columnCount, rowCount);

      if (result != null)
      {
        if (result.ColumnCount != columnCount || result.RowCount != rowCount)
        {
          throw new ArgumentException("result");
        }
      }
      else
      {
        result = new Matrix(columnCount, rowCount);
      }

      for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
      {
        for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
          result[columnIndex, rowIndex] = columnIndex == rowCount
            ? 1D
            : 0D;
        }
      }
    
      return result;
    }

    //public static Matrix Invert()

    public static Matrix Transpose(Matrix matrix)
    {
      var result = default(Matrix);

      return Transpose(matrix, ref result);
    }

    public static Matrix Transpose(Matrix matrix, ref Matrix result)
    {
      VerifyMatrix(matrix);

      if (result != null)
      {
        if (matrix.ColumnCount != result.RowCount && matrix.RowCount != result.ColumnCount)
        {
          throw new ArgumentException("result");
        }
      }
      else
      {
        result = new Matrix(matrix.ColumnCount, matrix.RowCount);
      }

      for (var columnIndex = 0; columnIndex < matrix.ColumnCount; columnIndex++)
      {
        for (var rowIndex = 0; rowIndex < matrix.RowCount; rowIndex++)
        {
          result[rowIndex, columnIndex] = matrix[columnIndex, rowIndex];
        }
      }

      return result;
    }

    #endregion

    #region Validation

    private static void Verify(Int32 columnCount, Int32 rowCount)
    {
      if (columnCount < 1)
      {
        throw new ArgumentException("columnCount");
      }

      if (rowCount < 1)
      {
        throw new ArgumentException("rowCount");
      }
    }

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

    private static void VerifyResult(Matrix matrix, ref Matrix result)
    {
      VerifyMatrix(matrix);

      if (result != null)
      {
        if (matrix.ColumnCount != result.ColumnCount)
        {
          throw new ArgumentException("matrix.ColumnCount must be equal to result.ColumnCount");
        }

        if (matrix.RowCount != result.RowCount)
        {
          throw new ArgumentException("matrix.RowCount must be equal to result.RowCount");
        }
      }
      else
      {
        result = new Matrix(matrix.ColumnCount, matrix.RowCount);
      }
    }

    #endregion
  }
}
