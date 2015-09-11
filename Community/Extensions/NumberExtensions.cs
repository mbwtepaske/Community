namespace System
{
  public static class NumberExtensions
  {
    /// <summary>
    /// Returns the value raised by the specified power.
    /// </summary>
    public static Int32 Power(this Int32 value, Int32 power)
    {
      if (power < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(power));
      }

      if (power > 0)
      {
        var result = value;

        for (var index = 1; index < power; index++)
        {
          result *= value;
        }

        return result;
      }

      return 1;
    }

    /// <summary>
    /// Returns the value raised by the specified power.
    /// </summary>
    public static Double Power(this Double value, Int32 power)
    {
      if (power > 0)
      {
        var result = value;

        for (int index = 1, count = Math.Abs(power); index < count; index++)
        {
          result *= value;
        }

        return power < 0 ? 1D / result : result;
      }
      
      if (power < 0)
      {
        return Power(value, Convert.ToDouble(power));
      }

      return 1;
    }

    /// <summary>
    /// Returns the value raised by the specified power.
    /// </summary>
    public static Double Power(this Double value, Double power)
    {
      return Math.Pow(value, power);
    }

    /// <summary>
    /// Returns the value raised by the specified power.
    /// </summary>
    public static Single Power(this Single value, Int32 power)
    {
      if (power != 0)
      {
        var result = Convert.ToDouble(value);

        for (int index = 1, count = Math.Abs(power); index < count; index++)
        {
          result *= value;
        }

        return Convert.ToSingle(power < 0 ? 1D / result : result);
      }

      return 0;
    }

    /// <summary>
    /// Returns the value raised by the specified power.
    /// </summary>
    public static Single Power(this Single value, Double power)
    {
      return Convert.ToSingle(Math.Pow(value, power));
    }
  }
}
