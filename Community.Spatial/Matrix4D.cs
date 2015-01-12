using MathNet.Numerics.LinearAlgebra;

namespace System.Spatial
{
  /// <summary>
  /// Represents a 4 x 4 matrix.
  /// </summary>
  public partial class Matrix4D : Matrix
  {
    public const Int32 Order = 4;

    //public class Projection : Matrix4D
    //{
    //  public class Isometric : Projection
    //  {
    //    /// <summary>
    //    /// Initialize a isometric projection matrix.
    //    /// </summary>
    //    /// <param name="frontPlane">Distance from origin to front plane along the z-axis.</param>
    //    /// <param name="backPlane">Distance from origin to back plane along the z-axis.</param>
    //    /// <param name="viewWidth">Width of the view volume.</param>
    //    /// <param name="viewHeight">Height of the view volume.</param>
    //    public Isometric(Double frontPlane, Double backPlane, Double viewWidth, Double viewHeight)
    //      : base(frontPlane, backPlane, viewWidth, viewHeight)
    //    {
    //      At(0, 0, 2D / viewWidth);
    //      At(1, 1, 2D / viewHeight);
    //      At(2, 2, 1D / (backPlane - frontPlane));
    //      At(3, 2, -frontPlane / (backPlane - frontPlane));
    //      At(3, 3, 1D);
    //    }
    //  }

    //  public class Perspective : Projection
    //  {
    //    /// <summary>
    //    /// Initialize a perspective projection matrix.
    //    /// </summary>
    //    /// <param name="frontPlane">Distance from origin to near plane along the z-axis.</param>
    //    /// <param name="backPlane">Distance from origin to far plane along the z-axis.</param>
    //    /// <param name="viewWidth">Width of the view volume.</param>
    //    /// <param name="viewHeight">Height of the view volume.</param>
    //    public Perspective(Double frontPlane, Double backPlane, Double viewWidth, Double viewHeight)
    //      : base(frontPlane, backPlane, viewWidth, viewHeight)
    //    {
    //      At(0, 0, 2D * frontPlane / viewWidth);
    //      At(1, 1, 2D * frontPlane / viewHeight);
    //      At(2, 2, backPlane / (backPlane - frontPlane));
    //      At(2, 3, 1D);
    //      At(3, 2, frontPlane * backPlane / (frontPlane - backPlane));
    //    }
    //  }

    //  public Double BackPlane
    //  {
    //    get;
    //    private set;
    //  }

    //  public Double FrontPlane
    //  {
    //    get;
    //    private set;
    //  }

    //  public Double ViewWidth
    //  {
    //    get;
    //    private set;
    //  }

    //  public Double ViewHeight
    //  {
    //    get;
    //    private set;
    //  }

    //  /// <summary>
    //  /// Initialize a perspective projection matrix.
    //  /// </summary>
    //  protected Projection(Double frontPlane, Double backPlane, Double viewWidth, Double viewHeight)
    //  {
    //    if (backPlane < frontPlane)
    //    {
    //      throw new ArgumentException("farPlane < nearPlane");
    //    }

    //    if (viewWidth <= 0D)
    //    {
    //      throw new ArgumentException("viewWidth must be greater than zero");
    //    }

    //    if (viewHeight <= 0D)
    //    {
    //      throw new ArgumentException("viewHeight must be greater than zero");
    //    }

    //    FrontPlane = frontPlane;
    //    BackPlane = backPlane;
    //    ViewWidth = viewWidth;
    //    ViewHeight = viewHeight;
    //  }
    //}

    //public class Rotation : Matrix4D
    //{
    //  public Rotation(Quaternion quaternion)
    //  {
    //  }
    //}

    public Matrix4D(params Double[] values)
      : base(Order, Order, values)
    {
    }

    public Matrix4D(Matrix<Double> matrix)
      : base(matrix)
    {
      if (ColumnCount != Order || RowCount != Order)
      {
        throw new ArgumentException("Column- and row count must equal to " + Order);
      }
    }
  }
}
