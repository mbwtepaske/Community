using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;

namespace System.Spatial
{
  /// <summary>
  /// Represents a 3 x 3 matrix.
  /// </summary>
  public static class Matrix3D
  {
    public const Int32 Order = 3;

    /// <summary>
    /// Creates a 3 x 3 identity-matrix.
    /// </summary>
    public static Matrix Identity()
    {
      return Matrix.Build.DenseIdentity(Order);
    }
  }
}