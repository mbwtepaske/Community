using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public partial class Matrix
  {
    public static Matrix operator *(Matrix left, Double right)
    {
      return Multiply(left, right);
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
      return Multiply(left, right);
    }

    public static implicit operator Double[](Matrix matrix)
    {
      return matrix.ToArray();
    }

    public static implicit operator Single[](Matrix matrix)
    {
      return matrix.Select(Convert.ToSingle).ToArray();
    }
  }
}
