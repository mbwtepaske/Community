using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;

namespace System.Spatial
{
  public static class Matrix2D
  {
    public const Int32 Order = 2;

    /// <summary>
    /// Creates a 2 x 2 identity-matrix.
    /// </summary>
    public static Matrix Identity()
    {
      return Matrix.Build.DenseIdentity(Order);
    }
  }
}