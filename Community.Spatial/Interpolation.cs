using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public static class Interpolation
  {
    public static Double Cubic(Double left, Double control, Double right, Double value)
    {
      var inverseValue = 1D - value;

      return inverseValue * inverseValue * left 
        + 2D * inverseValue * value * control 
        + value * value * right;
    }

    public static Double Linear(Double left, Double right, Double value)
    {
      return left + (right - left) * value;
    }

    public static TVector Linear<TVector>(TVector left, TVector right, TVector values) where TVector : Vector
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

      if (left.Size != right.Size || left.Size != values.Size)
      {
        throw new ArgumentException("all the specified vectors must have the same size");
      }

      return (TVector)values.SelectMany(value => Enumerable.Zip(left, right, (x, y) => Linear(x, y, value))).ToArray();
    }

    public static Double Polynomic(Double left, Double right, Double value, params Double[] coefficients)
    {
      throw new NotImplementedException();
    }

    public static Double Quadratic(Double left, Double controlLeft, Double controlRight, Double right, Double value)
    {
      var inverseValue = 1D - value;

      return inverseValue * inverseValue * inverseValue * left 
        + 3D * (inverseValue * inverseValue) * value * controlLeft 
        + 3D * inverseValue * (value * value) * controlRight
        + value * value * value * right;
    }

    public static Double Spherical(Double left, Double right, Double value, Double radius)
    {
      throw new NotImplementedException();
    }
  }
}
