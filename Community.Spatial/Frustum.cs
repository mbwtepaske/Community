using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  public static class Frustum
  {
    public static Boolean Contains(IEnumerable<Vector> planes, Vector vector)
    {
      if (planes == null)
      {
        throw new ArgumentNullException("planes");
      }

      if (vector == null)
      {
        throw new ArgumentNullException("vector");
      }
      
      return planes.All(plane => plane.DotProduct(vector) >= 0);
    }
  }
}