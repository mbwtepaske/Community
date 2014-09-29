namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Globalization;
  using Linq;

  public partial class Matrix : IMatrix<Matrix>
  {
    public Int32 ColumnCount
    {
      get;
      private set;
    }

    public Int32 RowCount
    {
      get;
      private set;
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
   
    public Double this[Int32 column, Int32 row]
    {
      get
      {
        return this[column + ColumnCount * row];
      }
      set
      {
        this[column + ColumnCount * row] = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Matrix"/>.
    /// </summary>
    public Matrix(Int32 columnCount, Int32 rowCount, params Double[] values)
    {
      Verify(columnCount, rowCount);

      ColumnCount = columnCount;
      RowCount = rowCount;
      Storage = new Double[columnCount * rowCount];

      Array.ConstrainedCopy(values, 0, Storage, 0, Math.Min(values.Length, Storage.Length));
    }

    public Matrix(Double[,] values) : this(values.GetLength(0), values.GetLength(1), values.Cast<Double>().ToArray())
    {
    }

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

    public override Int32 GetHashCode()
    {
      return Storage.Select(value => value.GetHashCode()).Aggregate((aggregation, current) => aggregation ^ current);
    }

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
      return String.Join(", ", Storage.Select((value, index) => String.Format("M{0}{1}: {2}", index / RowCount + 1, index % ColumnCount + 1, value.ToString(format, formatProvider))));
    }

    protected void Verify(Int32 columnCount, Int32 rowCount)
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
  }
}