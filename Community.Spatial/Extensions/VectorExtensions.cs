using System;
using System.Linq;

namespace MathNet.Numerics.LinearAlgebra
{
  using Matrix = Matrix<double>;
  using Vector = Vector<double>;

  public static class VectorExtensions
  {
    /// <summary>
    /// Returns the length of the vector.
    /// </summary>
    public static double GetLength(this Vector vector)
    {
      return Math.Sqrt(vector.GetLengthSquare());
    }

    /// <summary>
    /// Returns the square length of the vector.
    /// </summary>
    public static double GetLengthSquare(this Vector vector)
    {
      return vector.Sum(value => value * value);
    }

    /// <summary>
    /// Normalizes the vector and returns itself.
    /// </summary>
    public static TVector Normalize<TVector>(this TVector vector) where TVector : Vector
    {
      var length = vector.GetLength();

      if (length.CompareTo(0D) != 0)
      {
        for (var index = 0; index < vector.Count; index++)
        {
          vector[index] /= length;
        }
      }

      return vector;
    }
  }
}