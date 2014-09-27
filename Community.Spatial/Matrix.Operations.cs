using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public partial struct Matrix
  {
    public static Matrix Multiply(Matrix left, Double right)
    {
      var result = new Matrix();

      return Multiply(ref left, right, out result);
    }

    public static Matrix Multiply(ref Matrix left, Double right)
    {
      var result = new Matrix();

      return Multiply(ref left, right, out result);
    }

    public static Matrix Multiply(ref Matrix left, Double right, out Matrix result)
    {
      result.M11 = left.M11 * right;
      result.M12 = left.M12 * right;
      result.M13 = left.M13 * right;
      result.M14 = left.M14 * right;
      result.M21 = left.M21 * right;
      result.M22 = left.M22 * right;
      result.M23 = left.M23 * right;
      result.M24 = left.M24 * right;
      result.M31 = left.M31 * right;
      result.M32 = left.M32 * right;
      result.M33 = left.M33 * right;
      result.M34 = left.M34 * right;
      result.M41 = left.M41 * right;
      result.M42 = left.M42 * right;
      result.M43 = left.M43 * right;
      result.M44 = left.M44 * right;

      return result;
    }

    public static Matrix Multiply(Matrix left, Matrix right)
    {
      var result = new Matrix();

      return Multiply(ref left, ref right, out result);
    }

    public static Matrix Multiply(ref Matrix left, ref Matrix right)
    {
      var result = new Matrix();

      return Multiply(ref left, ref right, out result);
    }

    public static Matrix Multiply(ref Matrix left, ref Matrix right, out Matrix result)
    {
      result.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41;
      result.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42;
      result.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43;
      result.M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44;
      result.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41;
      result.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42;
      result.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43;
      result.M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44;
      result.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41;
      result.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42;
      result.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43;
      result.M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44;
      result.M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41;
      result.M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42;
      result.M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43;
      result.M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44;

      return result;
    }

    public static Matrix operator *(Matrix left, Double right)
    {
      return Multiply(ref left, right);
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
      return Multiply(ref left, ref right);
    }

    public static implicit operator Double[] (Matrix matrix)
    {
      return matrix.ToArray();
    }

    public static implicit operator Single[] (Matrix matrix)
    {
      return matrix.Select(Convert.ToSingle).ToArray();
    }
  }
}
