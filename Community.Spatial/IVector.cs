namespace System.Spatial
{
  using Collections.Generic;

  public interface IVector : ICloneable, IEnumerable<Double>, IFormattable
  {
    /// <summary>
    /// Gets the amount of components of the vector.
    /// </summary>
    Int32 Size
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

  public interface IVector<TVector> : IVector, IEquatable<TVector>
  {
    Boolean Equals(TVector other, Double tolerance);
  }
}
