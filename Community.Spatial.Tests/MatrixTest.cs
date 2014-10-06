using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Spatial.Tests
{
  using ComponentModel;
  using Linq;

  [TestClass]
  public class MatrixTest
  {
    [Description("Matrix Multiplication: A[3,2] * B[2,3]")]
    [TestCategory("Multiplication")]
    [TestMethod]
    public void MultiplicationTest()
    {
      var a = new Matrix(3, 2, 1D, 2D, 3D, 4D, 5D, 6D );
      var b = new Matrix(2, 3, 7D, 8D, 9D, 10D, 11D, 12D);

      Assert.IsTrue(Matrix.Multiply(a, b).SequenceEqual(new[]
      {
        58D, 64D, 
        139D, 154D 
      }));

      Assert.IsTrue(Matrix.Multiply(b, a).SequenceEqual(new[]
      {
        39D, 54D, 69D,
        49D, 68D, 87D,
        59D, 82D, 105D
      }));
    }
  }
}
