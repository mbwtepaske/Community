using System;
using System.Collections.Generic;
using System.Linq;

namespace Community.Mathematics
{
  public static class ScalarArrayExtensions
  {
    public static Double GetLength(this Vector<Double> vector)
    {
      if (vector == null)
      {
        throw new NullReferenceException("vector");
      }

      return Math.Sqrt(vector.Sum(scalar => scalar * scalar));
    }

    public static Double GetLengthSquare(this Vector<Double> vector)
    {
      if (vector == null)
      {
        throw new NullReferenceException("vector");
      }

      return vector.Sum(scalar => scalar * scalar);
    }

    public static Vector<Double> Interpolate(this Vector<Double> vector, Vector<Double> otherVector, Double value)
    {
      if (vector == null)
      {
        throw new NullReferenceException("vector");
      }

      if (otherVector == null)
      {
        throw new ArgumentNullException("otherVector");
      }

      if (vector.Size != otherVector.Size)
      {
        throw new ArgumentOutOfRangeException("otherVector");
      }

      var result = new Vector<Double>(vector.Size);

      for (var index = 0; index < result.Size; index++)
      {
        //result[index] = 
      }

      return result;
    }
  }
}
