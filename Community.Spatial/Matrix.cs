namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Linq;

  public class Matrix : IEnumerable<Double>
  {
    protected Double[,] Data
    {
      get;
      private set;
    }

    public Int32 ColumnCount
    {
      get
      {
        return Data.GetLength(0);
      }
    }

    public Int32 RowCount
    {
      get
      {
        return Data.GetLength(1);
      }
    }

    public Double this[Int32 columnIndex, Int32 rowIndex]
    {
      get
      {
        return Data[columnIndex, rowIndex];
      }
      set
      {
        Data[columnIndex, rowIndex] = value;
      }
    }

    public Matrix(Int32 columnCount, Int32 rowCount)
    {
      if (columnCount < 0)
      {
        throw new ArgumentOutOfRangeException("columnCount");
      }

      if (rowCount < 0)
      {
        throw new ArgumentOutOfRangeException("rowCount");
      }

      Data = new Double[columnCount, rowCount];
    }

    public IEnumerator<Double> GetEnumerator()
    {
      for (var rowIndex = 0; rowIndex < RowCount; rowIndex++)
      {
        for (var columnIndex = 0; columnIndex < ColumnCount; columnIndex++)
        {
          yield return Data[columnIndex, rowIndex];
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #region Operations

    public static Matrix Multiply(Matrix left, Double right, Matrix result = null)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (result == null)
      {
        result = new Matrix(left.ColumnCount, left.RowCount);
      }
      else if (result.ColumnCount != left.ColumnCount || result.RowCount != left.RowCount)
      {
        throw new ArgumentException("result");
      }

      for (var rowIndex = 0; rowIndex < result.RowCount; rowIndex++)
      {
        for (var columnIndex = 0; columnIndex < result.ColumnCount; columnIndex++)
        {
          result[columnIndex, rowIndex] *= right;
        }
      }

      return result;
    }

    public static Matrix Multiply(Matrix left, Matrix right, Matrix result = null)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (left.ColumnCount != right.RowCount)
      {
        throw new InvalidOperationException("left column count is unequal to right row count");
      }
      
      if (left.RowCount != right.ColumnCount)
      {
        throw new InvalidOperationException("left row count is unequal to right column count");
      }

      if (result == null)
      {
        result = new Matrix(left.ColumnCount, right.RowCount);
      }
      else if (result.ColumnCount != left.ColumnCount || result.RowCount != right.RowCount)
      {
        throw new ArgumentException("result");
      }

      for (var rowIndex = 0; rowIndex < result.RowCount; rowIndex++)
      {
        for (var columnIndex = 0; columnIndex < result.ColumnCount; columnIndex++)
        {
          result[columnIndex, rowIndex] = Enumerable.Range(0, left.RowCount).Sum(index => left[columnIndex, index] * right[index, rowIndex]);
        }
      }

      return result;
    }

    public static Matrix operator *(Matrix left, Double right)
    {
      return Multiply(left, right, null);
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
      return Multiply(left, right, null);
    }

    #endregion
  }
}
