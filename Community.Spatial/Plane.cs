using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public class Plane
  {
    public Double Constant
    {
      get;
      private set;
    }

    public Vector Normal
    {
      get;
      private set;
    }

    public Plane(Vector vector)
    {
      Constant = -vector.GetLength();
      Normal = Vector.Normalize(vector);
    }
  }
}
