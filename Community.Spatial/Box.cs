using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  /// <summary>
  /// Represents a N-dimensional hyperrectangle (a.k.a. orthotope).
  /// </summary>
  public class Box
  {
    public Vector Center
    {
      get
      {
        return Interpolation.Linear(Minimum, Maximum, 0.5D);
      }
    }

    public Vector Maximum
    {
      get;
      set;
    }

    public Vector Minimum
    {
      get;
      set;
    }

    public Box(Vector minimum, Vector maximum)
    {
      if (minimum == null)
      {
        throw new ArgumentNullException("minimum");
      }

      if (maximum == null)
      {
        throw new ArgumentNullException("maximum");
      }

      if (minimum.Size != maximum.Size)
      {
        throw new ArgumentException("minimum and maximum size must be the same");
      }

      Minimum = minimum;
      Maximum = maximum;
    }

    public override String ToString()
    {
      return base.ToString();
    }
  }
}
