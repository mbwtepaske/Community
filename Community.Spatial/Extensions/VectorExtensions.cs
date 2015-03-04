using System;
using System.Linq;

namespace MathNet.Numerics.LinearAlgebra
{
  using Matrix = Matrix<double>;
  using Vector = Vector<double>;

  public static class VectorExtensions
  {
    /// <summary>
    /// Returns the magnitude of the vector.
    /// </summary>
    public static double Magnitude(this Vector vector)
    {
      return Math.Sqrt(vector.MagnitudeSquare());
    }

    /// <summary>
    /// Returns the square magnitude length of the vector.
    /// </summary>
    public static double MagnitudeSquare(this Vector vector)
    {
      return vector.Sum(value => value * value);
    }

    /// <summary>
    /// Normalizes the vector and returns itself.
    /// </summary>
    public static Vector Normalize(this Vector vector)
    {
      var length = vector.Magnitude();

      return length.CompareTo(0D) != 0
        ? vector.Divide(length)
        : vector;
    }
  }
}