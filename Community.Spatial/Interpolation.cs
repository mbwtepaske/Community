namespace System.Spatial
{
  using Linq;

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

    public static Vector Linear(Vector left, Vector right, Double value)
    {
      if (left == null)
      {
        throw new ArgumentNullException(nameof(left));
      }

      if (right == null)
      {
        throw new ArgumentNullException(nameof(right));
      }
      
      if (left.Count != right.Count)
      {
        throw new ArgumentException("left and right vectors must have the same size");
      }

      return new Vector(Enumerable.Zip(left, right, (l, r) => Linear(l, r, value)).ToArray());
    }

    public static Vector Linear(Vector left, Vector right, Vector values)
    {
      if (left == null)
      {
        throw new ArgumentNullException(nameof(left));
      }

      if (right == null)
      {
        throw new ArgumentNullException(nameof(right));
      }

      if (values == null)
      {
        throw new ArgumentNullException(nameof(values));
      }

      if (left.Count != right.Count || left.Count != values.Count)
      {
        throw new ArgumentException("all the specified vectors must have the same size");
      }

      return new Vector(Enumerable
        .Range(0, values.Count)
        .Select(index => Linear(left[index], right[index], values[index]))
        .ToArray());
    }

    public static Double Logarithmic(Double left, Double right, Double value)
    {
      return Math.Pow(right, value) * Math.Pow(left, 1D - value);
    }

    public static Vector Logarithmic(Vector left, Vector right, Double value)
    {
      if (left == null)
      {
        throw new ArgumentNullException(nameof(left));
      }

      if (right == null)
      {
        throw new ArgumentNullException(nameof(right));
      }

      if (left.Count != right.Count)
      {
        throw new ArgumentException("left and right vectors must have the same size");
      }

      return Vector.Build.Dense(Enumerable.Zip(left, right, (l, r) => Logarithmic(l, r, value)).ToArray());
    }

    public static Vector Logarithmic(Vector left, Vector right, Vector values)
    {
      if (left == null)
      {
        throw new ArgumentNullException(nameof(left));
      }

      if (right == null)
      {
        throw new ArgumentNullException(nameof(right));
      }

      if (values == null)
      {
        throw new ArgumentNullException(nameof(values));
      }

      if (left.Count != right.Count || left.Count != values.Count)
      {
        throw new ArgumentException("all the specified vectors must have the same size");
      }

      return Vector.Build.Dense(Enumerable
        .Range(0, values.Count)
        .Select(index => Logarithmic(left[index], right[index], values[index]))
        .ToArray());
    }

    public static Double Quadratic(Double left, Double controlLeft, Double controlRight, Double right, Double value)
    {
      var inverseValue = 1D - value;

      return inverseValue * inverseValue * inverseValue * left 
        + 3D * (inverseValue * inverseValue) * value * controlLeft 
        + 3D * inverseValue * (value * value) * controlRight
        + value * value * value * right;
    }

    //public static Double Spherical(Double left, Double right, Double value, Double radius)
    //{
    //  throw new NotImplementedException();
    //}
  }
}
