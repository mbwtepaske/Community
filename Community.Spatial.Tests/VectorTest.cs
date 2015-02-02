using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial.Tests
{
  using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

  [TestClass]
  public class VectorTest
  {
    [TestMethod]
    public void TransformCoordinateTest()
    {
      var transform = Matrix4D.Rotate(Vector3D.UnitY, Math.PI / 4D) * Matrix4D.Rotate(Vector3D.UnitZ, Math.PI);
      var result = Vector3D.TransformCoordinate(Vector3D.UnitX, transform);
      var normal = Vector3D.One / Vector3D.One.L1Norm();

      Assert.Equals(result, normal);
    }
  }
}
