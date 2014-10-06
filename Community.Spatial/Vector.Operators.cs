using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public partial class Vector
  {
    public static implicit operator Vector(Single[] values)
    {
      return new Vector(values.Select(Convert.ToDouble).ToArray());
    }

    public static implicit operator Vector(Double[] values)
    {
      return new Vector(values);
    }

    public static implicit operator Single[](Vector vector)
    {
      return vector.Storage.Select(Convert.ToSingle).ToArray();
    }

    public static implicit operator Double[](Vector vector)
    {
      return vector.Storage.ToArray();
    }

    public static Vector operator +(Vector left, Double right)
    {
      return Operation(left, right, Add);
    }

    public static Vector operator +(Vector left, Vector right)
    {
      return Operation(left, right, Add);
    }

    public static Vector operator -(Vector left, Double right)
    {
      return Operation(left, right, Subtract);
    }

    public static Vector operator -(Vector left, Vector right)
    {
      return Operation(left, right, Subtract);
    }

    public static Vector operator *(Vector left, Double right)
    {
      return Operation(left, right, Multiply);
    }

    public static Vector operator *(Vector left, Vector right)
    {
      return Operation(left, right, Multiply);
    }

    public static Vector operator /(Vector left, Double right)
    {
      return Operation(left, right, Divide);
    }

    public static Vector operator /(Vector left, Vector right)
    {
      return Operation(left, right, Divide);
    }

    public static Vector operator %(Vector left, Double right)
    {
      return Operation(left, right, Modulo);
    }

    public static Vector operator %(Vector left, Vector right)
    {
      return Operation(left, right, Modulo);
    }

    public static Boolean operator ==(Vector left, Vector right)
    {
      if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
      {
        return true;
      }

      return !ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Equals(right);
    }

    public static Boolean operator !=(Vector left, Vector right)
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
  }
}
