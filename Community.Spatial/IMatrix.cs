namespace System.Spatial
{
  using Collections.Generic;

  public interface IMatrix : IEnumerable<Double>, IFormattable
  {
    /// <summary>
    /// Gets the amount of columns of this matrix.
    /// </summary>
    Int32 ColumnCount
    {
      get;
    }

    /// <summary>
    /// Gets the amount of columns of this matrix.
    /// </summary>
    Int32 RowCount
    {
      get;
    }

    /// <summary>
    /// Gets or sets the component at the specified index.
    /// </summary>
    Double this[Int32 index]
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the component at the specified column and row.
    /// </summary>
    Double this[Int32 columnIndex, Int32 rows]
    {
      get;
      set;
    }
  }

  public interface IMatrix<TMatrix> : IMatrix, IEquatable<TMatrix> where TMatrix : class, IMatrix
  {
    Boolean Equals(TMatrix other, Double tolerance);
  }
}
