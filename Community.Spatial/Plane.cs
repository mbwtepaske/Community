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

    public Double Dot(Vector vector)
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }

      if (Normal.Size != vector.Size)
      {
        throw new ArgumentException("vector must be the same dimension as the plane");
      }

      return vector.IsNormal
        ? Vector.Dot(Normal, vector) + Constant
        : Vector.Dot(Normal, vector);
    }
  }
}