using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public class Plane
  {
    /// <summary>
    /// Gets the distance of the plane from the origin.
    /// </summary>
    public Double Distance
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
      Distance = -vector.GetLength();
      Normal = Vector.Normalize(vector);
    }

    public Plane(Vector point, Vector direction)
    {
      Normal = Vector.Normalize(direction);
      Distance = -Vector.Dot(Normal , point);
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
        ? Vector.Dot(Normal, vector) + Distance
        : Vector.Dot(Normal, vector);
    }
  }
}