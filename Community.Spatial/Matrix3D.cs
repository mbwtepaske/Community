namespace System.Spatial
{
  /// <summary>
  /// Represents a 3 x 3 matrix.
  /// </summary>
  public static class Matrix3D
  {
    public const Int32 Order = 3;

    /// <summary>
    /// A 3 x 3 identity-matrix.
    /// </summary>
    public static readonly Matrix Identity = new Matrix(Order, Order, (columnIndex, rowIndex) => columnIndex == rowIndex ? 1D : 0D, true);
  }
}