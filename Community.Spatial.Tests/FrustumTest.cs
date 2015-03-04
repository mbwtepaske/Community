using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial.Tests
{
  using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

  [TestClass]
  public class FrustumTest
  {
    [TestMethod]
    public void ContainsTest()
    {
      const Double backPlaneDistance = 6.5;
      const Double frontPlaneWidth = 2.0;
      const Double frontPlaneHeight = 1.5;
      const Double frontPlaneDistance = 0.5;

      var matrix = Matrix4D.Perspective(frontPlaneWidth, frontPlaneHeight, frontPlaneDistance, backPlaneDistance); 
      var frustum = Matrix4D.Frustum(matrix);

      var inside = new[] 
      {
        // Center on the front-plane
        Vector3D.Create(0, 0, frontPlaneDistance),

        // Center in the middle of the front- and back-plane.
        Vector3D.Create(0, 0, (backPlaneDistance - frontPlaneDistance) * 0.5 + frontPlaneDistance),

        // Center on the back-plane
        Vector3D.Create(0, 0, backPlaneDistance),

        // Top-left in the middle of the front- and back-plane.
        Vector3D.Create(-14.0, +10.5, (backPlaneDistance - frontPlaneDistance) * 0.5 + frontPlaneDistance)
      };

      foreach (var coordinate in inside)
      {
        Assert.IsTrue(frustum.Select(plane => plane.DotProduct(coordinate)).All(value => DoubleComparison.Milli.Compare(value, 0D) >= 0), Format(coordinate, frustum));
      }

      var outside = new[] 
      {
        Vector3D.Create(+0.0, +0.0, -1.0),
        Vector3D.Create(+0.0, +2.0, +1.5),
        Vector3D.Create(+0.0, +0.0, +2.1),
      };

      foreach (var coordinate in outside/*.Select(vector => Vector.TransformCoordinate(vector, matrix))*/)
      {
        Assert.IsFalse(frustum.Select(plane => plane.DotProduct(coordinate)).All(value => DoubleComparison.Milli.Compare(value, 0D) >= 0), Format(coordinate, frustum));
      }
    }

    private static String Format(Vector vector, params Vector[] frustum)
    {
      var builder = new StringBuilder(Environment.NewLine);

      for (Int32 index = 0, count = Math.Min(frustum.Length / 2, Vector3D.Size) * 2; index < count; index += 2)
      {
        var dimension = Convert.ToChar('X' + index / 2);
        var minimum = frustum[index + 0].DotProduct(vector);
        var maximum = frustum[index + 1].DotProduct(vector);

        builder.AppendFormat("Min{0}: {1:E} Max{0}: {2:E} = {3}\n", dimension, minimum, maximum, minimum >= 0D && maximum >= 0D);
      }

      return builder.ToString();
    }
  }
}
