﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public class Frustum : IEnumerable<Plane>
  {
    /// <summary>
    /// Gets the planes describing the frustum.
    /// </summary>
    public Plane[] Planes
    {
      get;
      private set;
    }

    public Frustum(params Plane[] planes)
    {
      if (planes == null)
      {
        throw new ArgumentNullException("planes");
      }

      if (planes.Length % 2 != 0)
      {
        throw new ArgumentException("The amount of planes must be even");
      }

      if (!planes.Same(plane => plane.Normal.Size))
      {
        throw new ArgumentException("All planes must have the same dimension");
      }

      Planes = planes;
    }

    public Frustum(IEnumerable<Plane> planes) : this(planes.ToArray())
    {
    }

    IEnumerator<Plane> IEnumerable<Plane>.GetEnumerator()
    {
      return Planes.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Planes.GetEnumerator();
    }
  }
}