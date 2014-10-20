using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  /// <summary>
  /// Represents a N-dimensional hyperrectangle (a.k.a. orthotope).
  /// </summary>
  public class Domain
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
      private set;
    }

    public Vector Minimum
    {
      get;
      private set;
    }

    public Domain(Vector minimum, Vector maximum)
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

    public static Domain FromCenter(Vector center, Double radius)
    {
      if (center == null)
      {
        throw new ArgumentNullException("center");
      }
      
      if (radius < 0D)
      {
        throw new ArgumentException("radius must be zero or greater");
      }

      return new Domain(center - radius, center + radius);
    }

    public static Domain FromCenter(Vector center, Vector radii)
    {
      if (center == null)
      {
        throw new ArgumentNullException("center");
      }

      if (radii == null)
      {
        throw new ArgumentNullException("radii");
      }

      if (center.Size != radii.Size)
      {
        throw new ArgumentException("center and radii size must be the same");
      }

      if (radii.Any(radius => radius < 0D))
      {
        throw new ArgumentException("radii elements must be zero or greater");
      }

      return new Domain(center - radii, center + radii);
    }
  }
}
