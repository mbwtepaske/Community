using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MathNet.Numerics.LinearAlgebra;

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
      var result = Vector3D.TransformCoordinate(Vector3D.UnitX, Matrix4D.Rotate(Vector3D.UnitZ, Math.PI / 4D));
      var normal = Vector3D.Create(1D, 1D, 0D).Normalize();

      Assert.IsTrue(result.Zip(normal, (left, right) => Math.Abs(left - right)).All(value => DoubleComparison.Nano.Equals(value, 0D)));
    }
  }
}
