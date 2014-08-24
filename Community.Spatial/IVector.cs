using System;
using System.Collections;
using System.Collections.Generic;

namespace Community.Mathematics
{
  public interface IVector<TScalar> : IEnumerable<TScalar>, IFormattable
    where TScalar : IComparable<TScalar>, IConvertible, IFormattable
  {
    TScalar this[Int32 index]
    {
      get;
      set;
    }
  }
}
