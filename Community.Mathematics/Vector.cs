using System;
using System.Runtime.InteropServices;

namespace Community.Mathematics
{
  [StructLayout(LayoutKind.Explicit)]
  public struct Vector2D : IVector<Double>
  {
    [FieldOffset(0x00)]
    public Double A;

    [FieldOffset(0x08)]
    public Double B;

    [FieldOffset(0x00)]
    public Double X;

    [FieldOffset(0x08)]
    public Double Y;

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return A;
          case 1: return B;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: A = value; break;
          case 1: B = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

  [StructLayout(LayoutKind.Explicit)]
  public struct Vector2F : IVector<Single>
  {
    [FieldOffset(0x00)]
    public Single A;

    [FieldOffset(0x04)]
    public Single B;

    [FieldOffset(0x00)]
    public Single X;

    [FieldOffset(0x04)]
    public Single Y;

    public Single this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return A;
          case 1: return B;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: A = value; break;
          case 1: B = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

  [StructLayout(LayoutKind.Explicit)]
  public struct Vector3D : IVector<Double>
  {
    [FieldOffset(0x00)]
    public Double A;

    [FieldOffset(0x08)]
    public Double B;

    [FieldOffset(0x10)]
    public Double C;

    [FieldOffset(0x00)]
    public Double X;

    [FieldOffset(0x08)]
    public Double Y;

    [FieldOffset(0x10)]
    public Double Z;

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return A;
          case 1: return B;
          case 2: return C;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: A = value; break;
          case 1: B = value; break;
          case 2: C = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

  [StructLayout(LayoutKind.Explicit)]
  public struct Vector3F : IVector<Single>
  {
    [FieldOffset(0x00)]
    public Single A;

    [FieldOffset(0x04)]
    public Single B;

    [FieldOffset(0x08)]
    public Single C;

    [FieldOffset(0x00)]
    public Single X;

    [FieldOffset(0x04)]
    public Single Y;

    [FieldOffset(0x08)]
    public Single Z;

    public Single this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return A;
          case 1: return B;
          case 2: return C;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: A = value; break;
          case 1: B = value; break;
          case 2: C = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

  [StructLayout(LayoutKind.Explicit)]
  public struct Vector4D : IVector<Double>
  {
    [FieldOffset(0x00)]
    public Double A;

    [FieldOffset(0x08)]
    public Double B;

    [FieldOffset(0x10)]
    public Double C;

    [FieldOffset(0x18)]
    public Double D;

    [FieldOffset(0x00)]
    public Double X;

    [FieldOffset(0x08)]
    public Double Y;

    [FieldOffset(0x10)]
    public Double Z;

    [FieldOffset(0x18)]
    public Double W;

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return A;
          case 1: return B;
          case 2: return C;
          case 3: return D;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: A = value; break;
          case 1: B = value; break;
          case 2: C = value; break;
          case 3: D = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

  [StructLayout(LayoutKind.Explicit)]
  public struct Vector4F : IVector<Single>
  {
    [FieldOffset(0x00)]
    public Single A;

    [FieldOffset(0x04)]
    public Single B;

    [FieldOffset(0x08)]
    public Single C;

    [FieldOffset(0x0C)]
    public Single D;

    [FieldOffset(0x00)]
    public Single X;

    [FieldOffset(0x04)]
    public Single Y;

    [FieldOffset(0x08)]
    public Single Z;

    [FieldOffset(0x0C)]
    public Single W;

    public Single this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return A;
          case 1: return B;
          case 2: return C;
          case 3: return D;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: A = value; break;
          case 1: B = value; break;
          case 2: C = value; break;
          case 3: D = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

}
// <autogenerated>
//   This file was generated by T4 code generator Vector.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


