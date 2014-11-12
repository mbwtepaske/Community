namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Globalization;
  using Linq;

  /// <summary>
  /// Represents a row-major matrix.
  /// </summary>
  public partial class Matrix : IMatrix<Matrix>
  {
    /// <summary>
    /// Gets the column count of this matrix.
    /// </summary>
    public Int32 ColumnCount
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets the row count of this matrix.
    /// </summary>
    public Int32 RowCount
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets whether this matrix is a square matrix.
    /// </summary>
    public Boolean IsSquare
    {
      get
      {
        return ColumnCount == RowCount;
      }
    }

    protected Double[] Storage
    {
      get;
      private set;
    }

    public Double this[Int32 index]
    {
      get
      {
        return Storage[index];
      }
      set
      {
        Storage[index] = value;
      }
    }
   
    public Double this[Int32 columnIndex, Int32 rowIndex]
    {
      get
      {
        return this[columnIndex + ColumnCount * rowIndex];
      }
      set
      {
        this[columnIndex + ColumnCount * rowIndex] = value;
      }
    }

    /// <summary>
    /// Initializes a new <see cref="T:Matrix"/>, specifying the column- and row count.
    /// </summary>
    public Matrix(Int32 columnCount, Int32 rowCount, params Double[] values)
    {
      Verify(columnCount, rowCount);

      ColumnCount = columnCount;
      RowCount = rowCount;
      Storage = new Double[columnCount * rowCount];

      Array.ConstrainedCopy(values, 0, Storage, 0, Math.Min(values.Length, Storage.Length));
    }

    /// <summary>
    /// Initializes a new <see cref="T:Matrix"/>, using a 2D-array.
    /// </summary>
    public Matrix(Double[,] values) : this(values.GetLength(1), values.GetLength(0), values.Cast<Double>().ToArray())
    {
    }

    /// <summary>
    /// Returns the values in the specific column of the matrix.
    /// </summary>
    public IEnumerable<Double> GetColumn(Int32 columnIndex)
    {
      for (var rowIndex = 0; rowIndex < RowCount; rowIndex++)
      {
        yield return this[columnIndex, rowIndex];
      }
    }

    /// <summary>
    /// Returns the values in the specific row of the matrix.
    /// </summary>
    public IEnumerable<Double> GetRow(Int32 rowIndex)
    {
      for (var columnIndex = 0; columnIndex < ColumnCount; columnIndex++)
      {
        yield return this[columnIndex, rowIndex];
      }
    }

    #region Enumeration

    public IEnumerator<Double> GetEnumerator()
    {
      foreach (var value in Storage)
      {
        yield return value;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion

    #region Equatability

    public Boolean Equals(Matrix other)
    {
      return Equals(other, 0D);
    }

    public Boolean Equals(Matrix other, Double tolerance)
    {
      if (ColumnCount == other.ColumnCount || RowCount == other.RowCount)
      {
        return Storage.Zip(other.Storage, (left, right) => Math.Abs(left - right)).All(value => value <= tolerance);
      }
      
      return false;
    }

    public override Int32 GetHashCode()
    {
      return Storage.Select(value => value.GetHashCode()).Aggregate((aggregation, current) => aggregation ^ current);
    }

    #endregion

    #region Formatability
    
    public override String ToString()
    {
      return ToString("F6", CultureInfo.CurrentUICulture);
    }

    public String ToString(String format)
    {
      return ToString(format, CultureInfo.CurrentUICulture);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return String.Join(", ", Storage.Select((value, index) => String.Format("M{0}{1}: {2}"
        , (index / ColumnCount) + 1
        , (index % ColumnCount) + 1
        , value.ToString(format, formatProvider))));
    }

    #endregion
  }
}