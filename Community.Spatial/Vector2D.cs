using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public sealed class Vector2D : Vector
  {
    private const Int32 Count = 2;

    public static readonly Vector2D UnitX = new Vector2D(1D, 0D);
    public static readonly Vector2D UnitY = new Vector2D(0D, 1D);

    public Double X
    {
      get
      {
        return this[0];
      }
      set
      {
        this[0] = value;
      }
    }

    public Double Y
    {
      get
      {
        return this[1];
      }
      set
      {
        this[1] = value;
      }
    }

    public Vector2D()
      : base(Count)
    {
    }

    public Vector2D(Double x, Double y)
      : base(x, y)
    {
    }

    public Vector2D(IVector vector)
      : base(vector.Take(Count).ToArray())
    {
      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }

      if (vector.Size != 2)
      {
        throw new ArgumentException("vector size must be 2");
      }
    }
  }
}
