using System.Collections.ObjectModel;
using System.Text;

namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Diagnostics;
  using Linq;

  public delegate Double MatrixValueFactory(Int32 columnIndex, Int32 rowIndex);

  [DebuggerDisplay("{ToString(\"F5\", null)}")]
  public class Matrix : ICloneable, IEnumerable<Double>, IEquatable<Matrix>, IFormattable
  {
    public readonly MatrixStorage Storage;

    public Int32 ColumnCount
    {
      get
      {
        return Storage.ColumnCount;
      }
    }

    public Int32 RowCount
    {
      get
      {
        return Storage.RowCount;
      }
    }

    public Boolean IsSquare
    {
      get
      {
        return Storage.ColumnCount == Storage.RowCount;
      }
    }

    public Double this[Int32 columnIndex, Int32 rowIndex]
    {
      get
      {
        return Storage[columnIndex, rowIndex];
      }
      set
      {
        Storage[columnIndex, rowIndex] = value;
      }
    }

    #region Initialization

    public Matrix(Int32 order, Double defaultValue = 0D)
      : this(new MatrixStorage(order, order, Enumerable.Repeat(defaultValue, order * order).ToArray()))
    {
    }

    public Matrix(Int32 order, Func<Int32, Double> valueFactory, Boolean isReadOnly = false)
      : this(new MatrixStorage(order, order, Enumerable.Range(0, order * order).Select(valueFactory).ToArray(), isReadOnly))
    {
    }

    public Matrix(Int32 order, MatrixValueFactory valueFactory, Boolean isReadOnly = false)
      : this(CreateMatrixStorage(order, order, valueFactory, isReadOnly))
    {
    }

    public Matrix(Int32 columnCount, Int32 rowCount, Double defaultValue = 0D)
      : this(new MatrixStorage(columnCount, rowCount, Enumerable.Repeat(defaultValue, columnCount * rowCount).ToArray()))
    {
    }

    public Matrix(Int32 columnCount, Int32 rowCount, Func<Int32, Double> valueFactory, Boolean isReadOnly = false)
      : this(new MatrixStorage(columnCount, rowCount, Enumerable.Range(0, columnCount * rowCount).Select(valueFactory).ToArray(), isReadOnly))
    {
    }

    public Matrix(Int32 columnCount, Int32 rowCount, MatrixValueFactory valueFactory, Boolean isReadOnly = false)
      : this(CreateMatrixStorage(columnCount, rowCount, valueFactory, isReadOnly))
    {
    }

    public Matrix(Matrix matrix)
      : this(matrix.Storage.Clone())
    {
    }

    public Matrix(MatrixStorage storage)
    {
      Storage = storage;
    }

    private static MatrixStorage CreateMatrixStorage(Int32 columnCount, Int32 rowCount, MatrixValueFactory valueFactory, Boolean isReadOnly = false)
    {
      var data = new Double[columnCount * rowCount];

      for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
      {
        for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
          data[rowIndex * rowCount + columnIndex] = valueFactory(columnIndex, rowIndex);
        }
      }

      return new MatrixStorage(columnCount, rowCount, data, isReadOnly);
    }

    #endregion

    #region Cloning

    Object ICloneable.Clone()
    {
      return Clone();
    }

    public Matrix Clone()
    {
      return new Matrix(Storage.Clone());
    }

    #endregion

    #region Enumerable

    public IEnumerator<Double> GetEnumerator()
    {
      return Storage.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public IEnumerable<Vector> EnumerateColumns()
    {
      for (var columnIndex = 0; columnIndex < ColumnCount; columnIndex++)
      {
        yield return GetColumn(columnIndex);
      }
    }

    public IEnumerable<Vector> EnumerateRows()
    {
      for (var rowIndex = 0; rowIndex < RowCount; rowIndex++)
      {
        yield return GetRow(rowIndex);
      }
    }

    #endregion

    #region Equation

    public override Boolean Equals(Object other)
    {
      return Equals(other as Matrix);
    }

    public Boolean Equals(Matrix other)
    {
      return Equals(other, EqualityComparer<Double>.Default);
    }

    public Boolean Equals(Matrix other, IEqualityComparer<Double> comparer)
    {
      if (comparer == null)
      {
        throw new ArgumentNullException("comparer");
      }

      if (other == null || ColumnCount != other.ColumnCount || RowCount != other.RowCount)
      {
        return false;
      }

      if (!ReferenceEquals(this, other))
      {
        for (int index = 0, count = ColumnCount * RowCount; index < count; index++)
        {
          if (!comparer.Equals(Storage[index], other.Storage[index]))
          {
            return false;
          }
        }
      }

      return true;
    }

    public override Int32 GetHashCode()
    {
      return Storage.Data.Select(value => value.GetHashCode()).Aggregate((left, right) => left ^ right);
    }

    #endregion

    #region Formatting

    public override String ToString()
    {
      return ToString(null, null);
    }

    public String ToString(String format)
    {
      return ToString(format, null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return String.Join(" ", EnumerateRows().Select(row => row.ToString(format, formatProvider)));
    }

    #endregion

    #region Operations

    public virtual Matrix Addition(Matrix right)
    {
      Verify(right);

      return new Matrix(ColumnCount, RowCount, index => Storage[index] + right.Storage[index]);
    }

    public virtual Matrix Addition(Double right)
    {
      return new Matrix(ColumnCount, RowCount, index => Storage[index] + right);
    }

    /// <summary>
    /// Returns the column as a vector.
    /// </summary>
    public Vector GetColumn(Int32 columnIndex)
    {
      if (columnIndex < 0 || columnIndex >= ColumnCount)
      {
        throw new ArgumentOutOfRangeException("columnIndex");
      }

      return new Vector(RowCount, rowIndex => Storage[columnIndex, rowIndex]);
    }

    /// <summary>
    /// Returns the row as a vector.
    /// </summary>
    public Vector GetRow(Int32 rowIndex)
    {
      if (rowIndex < 0 || rowIndex >= RowCount)
      {
        throw new ArgumentOutOfRangeException("rowIndex");
      }

      return new Vector(ColumnCount, columnIndex => Storage[columnIndex, rowIndex]);
    }

    public virtual Matrix Inverse()
    {
      if (ColumnCount != RowCount)
      {
        throw new ArgumentException(Exceptions.ArgumentMatrixSquare);
      }

      throw new NotImplementedException();
    }

    public virtual Matrix Multiply(Matrix right)
    {
      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (ColumnCount != right.RowCount)
      {
        throw new ArgumentDimensionMismatchException("right.RowCount", ColumnCount);
      }

      if (RowCount != right.ColumnCount)
      {
        throw new ArgumentDimensionMismatchException("right.ColumnCount", RowCount);
      }

      var result = new MatrixStorage(right.ColumnCount, RowCount);

      for (var columnIndex = 0; columnIndex < right.ColumnCount; columnIndex++)
      {
        for (var rowIndex = 0; rowIndex < RowCount; rowIndex++)
        {
          result[columnIndex, rowIndex] = GetRow(rowIndex).DotProduct(right.GetColumn(columnIndex));
        }
      }

      return new Matrix(ColumnCount, RowCount, index => Storage[index] + right.Storage[index]);
    }

    public virtual Matrix Multiply(Double right)
    {
      return new Matrix(ColumnCount, RowCount, index => Storage[index] * right);
    }

    public virtual Matrix Subtract(Matrix right)
    {
      Verify(right);

      return new Matrix(ColumnCount, RowCount, index => Storage[index] - right.Storage[index]);
    }

    public virtual Matrix Subtract(Double right)
    {
      return new Matrix(ColumnCount, RowCount, index => Storage[index] - right);
    }

    public virtual Matrix Transpose()
    {
      return new Matrix(RowCount, ColumnCount, (columnIndex, rowIndex) => Storage[rowIndex, columnIndex]);
    }

    protected virtual void Verify(Matrix right)
    {
      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (ColumnCount != right.ColumnCount)
      {
        throw new ArgumentDimensionMismatchException("right.ColumnCount", ColumnCount);
      }

      if (RowCount != right.RowCount)
      {
        throw new ArgumentDimensionMismatchException("right.RowCount", RowCount);
      }
    }

    #endregion

    #region Operators

    public static Matrix operator +(Matrix matrix)
    {
      return matrix.Clone();
    }

    public static Matrix operator +(Matrix left, Matrix right)
    {
      return left.Addition(right);
    }

    public static Matrix operator -(Matrix matrix)
    {
      return matrix.Multiply(-1D);
    }

    public static Matrix operator -(Matrix left, Matrix right)
    {
      return left.Subtract(right);
    }

    public static Matrix operator *(Matrix left, Double right)
    {
      return left.Multiply(right);
    }

    public static Matrix operator *(Double left, Matrix right)
    {
      return right.Multiply(left);
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
      return left.Multiply(right);
    }

    #endregion
  }
}