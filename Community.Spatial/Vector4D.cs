namespace System.Spatial
{
  using Linq;

  public sealed class Vector4D : Vector
  {
    private const Int32 Size = 4;

    public static readonly Vector4D UnitX = new Vector4D(1D, 0D, 0D, 0D);
    public static readonly Vector4D UnitY = new Vector4D(0D, 1D, 0D, 0D);
    public static readonly Vector4D UnitZ = new Vector4D(0D, 0D, 1D, 0D);
    public static readonly Vector4D UnitW = new Vector4D(0D, 0D, 0D, 1D);
    public new static readonly Vector4D Zero = new Vector4D();

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

    public Double Z
    {
      get
      {
        return this[2];
      }
      set
      {
        this[2] = value;
      }
    }

    public Double W
    {
      get
      {
        return this[3];
      }
      set
      {
        this[3] = value;
      }
    }

    public Vector4D()
      : base(Size)
    {
    }

    public Vector4D(Double x, Double y, Double z, Double w)
      : base(x, y, z, w)
    {
    }

    public Vector4D(params Double[] values)
      : base(values.Take(Size).ToArray())
    {
      if (values.Length != Size)
      {
        throw new ArgumentException("vector size must be " + Size);
      }
    }
  }
}