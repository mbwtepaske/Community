using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public static class VectorOperation
  {
    #region Get Length

    public static Double GetLength<TVector>(TVector vector) where TVector : IVector, new()
    {
      return Math.Sqrt(GetLengthSquare(ref vector));
    }

    public static Double GetLength<TVector>(ref TVector vector) where TVector : IVector, new()
    {
      return Math.Sqrt(GetLengthSquare(ref vector));
    }

    public static Double GetLengthSquare<TVector>(TVector vector) where TVector : IVector, new()
    {
      return vector.Sum(value => value * value);
    }

    public static Double GetLengthSquare<TVector>(ref TVector vector) where TVector : IVector, new()
    {
      return vector.Sum(value => value * value);
    }

    #endregion

    #region Linear Interpolation

    private static Double Lerp(Double left, Double right, Double value)
    {
      return left + (right - left) * value;
    }

    /// <summary>
    /// Linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="TVector">
    /// A type implementing the <see cref="T:IVector"/>-interface and has a default constructor.
    /// </typeparam>
    public static TVector Lerp<TVector>(TVector left, TVector right, Double value) where TVector : IVector, new()
    {
      var result = default(TVector);

      return Lerp(ref left, ref right, value, out result);
    }

    /// <summary>
    /// Linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="TVector">
    /// A type implementing the <see cref="T:IVector"/>-interface and has a default constructor.
    /// </typeparam>
    public static TVector Lerp<TVector>(ref TVector left, ref TVector right, Double value) where TVector : IVector, new()
    {
      var result = default(TVector);

      return Lerp(ref left, ref right, value, out result);
    }

    /// <summary>
    /// Linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="TVector">
    /// A type implementing the <see cref="T:IVector"/>-interface and has a default constructor.
    /// </typeparam>
    public static TVector Lerp<TVector>(ref TVector left, ref TVector right, Double value, out TVector result) where TVector : IVector, new()
    {
      VerifyComponentCount(ref left, ref right);

      result = new TVector();

      for (var index = 0; index < left.Count; index++)
      {
        result[index] = Lerp(left[index], right[index], value);
      }

      return result;
    }

    /// <summary>
    /// Linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="TVector">
    /// A type implementing the <see cref="T:IVector"/>-interface and has a default constructor.
    /// </typeparam>
    public static TVector Lerp<TVector>(TVector left, TVector right, TVector values) where TVector : IVector, new()
    {
      var result = default(TVector);

      return Lerp(ref left, ref right, ref values, out result);
    }

    /// <summary>
    /// Linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="TVector">
    /// A type implementing the <see cref="T:IVector"/>-interface and has a default constructor.
    /// </typeparam>
    public static TVector Lerp<TVector>(ref TVector left, ref TVector right, ref TVector values) where TVector : IVector, new()
    {
      var result = default(TVector);

      return Lerp(ref left, ref right, ref values, out result);
    }

    /// <summary>
    /// Linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="TVector">
    /// A type implementing the <see cref="T:IVector"/>-interface and has a default constructor.
    /// </typeparam>
    public static TVector Lerp<TVector>(ref TVector left, ref TVector right, ref TVector values, out TVector result) where TVector : IVector, new()
    {
      VerifyComponentCount(ref left, ref right, ref values);
      
      result = new TVector();

      for (var index = 0; index < left.Count; index++)
      {
        result[index] = Lerp(left[index], right[index], values[index]);
      }

      return result;
    }

    #endregion

    #region Addition

    public static TVector Add<TVector>(ref TVector left, ref TVector right, out TVector result) where TVector : IVector, new()
    {
      VerifyComponentCount(ref left, ref right);

      result = new TVector();

      for (var index = 0; index < result.Count; index++)
      {
        result[index] = left[index] + right[index];
      }

      return result;
    }

    #endregion

    private static void VerifyComponentCount<TVector>(ref TVector left, ref TVector right) where TVector : IVector, new()
    {
      if (left.Count != right.Count)
      {
        throw new ArgumentException("vector left and right are not equal in size");
      }
    }

    private static void VerifyComponentCount<TVector>(ref TVector left, ref TVector right, ref TVector values) where TVector : IVector, new()
    {
      if (left.Count != right.Count || left.Count != values.Count)
      {
        throw new ArgumentException("vector left, right and values are not equal in size");
      }
    }
  }
}
