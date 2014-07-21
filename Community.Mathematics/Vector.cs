using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Community.Mathematics
{
  public struct Vector2D : IVector<Double>
  {
    public Double M1;
    public Double M2;

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return M1;
          case 1: return M2;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: M1 = value; break;
          case 1: M2 = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }

    public Vector2D(Double value)
    {
      M1 = value;
      M2 = value;
    }

    public Vector2D(Double m1, Double m2)
    {
      M1 = m1;
      M2 = m2;
    }

    public IEnumerator<Double> GetEnumerator()
    {
      yield return M1;
      yield return M2;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return "[" + String.Join(", ", this.Select(value => value.ToString(format, formatProvider))) + "]";
    }
  }

  public struct Vector2F : IVector<Single>
  {
    public Single M1;
    public Single M2;

    public Single this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return M1;
          case 1: return M2;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: M1 = value; break;
          case 1: M2 = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }

    public Vector2F(Single value)
    {
      M1 = value;
      M2 = value;
    }

    public Vector2F(Single m1, Single m2)
    {
      M1 = m1;
      M2 = m2;
    }

    public IEnumerator<Single> GetEnumerator()
    {
      yield return M1;
      yield return M2;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return "[" + String.Join(", ", this.Select(value => value.ToString(format, formatProvider))) + "]";
    }
  }

  public struct Vector3D : IVector<Double>
  {
    public Double M1;
    public Double M2;
    public Double M3;

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return M1;
          case 1: return M2;
          case 2: return M3;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: M1 = value; break;
          case 1: M2 = value; break;
          case 2: M3 = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }

    public Vector3D(Double value)
    {
      M1 = value;
      M2 = value;
      M3 = value;
    }

    public Vector3D(Double m1, Double m2, Double m3)
    {
      M1 = m1;
      M2 = m2;
      M3 = m3;
    }

    public IEnumerator<Double> GetEnumerator()
    {
      yield return M1;
      yield return M2;
      yield return M3;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return "[" + String.Join(", ", this.Select(value => value.ToString(format, formatProvider))) + "]";
    }
  }

  public struct Vector3F : IVector<Single>
  {
    public Single M1;
    public Single M2;
    public Single M3;

    public Single this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return M1;
          case 1: return M2;
          case 2: return M3;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: M1 = value; break;
          case 1: M2 = value; break;
          case 2: M3 = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }

    public Vector3F(Single value)
    {
      M1 = value;
      M2 = value;
      M3 = value;
    }

    public Vector3F(Single m1, Single m2, Single m3)
    {
      M1 = m1;
      M2 = m2;
      M3 = m3;
    }

    public IEnumerator<Single> GetEnumerator()
    {
      yield return M1;
      yield return M2;
      yield return M3;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return "[" + String.Join(", ", this.Select(value => value.ToString(format, formatProvider))) + "]";
    }
  }

  public struct Vector4D : IVector<Double>
  {
    public Double M1;
    public Double M2;
    public Double M3;
    public Double M4;

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return M1;
          case 1: return M2;
          case 2: return M3;
          case 3: return M4;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: M1 = value; break;
          case 1: M2 = value; break;
          case 2: M3 = value; break;
          case 3: M4 = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }

    public Vector4D(Double value)
    {
      M1 = value;
      M2 = value;
      M3 = value;
      M4 = value;
    }

    public Vector4D(Double m1, Double m2, Double m3, Double m4)
    {
      M1 = m1;
      M2 = m2;
      M3 = m3;
      M4 = m4;
    }

    public IEnumerator<Double> GetEnumerator()
    {
      yield return M1;
      yield return M2;
      yield return M3;
      yield return M4;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return "[" + String.Join(", ", this.Select(value => value.ToString(format, formatProvider))) + "]";
    }
  }

  public struct Vector4F : IVector<Single>
  {
    public Single M1;
    public Single M2;
    public Single M3;
    public Single M4;

    public Single this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0: return M1;
          case 1: return M2;
          case 2: return M3;
          case 3: return M4;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0: M1 = value; break;
          case 1: M2 = value; break;
          case 2: M3 = value; break;
          case 3: M4 = value; break;
          default: throw new ArgumentOutOfRangeException();
        }
      }
    }

    public Vector4F(Single value)
    {
      M1 = value;
      M2 = value;
      M3 = value;
      M4 = value;
    }

    public Vector4F(Single m1, Single m2, Single m3, Single m4)
    {
      M1 = m1;
      M2 = m2;
      M3 = m3;
      M4 = m4;
    }

    public IEnumerator<Single> GetEnumerator()
    {
      yield return M1;
      yield return M2;
      yield return M3;
      yield return M4;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return "[" + String.Join(", ", this.Select(value => value.ToString(format, formatProvider))) + "]";
    }
  }

}
// <autogenerated>
//   This file was generated by T4 code generator Vector.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


