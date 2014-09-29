namespace System.Spatial
{
  using Collections.Generic;

  public interface IMatrix : IEnumerable<Double>, IFormattable
  {
    /// <summary>
    /// Gets the amount of columns of this matrix.
    /// </summary>
    Int32 Columns
    {
      get;
    }

    /// <summary>
    /// Gets the amount of columns of this matrix.
    /// </summary>
    Int32 Rows
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
    Double this[Int32 column, Int32 rows]
    {
      get;
      set;
    }
  }

  public interface IMatrix<TMatrix> : IMatrix, IEquatable<TMatrix> where TMatrix : struct, IMatrix
  {
    Boolean Equals(TMatrix other, Double tolerance);
  }
}
