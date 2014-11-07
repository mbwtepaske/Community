namespace System.Spatial
{
  /// <summary>
  /// Represents a 4 x 4 matrix.
  /// </summary>
  public class Matrix4D : Matrix
  {
    public Matrix4D(params Double[] values) 
      : base(4, 4, values)
    {
    }

    public Matrix4D(Matrix matrix)
      : base(4, 4, matrix)
    {
    }
  }
}