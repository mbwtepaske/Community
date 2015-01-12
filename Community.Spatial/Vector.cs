using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace System.Spatial
{
  /// <summary>
  /// Represents a double-precision vector.
  /// </summary>
  public partial class Vector : DenseVector
  {
    public Boolean IsNormal
    {
      get
      {
        return Utility.Equals(GetLength(), 1D);
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
    public Vector(Int32 size, Double value = 0D) : base(Enumerable.Repeat(value, size).ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instances of <see cref="T:Vector"/>, specifying an array of values.
    /// </summary>
    /// <param name="values">
    /// The values of the vector. The length of the array must be greater than zero.
    /// </param>
    public Vector(params Double[] values) : base(values)
    {
    }

    public Vector(Vector<Double> vector) : base(vector.ToArray())
    {
    }

    public Double GetLength()
    {
      return Math.Sqrt(GetLengthSquare());
    }

    public Double GetLengthSquare()
    {
      return Values.Sum(value => value * value);
    }

    public Vector Normalize()
    {
      var result = this;

      return Divide(this, GetLength(), ref result);
    }

    #region Cloning

    public new Vector Clone()
    {
      return new Vector(Values);
    }

    #endregion
    
    #region Enumeration

    //public IEnumerator<Double> GetEnumerator()
    //{
    //  return Values.Cast<Double>().GetEnumerator();
    //}

    //IEnumerator IEnumerable.GetEnumerator()
    //{
    //  return Values.GetEnumerator();
    //}

    #endregion

    #region Equation

    //public Boolean Equals(Vector other)
    //{
    //  return Equals(other, 0D);
    //}

    //public Boolean Equals(Vector other, Double tolerance)
    //{
    //  return !ReferenceEquals(other, null) && (ReferenceEquals(this, other) || Values.Zip(other.Values, (left, right) => Math.Abs(left - right)).All(value => value <= tolerance));
    //}

    //Boolean IEquatable<Vector>.Equals(Vector other)
    //{
    //  return Equals(other);
    //}

    //bool IVector<Vector>.Equals(Vector other, double tolerance)
    //{
    //  return Equals(other, tolerance);
    //}

    #endregion

    #region Formatting
    
    public String ToString(String format)
    {
      return ToString(format, null);
    }

    public new String ToString(String format, IFormatProvider formatProvider)
    {
      return String.Join(", ", Values.Select(scalar => scalar.ToString(format, formatProvider)));
    }

    #endregion
  }
}