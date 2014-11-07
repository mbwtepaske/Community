namespace System.Spatial
{
  /// <summary>
  /// Represents a 3 x 3 matrix.
  /// </summary>
  public class Matrix3D : Matrix
  {
    public Matrix3D(params Double[] values)
      : base(3, 3, values)
    {
    }

    public Matrix3D(Matrix matrix)
      : base(3, 3, matrix)
    {
    }
  }
}