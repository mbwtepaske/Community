using System.Collections.ObjectModel;

namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Linq;

  public class MatrixStorage : ICloneable, IEnumerable<Double>
  {
    public readonly IList<Double> Data;

    /// <summary>
    /// The number of columns.
    /// </summary>
    public readonly Int32 ColumnCount;

    /// <summary>
    /// The number of rows.
    /// </summary>
    public readonly Int32 RowCount;

    public Double this[Int32 index]
    {
      get
      {
        return Data[index];
      }
      set
      {
        Data[index] = value;
      }
    }

    public Double this[Int32 columnIndex, Int32 rowIndex]
    {
      get
      {
        return Data[ColumnCount * rowIndex + columnIndex];
      }
      set
      {
        Data[ColumnCount * rowIndex + columnIndex] = value;
      }
    }

    public MatrixStorage(Int32 columnCount, Int32 rowCount, params Double[] data)
      : this(columnCount, rowCount, data as IList<Double>)
    {
    }

    public MatrixStorage(Int32 columnCount, Int32 rowCount, IEnumerable<Double> data, Boolean isReadOnly = false)
      : this(columnCount, rowCount, data.ToArray(), isReadOnly)
    {
    }

    public MatrixStorage(Int32 columnCount, Int32 rowCount, IList<Double> data, Boolean isReadOnly = false)
    {
      if (columnCount < 1)
      {
        throw new ArgumentOutOfRangeLessException("columnCount", 1);
      }
      
      if (rowCount < 1)
      {
        throw new ArgumentOutOfRangeLessException("rowCount", 1);
      }

      if (data.Count != columnCount * rowCount)
      {
        throw new ArgumentOutOfRangeLessException("data.Count", columnCount * rowCount);
      }
      
      ColumnCount = columnCount;
      RowCount = rowCount;

      Data = isReadOnly 
        ? new ReadOnlyCollection<Double>(data) 
        : data;
    }

    #region Cloning
    
    Object ICloneable.Clone()
    {
      return Clone();
    }

    public MatrixStorage Clone()
    {
      return new MatrixStorage(ColumnCount, RowCount, Data.IsReadOnly ? Data : Data.ToArray());
    }

    #endregion

    #region Enumeration

    public IEnumerator<Double> GetEnumerator()
    {
      return Data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Data.GetEnumerator();
    }

    #endregion
  }
}