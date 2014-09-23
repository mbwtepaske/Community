using System.Collections;

namespace System.Spatial
{
  using Collections.Generic;

  public interface IVector : IEnumerable<Double>, IFormattable
  {
    /// <summary>
    /// Gets the amount of components of the vector.
    /// </summary>
    Int32 Count
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
  }

  public interface IVector<TVector> : IVector, IEquatable<TVector> where TVector : struct, IVector
  {
    Boolean Equals(TVector other, Double tolerance);
  }
}
