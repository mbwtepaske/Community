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
      return new Matrix(Order, Order, (columnIndex, rowIndex) => columnIndex == rowIndex ? 1D : 0D);
    }
  }
}