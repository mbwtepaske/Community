using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Spatial.Tests
{
  [TestClass]
  public class MatrixTest
  {
    [TestMethod]
    public void MultiplicationTest()
    {
      var left = new Matrix(new Double[,]
      {
        { 1D, 2D },
        { 4D, 3D }
      });

      var right = new Matrix(new Double[,]
      {
        { 1D, 2D, 3D },
        { 3D, -4D, 7D }
      });

      var result = Matrix.Multiply(left, right);

      Assert.IsTrue(result.SequenceEqual(new[] { 7D, -6D, 17D, 13D, -4D, 33D }));
    }
  }
}
