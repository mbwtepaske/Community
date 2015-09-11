using System;
using System.Linq;
using System.Numerics;

namespace MathNet.Numerics.LinearAlgebra
{
  //using Matrix = Matrix<double>;
  //using Vector = Vector<double>;

  //public static class VectorExtensions
  //{
  //  /// <summary>
  //  /// Returns the magnitude of the vector.
  //  /// </summary>
  //  public static double Magnitude(this Vector vector)
  //  {
  //    return Math.Sqrt(vector.MagnitudeSquare());
  //  }

  //  /// <summary>
  //  /// Returns the square magnitude length of the vector.
  //  /// </summary>
  //  public static double MagnitudeSquare(this Vector vector)
  //  {
  //    return vector.Sum(value => value * value);
  //  }

    /// <summary>
    /// Returns a normalized vector.
    /// </summary>
    public static Vector Normalize(this Vector vector)
    {
      var length = vector.Magnitude();
  //  /// <summary>
  //  /// Normalizes the vector and returns itself.
  //  /// </summary>
  //  public static Vector Normalize(this Vector vector)
  //  {
  //    var length = vector.Magnitude();

      return length.CompareTo(0D) != 0
        ? vector.Divide(length)
        : vector;
    }

    public static Vector2 ToVector2(this Vector vector)
    {
      var result = default(Vector2);

      ToVector2(vector, out result);

      return result;
    }

    public static void ToVector2(this Vector vector, out Vector2 result)
    {
      result.X = Convert.ToSingle(vector.ElementAtOrDefault(0));
      result.Y = Convert.ToSingle(vector.ElementAtOrDefault(1));
    }

    public static Vector3 ToVector3(this Vector vector)
    {
      var result = default(Vector3);

      ToVector3(vector, out result);

      return result;
    }

    public static void ToVector3(this Vector vector, out Vector3 result)
    {
      result.X = Convert.ToSingle(vector.ElementAtOrDefault(0));
      result.Y = Convert.ToSingle(vector.ElementAtOrDefault(1));
      result.Z = Convert.ToSingle(vector.ElementAtOrDefault(2));
    }

    public static Vector4 ToVector4(this Vector vector)
    {
      var result = default(Vector4);

      ToVector4(vector, out result);

      return result;
    }

    public static void ToVector4(this Vector vector, out Vector4 result)
    {
      result.X = Convert.ToSingle(vector.ElementAtOrDefault(0));
      result.Y = Convert.ToSingle(vector.ElementAtOrDefault(1));
      result.Z = Convert.ToSingle(vector.ElementAtOrDefault(2));
      result.W = Convert.ToSingle(vector.ElementAtOrDefault(3));
    }
  }
}