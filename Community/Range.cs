namespace System
{
  /// <summary>
  /// Represents a closed interval between a minimum value and a maximum value.
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  public struct Range<TValue> where TValue : IComparable
  {
    /// <summary>
    /// The minimum value of the range.
    /// </summary>
    public readonly TValue Minimum;

    /// <summary>
    /// The maximum value of the range.
    /// </summary>
    public readonly TValue Maximum;

    /// <summary>
    /// Initializes a range using a minimum- and a maximum value.
    /// </summary>
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

    public override Boolean Equals(Object other)
    {
      if (other is Range<TValue>)
      {
        var otherRange = (Range<TValue>) other;

        return Maximum.Equals(otherRange.Maximum) && Minimum.Equals(otherRange.Minimum);
      }

      return false;
    }

    public override String ToString()
    {
      return String.Format("{0} - {1}", Minimum, Maximum);
    }
  }
}
