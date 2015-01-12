using MathNet.Numerics.LinearAlgebra;

namespace System.Spatial
{
  /// <summary>
  /// Represents a 3 x 3 matrix.
  /// </summary>
  public class Matrix3D : Matrix
  {
    public const Int32 Order = 3;

    public Matrix3D(params Double[] values)
      : base(Order, Order, values)
    {
    }

    public Matrix3D(Matrix<Double> matrix)
      : base(matrix)
    {
      if (ColumnCount != Order || RowCount != Order)
      {
        throw new ArgumentException("Column- and row count must equal to " + Order);
      }
    }
  }
}