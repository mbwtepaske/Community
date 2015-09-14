using System.Collections.Generic;

namespace System.Spatial
{
  /// <summary>
  /// Represents a double-precision floating-point comparer that has a tolerance.
  /// </summary>
  public class DoubleComparison : IComparer<Double>, IEqualityComparer<Double>
  {
    /// <summary>
    /// Gets a zero-tolerance double-precision equality comparer.
    /// </summary>
    public static readonly DoubleComparison Default = new DoubleComparison();

    /// <summary>
    /// Gets a 1e-3 tolerance double-precision equality comparer.
    /// </summary>
    public static DoubleComparison Milli = new DoubleComparison(1E-3);

    /// <summary>
    /// Gets a 1e-6 tolerance double-precision equality comparer.
    /// </summary>
    public static DoubleComparison Micro = new DoubleComparison(1E-6);

    /// <summary>
    /// Gets a 1e-9 tolerance double-precision equality comparer.
    /// </summary>
    public static DoubleComparison Nano = new DoubleComparison(1E-9);

    /// <summary>
    /// Gets a 1e-12 tolerance double-precision equality comparer.
    /// </summary>
    public static DoubleComparison Pico = new DoubleComparison(1E-12);

    public Double Tolerance
    {
      get;
      private set;
    }

    public DoubleComparison(Double tolerance = 0D)
    {
      Tolerance = tolerance;
    }

    public Int32 Compare(Double left, Double right)
    {
      return Equals(left, right) ? 0 : left.CompareTo(right);
    }

    public Boolean Equals(Double left, Double right)
    {
      return Math.Abs(left - right) <= Tolerance;
    }

    public Int32 GetHashCode(Double value)
    {
      return value.GetHashCode();
    }
  }
}