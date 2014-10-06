namespace System
{
  using Collections.Generic;
  using Linq;

  public static class ComparableExtensions
  {
    /// <summary>
    /// Returns the value in between the minimum and maximum values.
    /// </summary>
    public static TValue Clamp<TValue>(this TValue value, TValue? minimum = null, TValue? maximum = null) where TValue : struct, IComparable
    {
      if (minimum.HasValue && minimum.Value.CompareTo(value) > 0)
      {
        return minimum.Value;
      }

      if (maximum.HasValue && maximum.Value.CompareTo(value) < 0)
      {
        return maximum.Value;
      }

      return value;
    }

    /// <summary>
    /// Returns a collection where all the values in between the minimum and maximum values.
    /// </summary>
    public static IEnumerable<TValue> Clamp<TValue>(this IEnumerable<TValue> values, TValue? minimum = null, TValue? maximum = null) where TValue : struct, IComparable
    {
      return values.Select(value => value.Clamp(minimum, maximum));
    }
  }
}
