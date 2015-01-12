using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public partial class Vector
  {
    #region Arithmetic
    
    #region Addition

    private static Double Add(Double left, Double right)
    {
      return left + right;
    }

    public static Vector Add(Vector left, Double right)
    {
      return Operation(left, right, Add);
    }

    public static Vector Add(Vector left, Double right, ref Vector result)
    {
      return Operation(left, right, ref result, Add);
    }

    public static Vector Add(Vector left, Vector right)
    {
      return Operation(left, right, Add);
    }

    public static Vector Add(Vector left, Vector right, ref Vector result)
    {
      return Operation(left, right, ref result, Add);
    }

    #endregion

    #region Division
    
    private static Double Divide(Double left, Double right)
    {
      return left / right;
    }

    public static Vector Divide(Vector left, Double right)
    {
      return Operation(left, right, Divide);
    }

    public static Vector Divide(Vector left, Double right, ref Vector result)
    {
      return Operation(left, right, ref result, Divide);
    }

    public static Vector Divide(Vector left, Vector right)
    {
      return Operation(left, right, Divide);
    }

    public static Vector Divide(Vector left, Vector right, ref Vector result)
    {
      return Operation(left, right, ref result, Divide);
    }

    #endregion

    #region Multiplication

    private static Double Multiply(Double left, Double right)
    {
      return left * right;
    }

    public static Vector Multiply(Vector left, Double right)
    {
      return Operation(left, right, Multiply);
    }

    public static Vector Multiply(Vector left, Double right, ref Vector result)
    {
      return Operation(left, right, ref result, Multiply);
    }

    public static Vector Multiply(Vector left, Vector right)
    {
      return Operation(left, right, Multiply);
    }

    public static Vector Multiply(Vector left, Vector right, ref Vector result)
    {
      return Operation(left, right, ref result, Multiply);
    }

    #endregion

    #region Modulation

    private static Double Modulo(Double left, Double right)
    {
      return left % right;
    }

    public static Vector Modulo(Vector left, Double right)
    {
      return Operation(left, right, Modulo);
    }

    public static Vector Modulo(Vector left, Double right, ref Vector result)
    {
      return Operation(left, right, ref result, Modulo);
    }

    public static Vector Modulo(Vector left, Vector right)
    {
      return Operation(left, right, Modulo);
    }

    public static Vector Modulo(Vector left, Vector right, ref Vector result)
    {
      return Operation(left, right, ref result, Modulo);
    }

    #endregion

    #region Subtraction
    
    private static Double Subtract(Double left, Double right)
    {
      return left - right;
    }

    public static Vector Subtract(Vector left, Double right)
    {
      return Operation(left, right, Subtract);
    }

    public static Vector Subtract(Vector left, Double right, ref Vector result)
    {
      return Operation(left, right, ref result, Subtract);
    }

    public static Vector Subtract(Vector left, Vector right)
    {
      return Operation(left, right, Subtract);
    }

    public static Vector Subtract(Vector left, Vector right, ref Vector result)
    {
      return Operation(left, right, ref result, Subtract);
    }

    #endregion

    #region Transformation
    
    public static Vector Transform(Vector left, Matrix right)
    {
      var result = default(Vector);

      return Transform(left, right, ref result);
    }

    public static Vector Transform(Vector left, Matrix right, ref Vector result)
    {
      Verify(left, ref result);

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (left.Count != right.ColumnCount)
      {
        throw new ArgumentException("column count of the matrix must match the size of the vector");
      }

      if (left.Count != right.RowCount)
      {
        throw new ArgumentException("row count of the matrix must match the size of the vector");
      }

      for (var index = 0; index < left.Count; index++)
      {
        result[index] = left.Zip(right.GetRow(index), Multiply).Sum();
      }

      return result;
    }

    #endregion

    protected static Vector Operation(Vector vector, Double value, Func<Double, Double, Double> operation)
    {
      var result = default(Vector);

      return Operation(vector, value, ref result, operation);
    }

    protected static Vector Operation(Vector vector, Double value, ref Vector result, Func<Double, Double, Double> operation)
    {
      Verify(vector, ref result);

      for (var index = 0; index < result.Count; index++)
      {
        result[index] = operation(vector[index], value);
      }

      return result;
    }

    protected static Vector Operation(Vector left, Vector right, Func<Double, Double, Double> operation)
    {
      var result = default(Vector);

      return Operation(left, right, ref result, operation);
    }

    protected static Vector Operation(Vector left, Vector right, ref Vector result, Func<Double, Double, Double> operation)
    {
      Verify(left, right, ref result);
      
      for (var index = 0; index < result.Count; index++)
      {
        result[index] = operation(left[index], right[index]);
      }

      return result;
    }

    #endregion

    #region Various

    public static Double Dot(Vector left, Vector right)
    {
      Verify(left, right);

      return left.Zip(right, Multiply).Sum();
    }

    public static Vector Normalize(Vector vector)
    {
      var result = default(Vector);

      return Normalize(vector, ref result);
    }

    public static Vector Normalize(Vector vector, ref Vector result)
    {
      Verify(vector, ref result);

      var length = vector.GetLength();

      if (Utility.Equals(length, 0D))
      {
        throw new ArgumentException("vector length must be greater than zero");
      }

      return Divide(vector, length, ref result);
    }

    #endregion

    #region Verification

    protected static void Verify<TVector>(TVector vector) where TVector : Vector
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }
    }

    protected static void Verify<TVector>(TVector vector, ref TVector result) where TVector : Vector, new()
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }

      if (result != null)
      {
        if (result.Count != vector.Count)
        {
          throw new ArgumentException("the size of result vector must be " + vector.Count);
        }
      }
      else
      {
        vector = new TVector();
      }
    }

    protected static void Verify(Vector vector, ref Vector result)
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }

      VerifyOrCreateResult(vector.Count, ref result);
    }
    
    protected static void Verify<TVector>(TVector left, TVector right) where TVector : Vector
    {
      if (left == null)
      {
        throw new ArgumentNullException("left");
      }

      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (left.Count != right.Count)
      {
        throw new ArgumentException("vector left and right are not equal in size");
      }
    }

    protected static void Verify(Vector left, Vector right, ref Vector result)
    {
      Verify(left, right);
      VerifyOrCreateResult(left.Count, ref result);
    }

    protected static void Verify<TVector>(TVector left, TVector right, ref TVector result) where TVector : Vector, new()
    {
      Verify(left, right);
      VerifyOrCreateResult(left.Count, ref result);
    }

    protected static void VerifyOrCreateResult(Int32 size, ref Vector result)
    {
      if (result != null)
      {
        if (result.Count != size)
        {
          throw new ArgumentException("the size of result vector must be " + size);
        }
      }
      else
      {
        result.Clone();
        result = new Vector(size);
      }
    }

    protected static void VerifyOrCreateResult<TVector>(Int32 size, ref TVector result) where TVector : Vector, new()
    {
      if (result != null)
      {
        if (result.Count != size)
        {
          throw new ArgumentException("the size of result vector must be " + size);
        }
      }
      else
      {
        result = new TVector();
      }
    }

    #endregion
  }
}
