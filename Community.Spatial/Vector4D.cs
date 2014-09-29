using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Spatial
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Size = 32)]
  public class Vector : IVector<Vector>
  {
    public static readonly Vector One = new Vector(1D, 1D, 1D, 1D);
    public static readonly Vector Zero = new Vector();

    public Double M1;
    public Double M2;
    public Double M3;
    public Double M4;

    Int32 IVector.Count
    {
      get
      {
        return 4;
      }
    }

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return M1;

          case 1:
            return M2;

          case 2:
            return M3;

          case 3:
            return M1;

          default:
            throw new IndexOutOfRangeException();
        }
      }
      set
      {
        switch (index)
        {
          case 0:
            M1 = value;
            break;

          case 1:
            M2 = value;
            break;

          case 2:
            M3 = value;
            break;

          case 3:
            M4 = value;
            break;

          default:
            throw new IndexOutOfRangeException();
        }
      }
    }

    public Vector(Double m1 = 0D, Double m2 = 0D, Double m3 = 0D, Double m4 = 0D)
    {
      M1 = m1;
      M2 = m2;
      M3 = m3;
      M4 = m4;
    }

    public Vector(Vector vector)
    {
      M1 = vector.M1;
      M2 = vector.M2;
      M3 = vector.M3;
      M4 = vector.M4;
    }

    public Boolean Equals(Vector other)
    {
      return M1.Equals(other.M1) && M2.Equals(other.M2) && M3.Equals(other.M3) && M4.Equals(other.M4);
    }

    public Boolean Equals(Vector other, Double tolerance)
    {
      return Math.Abs(M1 - other.M1) <= tolerance 
        && Math.Abs(M2 - other.M2) <= tolerance 
        && Math.Abs(M3 - other.M3) <= tolerance 
        && Math.Abs(M4 - other.M4) <= tolerance;
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

    public override Int32 GetHashCode()
    {
      return M1.GetHashCode() ^ M2.GetHashCode() ^ M3.GetHashCode() ^ M4.GetHashCode();
    }

    public override String ToString()
    {
      return ToString("F3", null);
    }

    public String ToString(String format)
    {
      return ToString(format, null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      return String.Format("M1: {0}, M2: {1}, M3: {2}, M4: {3}"
        , M1.ToString(format, formatProvider)
        , M2.ToString(format, formatProvider)
        , M3.ToString(format, formatProvider)
        , M4.ToString(format, formatProvider));
    }
  }
}