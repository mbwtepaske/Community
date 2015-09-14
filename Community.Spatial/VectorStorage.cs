namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Linq;

  /// <summary>
  /// Represents the vector data storage.
  /// </summary>
  public class VectorStorage : ICloneable, IEnumerable<Double> //, IList<Double>
  {
    private static readonly Double[] Empty = new Double[0];

    public readonly IList<Double> Data;

    public Int32 Count
    {
      get
      {
        return Data.Count;
      }
    }

    public Boolean IsReadOnly
    {
      get
      {
        return Data.IsReadOnly;
      }
    }

    public virtual Double this[Int32 index]
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

    public VectorStorage(params Double[] values)
      : this(values as IList<Double>)
    {
    }

    public VectorStorage(IEnumerable<Double> values)
      : this(values != null ? values.ToArray() : Empty)
    {
    }

    public VectorStorage(IList<Double> data)
    {
      if (data == null)
      {
        throw new ArgumentNullException("data");
      }

      if (data.Count < 1)
      {
        throw new ArgumentOutOfRangeLessException("data.Count", 1);
      }

      Data = data;
    }

    public virtual Double At(Int32 index)
    {
      return Data[index];
    }

    public virtual void At(Int32 index, Double value)
    {
      Data[index] = value;
    }

    Object ICloneable.Clone()
    {
      return Clone();
    }

    public virtual VectorStorage Clone()
    {
      return new VectorStorage(Data.IsReadOnly ? Data : Data.ToArray());
    }

    public virtual IEnumerable<Double> Enumerate()
    {
      return Data;
    }

    public virtual IEnumerable<TResult> Enumerate<TResult>(Func<Double, TResult> selector)
    {
      if (selector == null)
      {
        throw new ArgumentNullException("selector");
      }

      foreach (var value in Data)
      {
        yield return selector(value);
      }
    }

    public virtual IEnumerable<TResult> Enumerate<TResult>(Func<Double, Int32, TResult> selector)
    {
      if (selector == null)
      {
        throw new ArgumentNullException("selector");
      }

      for (var index = 0; index < Data.Count; index++)
      {
        yield return selector(Data[index], index);
      }
    }

    public IEnumerator<Double> GetEnumerator()
    {
      return Enumerate().GetEnumerator();
    }

    //void ICollection<Double>.Add(Double item)
    //{
    //  throw new NotSupportedException();
    //}

    //void ICollection<Double>.Clear()
    //{
    //  throw new NotSupportedException();
    //}

    //Boolean ICollection<Double>.Contains(Double value)
    //{
    //  return Data.Contains(value);
    //}

    //void ICollection<Double>.CopyTo(Double[] destination, Int32 destinationIndex)
    //{
    //  Data.CopyTo(destination, destinationIndex);
    //}

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    //Int32 IList<Double>.IndexOf(Double value)
    //{
    //  return Data.IndexOf(value);
    //}

    //void IList<Double>.Insert(Int32 index, Double value)
    //{
    //  throw new NotSupportedException();
    //}

    //Boolean ICollection<Double>.Remove(Double item)
    //{
    //  throw new NotSupportedException();
    //}

    //void IList<Double>.RemoveAt(Int32 index)
    //{
    //  throw new NotSupportedException();
    //}
  }
}