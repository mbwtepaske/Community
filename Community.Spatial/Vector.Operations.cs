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

      if (left.Size != right.ColumnCount)
      {
        throw new ArgumentException("column count of the matrix must match the size of the vector");
      }

      if (left.Size != right.RowCount)
      {
        throw new ArgumentException("row count of the matrix must match the size of the vector");
      }

      for (var index = 0; index < left.Size; index++)
      {
        result[index] = left.Zip(right.GetRow(index), Multiply).Sum();
      }

      return result;
    }

    #endregion

    private static Vector Operation(Vector vector, Double value, Func<Double, Double, Double> operation)
    {
      var result = default(Vector);

      return Operation(vector, value, ref result, operation);
    }

    private static Vector Operation(Vector vector, Double value, ref Vector result, Func<Double, Double, Double> operation)
    {
      Verify(vector, ref result);

      for (var index = 0; index < result.Size; index++)
      {
        result[index] = operation(vector[index], value);
      }

      return result;
    }

    private static Vector Operation(Vector left, Vector right, Func<Double, Double, Double> operation)
    {
      var result = default(Vector);

      return Operation(left, right, ref result, operation);
    }

    private static Vector Operation(Vector left, Vector right, ref Vector result, Func<Double, Double, Double> operation)
    {
      Verify(left, right, ref result);
      
      for (var index = 0; index < result.Size; index++)
      {
        result[index] = operation(left[index], right[index]);
      }

      return result;
    }

    #endregion

    #region Products

    public static Double Dot(Vector left, Vector right)
    {
      Verify(left, right);

      return left.Zip(right, Multiply).Sum();
    }

    #endregion

    #region Verification

    private static void Verify(Vector vector, ref Vector result)
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }

      VerifyOrCreateResult(vector.Size, ref result);
    }

    private static void Verify(Vector left, Vector right)
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
        throw new ArgumentException("vector left and right are not equal in size");
      }
    }

    private static void Verify(Vector left, Vector right, ref Vector result)
    {
      Verify(left, right);
      VerifyOrCreateResult(left.Size, ref result);
    }

    private static void VerifyOrCreateResult(Int32 size, ref Vector result)
    {
      if (result != null)
      {
        if (result.Size != size)
        {
          throw new ArgumentException("the size of result vector must be " + size);
        }
      }
      else
      {
        result = new Vector(size);
      }
    }

    #endregion
  }
}
