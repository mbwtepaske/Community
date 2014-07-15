using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Community.Mathematics
{
  /// <summary>
  /// Represents a double-precision vector.
  /// </summary>
  [Serializable]
  public class Vector : IEnumerable<Double>, IEquatable<Vector>, IFormattable
  {
    public Double this[Int32 index]
    {
      get
      {
        return Values[index];
      }
      set
      {
        Values[index] = value;
      }
    }

    public Int32 Size
    {
      get
      {
        return Values.Length;
      }
    }

    protected Double[] Values
    {
      get;
      private set;
    }

    public Vector(Int32 size)
    {
      Values = new Double[size];
    }

    public Vector(Double value, Int32 size)
    {
      Values = Enumerable.Repeat(value, size).ToArray();
    }

    public Vector(params Double[] values) : this(values.Length)
    {
      values.CopyTo(Values, 0);
    }
    
    public Double GetLength()
    {
      return GetLength(this);
    }

    public Double GetLengthSquare()
    {
      return GetLengthSquare(this);
    }

    public static Double GetLength(Vector vector)
    {
      return Math.Sqrt(GetLengthSquare(vector));
    }

    public static Double GetLengthSquare(Vector vector)
    {
      return vector.Sum(value => value * value);
    }

    #region IEnumeration

    public IEnumerator<Double> GetEnumerator()
    {
      return Values.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Values.GetEnumerator();
    }

    #endregion

    #region IEquatability

    public Boolean Equals(Vector other)
    {
      return !ReferenceEquals(other, null) && Values.SequenceEqual(other.Values);
    }

    public override Boolean Equals(Object other)
    {
      return !ReferenceEquals(other, null) && GetType() == other.GetType() && Equals(other as Vector);
    }

    public override Int32 GetHashCode()
    {
      return Values
        .Select(scalar => scalar.GetHashCode())
        .Aggregate((current, next) => current ^ next);
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

    #endregion

    #region IFormattable
    
    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return "[" + String.Join(", ", Values.Select(scalar => scalar.ToString(format, formatProvider))) + "]";
    }

    #endregion

    private static void ValidateParameters(Vector first, params Vector[] additional)
    {
      if (first == null)
      {
        throw new ArgumentNullException("first");
      }

      foreach (var vector in additional)
      {
        if (vector == null)
        {
          throw new ArgumentNullException();
        }

        if (first.Size != vector.Size)
        {
          throw new ArgumentException("vector sizes do not match");
        }
      }
    }

    #region Operators

    //public static Vector Add(Vector left, Vector right, Vector result)
    //{
    //  ValidateParameters(left, right);

    //  if (result != null)
    //  {
    //    ValidateParameters(left, result);
    //  }
    //  else
    //  {
    //    result = new Vector(left.Size);
    //  }

      
    //}

    public static Vector Interpolate(Vector left, Vector right, Double value)
    {
      return Interpolate(left, right, value, null);
    }

    public static Vector Interpolate(Vector left, Vector right, Double value, Vector result)
    {
      return Interpolate(left, right, new Vector(value, left.Size), result);
    }

    public static Vector Interpolate(Vector left, Vector right, Vector values)
    {
      return Interpolate(left, right, values, null);
    }

    public static Vector Interpolate(Vector left, Vector right, Vector values, Vector result)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (values == null)
      {
        throw new ArgumentNullException("values");
      }

      if (left.Size != right.Size)
      {
        throw new ArgumentException("left-size and right-size do not match");
      }

      if (left.Size != values.Size)
      {
        throw new ArgumentException("left-size and values-size do not match");
      }

      if (result != null && left.Size != result.Size)
      {
        throw new ArgumentException("left-size and result-size do not match");
      }

      if (result == null)
      {
        result = new Vector(left.Size);
      }

      for (var index = 0; index < left.Size; index++)
      {
        result[index] = (right[index] - left[index]) * values[index] + left[index];
      }

      return result;
    }

    private static IEnumerable<Double> Operation(Vector left, Double right, Func<Double, Double> operation)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      return Enumerable.Select(left, operation);
    }

    private static IEnumerable<Double> Operation(Vector left, Vector right, Func<Double, Double, Double> operation)
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (left.Size != right.Size)
      {
        throw new ArgumentException("left-size and right-size do not match");
      }

      return Enumerable.Zip(left, right, operation);
    }

    public static implicit operator Vector(Double[] values)
    {
      return new Vector(values);
    }

    public static implicit operator Double[](Vector vector)
    {
      return vector.Values;
    }

    public static Vector operator +(Vector left, Double right)
    {
      return Operation(left, right, v1 => v1 + right).ToArray();
    }

    public static Vector operator +(Vector left, Vector right)
    {
      return Operation(left, right, (v1, v2) => v1 + v2).ToArray();
    }

    public static Vector operator -(Vector left, Double right)
    {
      return Operation(left, right, v1 => v1 - right).ToArray();
    }

    public static Vector operator -(Vector left, Vector right)
    {
      return Operation(left, right, (v1, v2) => v1 - v2).ToArray();
    }

    public static Vector operator *(Vector left, Double right)
    {
      return Operation(left, right, v1 => v1 * right).ToArray();
    }

    public static Vector operator *(Vector left, Vector right)
    {
      return Operation(left, right, (v1, v2) => v1 * v2).ToArray();
    }

    public static Vector operator /(Vector left, Double right)
    {
      return Operation(left, right, v1 => v1 / right).ToArray();
    }

    public static Vector operator /(Vector left, Vector right)
    {
      return Operation(left, right, (v1, v2) => v1 / v2).ToArray();
    }

    public static Vector operator %(Vector left, Double right)
    {
      return Operation(left, right, v1 => v1 % right).ToArray();
    }

    public static Vector operator %(Vector left, Vector right)
    {
      return Operation(left, right, (v1, v2) => v1 % v2).ToArray();
    }

    #endregion
  }
}
