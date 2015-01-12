using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public sealed class Vector2D : Vector
  {
    private const Int32 Size = 2;

    public static readonly Vector2D UnitX = new Vector2D(1D, 0D);
    public static readonly Vector2D UnitY = new Vector2D(0D, 1D);
    public new static readonly Vector2D Zero = new Vector2D();

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
      : base(Size)
    {
    }

    public Vector2D(Double x, Double y)
      : base(x, y)
    {
    }

    public Vector2D(params Double[] values)
      : base(values.Take(Size).ToArray())
    {
      if (values.Length != Size)
      {
        throw new ArgumentException("vector size must be " + Size);
      }
    }
  }
}
