using System;
using System.Collections.Generic;
using System.Linq;

namespace Community.Mathematics
{
  public static class VectorExtensions
  {
    public static TScalar GetLengthSquare<TVector, TScalar>(this TVector vector)
      where TVector : IVector<TScalar> 
      where TScalar : IComparable<TScalar>, IConvertible, IFormattable
    {
      return (TScalar)Convert.ChangeType(vector.Cast<Double>().Sum(value => value * value), typeof(TScalar));
    }
  }
}
