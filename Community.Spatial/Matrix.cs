using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Globalization;
  using Linq;

  /// <summary>
  /// Represents a column-major matrix.
  /// </summary>
  public partial class Matrix : DenseMatrix //, IMatrix//<Matrix>
  {
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

    /// <summary>
    /// Initializes a new <see cref="T:Matrix"/>, specifying the column- and row count.
    /// </summary>
    public Matrix(Int32 rows, Int32 columns, params Double[] values) : base(rows, columns, values.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new <see cref="T:Matrix"/>, using a 2D-array.
    /// </summary>
    public Matrix(Double[,] values) : this(values.GetLength(1), values.GetLength(0), values.Cast<Double>().ToArray())
    {
    }

    /// <summary>
    /// Initializes a new <see cref="T:Matrix"/>, using a 2D-array.
    /// </summary>
    public Matrix(Matrix<Double> matrix) : base(matrix.RowCount, matrix.ColumnCount, matrix.ToRowWiseArray())
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

    //public IEnumerator<Double> GetEnumerator()
    //{
    //  return Storage.Enumerate().GetEnumerator();
    //}

    //IEnumerator IEnumerable.GetEnumerator()
    //{
    //  return GetEnumerator();
    //}

    #endregion

    #region Equatability

    //public Boolean Equals(Matrix other)
    //{
    //  return Equals(other, 0D);
    //}

    //public Boolean Equals(Matrix other, Double tolerance)
    //{
    //  if (ColumnCount == other.ColumnCount || RowCount == other.RowCount)
    //  {
    //    return Values.Zip(other.Values, (left, right) => Math.Abs(left - right)).All(value => value <= tolerance);
    //  }
      
    //  return false;
    //}

    //public override Int32 GetHashCode()
    //{
    //  base.GetHashCode()
    //  return Values.Select(value => value.GetHashCode()).Aggregate((aggregation, current) => aggregation ^ current);
    //}

    #endregion

    #region Formatability
    
    public String ToString(String format)
    {
      return ToString(format, CultureInfo.CurrentUICulture);
    }

    public new String ToString(String format, IFormatProvider formatProvider)
    {
      return String.Join(", ", Values.Select((value, index) => String.Format("M{0}{1}: {2}"
        , (index / ColumnCount) + 1
        , (index % ColumnCount) + 1
        , value.ToString(format, formatProvider))));
    }

    #endregion
  }
}