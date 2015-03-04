namespace System
{
  public struct Range<TValue> where TValue : IComparable
  {
    public readonly TValue Maximum;
    public readonly TValue Minimum;

    public Range(TValue minimum, TValue maximum)
    {
      if (minimum.CompareTo(maximum) > 0)
      {
        throw new ArgumentException("minimum is greater than maximum");
      }

      Minimum = minimum;
      Maximum = maximum;
    }

    /// <summary>
    /// Returns true if the specified value falls within minimum and maximum values.
    /// </summary>
    public Boolean Contains(TValue value)
    {
      return Contains(value, false, true);
    }

    /// <summary>
    /// Returns true if the specified value falls within minimum and maximum values.
    /// </summary>
    public Boolean Contains(TValue value, Boolean maximumInclusive)
    {
      return Contains(value, maximumInclusive, true);
    }

    /// <summary>
    /// Returns true if the specified value falls within minimum and maximum values.
    /// </summary>
    public Boolean Contains(TValue value, Boolean maximumInclusive, Boolean minimumInclusive)
    {
      var minimumComparison = Minimum.CompareTo(value);

      if (minimumInclusive ? minimumComparison > 0 : minimumComparison >= 0)
      {
        return false;
      }

      var maximumComparison = Maximum.CompareTo(value);

      return maximumInclusive
        ? maximumComparison >= 0
        : maximumComparison > 0;
    }

    public override Int32 GetHashCode()
    {
      return Maximum.GetHashCode() ^ Minimum.GetHashCode();
    }
  }
}
