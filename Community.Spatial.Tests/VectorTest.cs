using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Spatial.Tests
{
  [TestClass]
  public class VectorTest
  {
    private const string Category = "Vector";

    [TestCategory(Category)]
    [TestMethod]
    public void MultiplyWithMatrix()
    {
      var result = Vector4D.UnitX * Matrix4D.Rotate(Vector3D.UnitZ, Math.PI / 2D);

      Assert.IsTrue(result.Equals(Vector4D.UnitY, DoubleComparison.Pico));
    }

    [TestCategory(Category)]
    [TestMethod]
    public void TransformCoordinate()
    {
      var result = Vector3D.TransformCoordinate(Vector3D.UnitX, Matrix4D.Rotate(Vector3D.UnitZ, Math.PI / 4D));
      var normal = Vector3D.Create(1D, 1D, 0D).Normalize();

      Assert.IsTrue(result.Zip(normal, (left, right) => Math.Abs(left - right)).All(value => DoubleComparison.Nano.Equals(value, 0D)));
    }
  }
}