using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Community.Mathematics
{
  /// <summary>
  /// Represents an array of scalar / value-types.
  /// </summary>
  public class Vector<TScalar>
    : IEquatable<Vector<TScalar>>
    , IEnumerable<TScalar>
    where TScalar
    : struct 
    , IComparable
    , IComparable<TScalar>
    , IEquatable<TScalar>
  {
    public TScalar this[Int32 index]
    {
      get
      {
        return Scalars[index];
      }
      set
      {
        Scalars[index] = value;
      }
    }

    protected TScalar[] Scalars
    {
      get;
      private set;
    }

    public Int32 Size
    {
      get
      {
        return Scalars.Length;
      }
    }

    public Vector(Int32 size)
    {
      Scalars = new TScalar[size];
    }

    public Vector(params TScalar[] values)
      : this(values.Length)
    {
      values.CopyTo(Scalars, 0);
    }

    #region Enumeration

    public IEnumerator<TScalar> GetEnumerator()
    {
      return Scalars.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Scalars.GetEnumerator();
    }

    #endregion

    #region Equatability

    public Boolean Equals(Vector<TScalar> other)
    {
      return !ReferenceEquals(other, null) && Scalars.SequenceEqual(other.Scalars);
    }

    public override Boolean Equals(Object other)
    {
      return !ReferenceEquals(other, null) && GetType() == other.GetType() && Equals(other as Vector<TScalar>);
    }

    public override Int32 GetHashCode()
    {
      return Scalars
        .Select(scalar => scalar.GetHashCode())
        .Aggregate((current, next) => current ^ next);
    }

    public static Boolean operator ==(Vector<TScalar> left, Vector<TScalar> right)
    {
      if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
      {
        return true;
      }

      return !ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Equals(right);
    }

    public static Boolean operator !=(Vector<TScalar> left, Vector<TScalar> right)
    {
      if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
      {
        return false;
      }

      if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
      {
        return true;
      }

      return !left.Equals(right);
    }

    #endregion

    public override String ToString()
    {
      return "[" + String.Join(", ", Scalars) + "]";
    }
  }
}
