namespace System.Spatial
{
  public static class Matrix2D
  {
    public const Int32 Order = 2;

    /// <summary>
    /// A 2 x 2 identity-matrix.
    /// </summary>
    public static readonly Matrix Identity = new Matrix(Order, Order, (columnIndex, rowIndex) => columnIndex == rowIndex ? 1D : 0D, true);
  }
}