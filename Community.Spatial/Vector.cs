using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace System.Spatial
{
  /// <summary>
  /// Represents a double-precision vector.
  /// </summary>
  public partial class Vector : IVector<Vector>
  {
    public Boolean IsNormal
    {
      get
      {
        return Utility.Equals(GetLength(), 1D);
      }
    }

    /// <summary>
    /// Gets the number of components this vector contains.
    /// </summary>
    public Int32 Size
    {
      get
      {
        return Storage.Length;
      }
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

    /// <summary>
    /// Initializes a new instances of <see cref="T:Vector"/>, specifying a size and optionally a default value for each component.
    /// </summary>
    /// <param name="size">
    /// The size of the vector. This parameter must be greater than zero.
    /// </param>
    /// <param name="value">
    /// The initial value for each element in the vector.
    /// </param>
    public Vector(Int32 size, Double value = 0D)
    {
      if (size < 1)
      {
        throw new ArgumentException(@"size must be greater than zero", "size");
      }

      Storage = Enumerable.Repeat(value, size).ToArray();
    }

    /// <summary>
    /// Initializes a new instances of <see cref="T:Vector"/>, specifying an array of values.
    /// </summary>
    /// <param name="values">
    /// The values of the vector. The length of the array must be greater than zero.
    /// </param>
    public Vector(params Double[] values) : this(values.Length)
    {
      values.CopyTo(Storage, 0);
    }
    
    public Double GetLength()
    {
      return Math.Sqrt(GetLengthSquare());
    }

    public Double GetLengthSquare()
    {
      return Storage.Sum(value => value * value);
    }

    public Vector Normalize()
    {
      var result = this;

      return Divide(this, GetLength(), ref result);
    }

    #region Cloning

    public Vector Clone()
    {
      return new Vector(Storage);
    }

    Object ICloneable.Clone()
    {
      return Clone();
    }

    #endregion
    
    #region Enumeration

    public IEnumerator<Double> GetEnumerator()
    {
      return Storage.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Storage.GetEnumerator();
    }

    #endregion

    #region Equation

    public Boolean Equals(Vector other)
    {
      return Equals(other, 0D);
    }

    public Boolean Equals(Vector other, Double tolerance)
    {
      return !ReferenceEquals(other, null) && (ReferenceEquals(this, other) || Storage.Zip(other.Storage, (left, right) => Math.Abs(left - right)).All(value => value <= tolerance));
    }

    public override Boolean Equals(Object other)
    {
      return !ReferenceEquals(other, null) && GetType() == other.GetType() && Equals(other as Vector);
    }

    public override Int32 GetHashCode()
    {
      return Storage
        .Select(scalar => scalar.GetHashCode())
        .Aggregate((result, current) => result ^ current);
    }

    #endregion

    #region Formatting
    
    public override String ToString()
    {
      return ToString("G", null);
    }

    public String ToString(String format)
    {
      return ToString(format, null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return String.Join(", ", Storage.Select(scalar => scalar.ToString(format, formatProvider)));
    }

    #endregion
  }
}
