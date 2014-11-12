using System;

namespace System.Spatial
{
  internal static class Utility
  {
    public const Double DefaultTolerance = 1E-15;

    public static Boolean Equals(Double x, Double y, Double? tolerance = null)
    {
      var absoluteDifference = Math.Abs(x - y);

      if (!tolerance.HasValue)
      {
        var magitude = Math.Log10(absoluteDifference);

        tolerance = Math.Pow(10D, magitude) * DefaultTolerance;
      }
      
      return Math.Abs(x - y) <= tolerance;
    }
  }
}