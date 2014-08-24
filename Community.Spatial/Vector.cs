using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Spatial
{
  /// <summary>
  /// Represents a double-precision vector.
  /// </summary>
  [Serializable]
  public class Vector : IEnumerable<Double>, IEquatable<Vector>, IFormattable
  {
    protected Double[] Data
    {
      get;
      private set;
    }

    public Double this[Int32 index]
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

    public Int32 Size
    {
      get
      {
        return Data.Length;
      }
    }

    public Vector(Int32 size)
    {
      Data = new Double[size];
    }

    public Vector(Double value, Int32 size)
    {
      Data = Enumerable.Repeat(value, size).ToArray();
    }

    public Vector(params Double[] values) : this(values.Length)
    {
      values.CopyTo(Data, 0);
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
      return Data.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Data.GetEnumerator();
    }

    #endregion

    #region IEquatability

    public Boolean Equals(Vector other)
    {
      return !ReferenceEquals(other, null) && Data.SequenceEqual(other.Data);
    }

    public override Boolean Equals(Object other)
    {
      return !ReferenceEquals(other, null) && GetType() == other.GetType() && Equals(other as Vector);
    }

    public override Int32 GetHashCode()
    {
      return Data
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
      return "[" + String.Join(", ", Data.Select(scalar => scalar.ToString(format, formatProvider))) + "]";
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

    private static Double Add(Double left, Double right)
    {
      return left + right;
    }

    public static Vector Add(Vector left, Vector right, Vector result = null)
    {
      return Operation(left, right, result, Add);
    }

    private static Double Divide(Double left, Double right)
    {
      return left / right;
    }

    public static Vector Divide(Vector left, Vector right, Vector result = null)
    {
      return Operation(left, right, result, Divide);
    }

    public static Vector Interpolate(Vector left, Vector right, Double value, Vector result = null)
    {
      return Interpolate(left, right, new Vector(value, left.Size), result);
    }
    
    public static Vector Interpolate(Vector left, Vector right, Vector values, Vector result = null)
    {
      if (result != null)
      {
        ValidateParameters(left, right, values, result);
      }
      else
      {
        ValidateParameters(left, right, values);

        result = new Vector(left.Size);
      }

      for (var index = 0; index < result.Size; index++)
      {
        result[index] = left[index] + (right[index] - left[index]) * values[index];
      }

      return result;
    }

    private static Double Modulo(Double left, Double right)
    {
      return left % right;
    }
    
    public static Vector Modulo(Vector left, Vector right, Vector result = null)
    {
      return Operation(left, right, result, Modulo);
    }

    private static Double Multiply(Double left, Double right)
    {
      return left * right;
    }

    public static Vector Multiply(Vector left, Vector right, Vector result = null)
    {
      return Operation(left, right, result, Multiply);
    }

    private static Vector Operation(Vector left, Double right, Vector result, Func<Double, Double, Double> operation)
    {
      if (result != null)
      {
        ValidateParameters(left, result);
      }
      else
      {
        ValidateParameters(left);

        result = new Vector(left.Size);
      }

      for (var index = 0; index < result.Size; index++)
      {
        result[index] = operation(left[index], right);
      }

      return result;
    }

    private static Vector Operation(Vector left, Vector right, Vector result, Func<Double, Double, Double> operation)
    {
      if (result != null)
      {
        ValidateParameters(left, right, result);
      }
      else
      {
        ValidateParameters(left, right);

        result = new Vector(left.Size);
      }

      for (var index = 0; index < result.Size; index++)
      {
        result[index] = operation(left[index], right[index]);
      }

      return result;
    }

    private static Double Subtract(Double left, Double right)
    {
      return left - right;
    }

    public static Vector Subtract(Vector left, Vector right, Vector result = null)
    {
      return Operation(left, right, result, Subtract);
    }

    public static implicit operator Vector(Single[] values)
    {
      return new Vector(values.Cast<Single>().Select(Convert.ToDouble).ToArray());
    }

    public static implicit operator Vector(Double[] values)
    {
      return new Vector(values);
    }

    public static implicit operator Single[](Vector vector)
    {
      return vector.Data.Select(Convert.ToSingle).ToArray();
    }

    public static implicit operator Double[](Vector vector)
    {
      return vector.Data;
    }

    public static Vector operator +(Vector left, Double right)
    {
      return Operation(left, right, null, Add);
    }

    public static Vector operator +(Vector left, Vector right)
    {
      return Operation(left, right, null, Add);
    }

    public static Vector operator -(Vector left, Double right)
    {
      return Operation(left, right, null, Subtract);
    }

    public static Vector operator -(Vector left, Vector right)
    {
      return Operation(left, right, null, Subtract);
    }

    public static Vector operator *(Vector left, Double right)
    {
      return Operation(left, right, null, Multiply);
    }

    public static Vector operator *(Vector left, Vector right)
    {
      return Operation(left, right, null, Multiply);
    }

    public static Vector operator /(Vector left, Double right)
    {
      return Operation(left, right, null, Divide);
    }

    public static Vector operator /(Vector left, Vector right)
    {
      return Operation(left, right, null, Divide);
    }

    public static Vector operator %(Vector left, Double right)
    {
      return Operation(left, right, null, Modulo);
    }

    public static Vector operator %(Vector left, Vector right)
    {
      return Operation(left, right, null, Modulo);
    }

    #endregion
  }
}
