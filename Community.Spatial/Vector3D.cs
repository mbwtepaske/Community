namespace System.Spatial
{
  using Linq;

  public sealed class Vector3D : Vector
  {
    private const Int32 Size = 3;

    public static readonly Vector3D UnitX = new Vector3D(1D, 0D, 0D);
    public static readonly Vector3D UnitY = new Vector3D(0D, 1D, 0D);
    public static readonly Vector3D UnitZ = new Vector3D(0D, 0D, 1D);
    public new static readonly Vector3D Zero = new Vector3D();

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

    public Vector3D()
      : base(Size)
    {
    }

    public Vector3D(Double x, Double y, Double z)
      : base(x, y, z)
    {
    }

    public Vector3D(params Double[] values)
      : base(values.Take(Size).ToArray())
    {
      if (values.Length != Size)
      {
        throw new ArgumentException("vector size must be " + Size);
      }
    }

    public static Vector3D Cross(Vector3D left, Vector3D right)
    {
      var result = default(Vector3D);

      return Cross(left, right, ref result);
    }

    public static Vector3D Cross(Vector3D left, Vector3D right, ref Vector3D result)
    {
      Verify(left, right);

      if (result == null)
      {
        result = new Vector3D();
      }

      result.X = left.Y * right.Z - left.Z * right.Y;
      result.Y = left.Z * right.X - left.X * right.Z;
      result.Z = left.X * right.Y - left.Y * right.X;

      return result;
    }

    //public static Vector3D Divide(Vector3D left, Double right, Vector3D result)
    //{
    //  Verify(left, result);

    //  return result;
    //}
    
    //public static Vector3D Normalize(Vector3D vector, ref Vector3D result)
    //{
    //  Verify(vector, ref result);

    //  return Divide(vector, vector.GetLength(), ref result);
    //}

    //private static void Verify(Vector3D vector, ref Vector3D result)
    //{
    //  if (vector == null)
    //  {
    //    throw new ArgumentNullException("vector");
    //  }

    //  if (result == null)
    //  {
    //    result = new Vector3D();
    //  }
    //}
  }
}