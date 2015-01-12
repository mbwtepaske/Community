using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Spatial
{
  public sealed class Quaternion : MathNet.Numerics.LinearAlgebra.Double.DenseVector
  {
    public Double X
    {
      get
      {
        return At(0);
      }
      set
      {
        At(0, value);
      }
    }

    public Double Y
    {
      get
      {
        return At(1);
      }
      set
      {
        At(1, value);
      }
    }

    public Double Z
    {
      get
      {
        return At(2);
      }
      set
      {
        At(2, value);
      }
    }

    public Double W
    {
      get
      {
        return At(3);
      }
      set
      {
        At(3, value);
      }
    }

    public Quaternion(Double x, Double y, Double z, Double w) : base(new[]
    {
      x, 
      y, 
      z, 
      w
    })
    {
    }
  }
}
