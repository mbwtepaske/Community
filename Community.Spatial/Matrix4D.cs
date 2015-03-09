namespace System.Spatial
{
  using Linq;

  /// <summary>
  /// Represents a 4 x 4 matrix.
  /// </summary>
  public static class Matrix4D
  {
    public const Int32 Order = 4;

    /// <summary>
    /// A 4 x 4 identity-matrix.
    /// </summary>
    public static readonly Matrix Identity = new Matrix(Order, Order, (columnIndex, rowIndex) => columnIndex == rowIndex ? 1D : 0D, true);

    public static Vector[] Frustum(Matrix matrix)
    {
      if (matrix == null)
      {
        throw new ArgumentNullException("matrix");
      }

      if (matrix.ColumnCount != Order || matrix.RowCount != Order)
      {
        throw new ArgumentException("matrix is not a 4 x 4 matrix");
      }

      var columns = Enumerable.Range(0, Order).Select(matrix.GetColumn).ToArray();
      
      var minimumX = new Vector((columns[3] + columns[0])).Normalize();
      var maximumX = new Vector((columns[3] - columns[0])).Normalize();
      var minimumY = new Vector((columns[3] + columns[1])).Normalize();
      var maximumY = new Vector((columns[3] - columns[1])).Normalize();
      var minimumZ = new Vector(columns[2]).Normalize();
      var maximumZ = new Vector((columns[3] - columns[2])).Normalize();

      return new[]
      { 
        minimumX, maximumX, 
        minimumY, maximumY, 
        minimumZ, maximumZ 
      };
    }

    /// <summary>
    /// Creates a 4 x 4 left-handed projection matrix.
    /// </summary>
    public static Matrix Perspective(Double frontPlaneWidth, Double frontPlaneHeight, Double frontPlaneDistance, Double backPlaneDistance)
    {
      if (frontPlaneDistance <= 0D)
      {
        throw new ArgumentOutOfRangeException("frontPlaneDistance");
      }

      if (backPlaneDistance <= 0D)
      {
        throw new ArgumentOutOfRangeException("backPlaneDistance");
      }

      if (frontPlaneDistance >= backPlaneDistance)
      {
        throw new ArgumentOutOfRangeException("frontPlaneDistance");
      }

      var result = new Matrix(Order, Order);
      
      result[0, 0] = 2D * frontPlaneDistance / frontPlaneWidth;
      result[1, 1] = 2D * frontPlaneDistance / frontPlaneHeight;
      result[2, 2] = backPlaneDistance / (backPlaneDistance - frontPlaneDistance);
      result[2, 3] = frontPlaneDistance * backPlaneDistance / (frontPlaneDistance - backPlaneDistance);
      result[3, 2] = 1D;

      return result;
    }

    /// <summary>
    /// Create a 4x4 rotation matrix, using a rotation-axis and an angle.
    /// </summary>
    public static Matrix Rotate(Vector axis, Double angle)
    {
      var sin = Math.Sin(angle);
      var cos = Math.Cos(angle);
      var x = axis[0];
      var y = axis[1];
      var z = axis[2];
      var xx = x * x;
      var yy = y * y;
      var zz = z * z;
      var xy = x * y;
      var xz = x * z;
      var yz = y * z;

      var result = Identity.Clone();

      result[0, 0] = xx + cos * (1D - xx);
      result[1, 0] = xy - cos * xy + sin * z;
      result[2, 0] = xz - cos * xz - sin * y;

      result[0, 1] = xy - cos * xy - sin * z;
      result[1, 1] = yy + cos * (1D - yy);
      result[2, 1] = yz - cos * yz + sin * x;

      result[0, 2] = xz - cos * xz + sin * y;
      result[1, 2] = yz - cos * yz - sin * x;
      result[2, 2] = zz + cos * (1D - zz);

      return result;
    }

    /// <summary>
    /// Create a 4x4 rotation matrix, using a quaternion.
    /// </summary>
    public static Matrix Rotate(Vector quaternion)
    {
      var xx = quaternion[0] * quaternion[0];
      var xy = quaternion[0] * quaternion[1];
      var xw = quaternion[0] * quaternion[3];
      var yy = quaternion[1] * quaternion[1];
      var yz = quaternion[1] * quaternion[2];
      var yw = quaternion[1] * quaternion[3];
      var zx = quaternion[2] * quaternion[0];
      var zz = quaternion[2] * quaternion[2];
      var zw = quaternion[2] * quaternion[3];

      var result = Identity.Clone();

      result[0, 0] = 1D - 2D * (yy + zz);
      result[1, 0] = 2D * (xy + zw);
      result[2, 0] = 2D * (zx - yw);
      result[0, 1] = 2D * (xy - zw);
      result[1, 1] = 1D - 2D * (zz + xx);
      result[2, 1] = 2D * (yz + xw);
      result[0, 2] = 2D * (zx + yw);
      result[1, 2] = 2D * (yz - xw);
      result[2, 2] = 1D - 2D * (yy + xx);

      return result;
    }

    /// <summary>
    /// Create a 4x4 scale matrix, using a value as scale-factor.
    /// </summary>
    public static Matrix Scale(Double scale)
    {
      return Scale(Vector3D.Create(scale));
    }
    
    /// <summary>
    /// Create a 4x4 scale matrix, using a vector as scale-factor.
    /// </summary>
    public static Matrix Scale(Vector scale)
    {
      if (scale == null)
      {
        throw new ArgumentNullException("scale");
      }

      if (scale.Count != Vector3D.Size)
      {
        throw new ArgumentDimensionMismatchException("scale", Vector3D.Size);
      }

      var result = Identity.Clone();

      result[0, 0] = scale[0];
      result[1, 1] = scale[1];
      result[2, 2] = scale[2];

      return result;
    }

    /// <summary>
    /// Create a 4x4 translation matrix, using a vector as offset.
    /// </summary>
    public static Matrix Translate(Vector translation)
    {
      var result = Identity.Clone();

      for (var index = 0; index < translation.Count; index++)
      {
        result[index, Vector3D.Size] = translation[index];
      }

      return result;
    }

    public static Matrix View(Vector position, Vector forward, Vector upward)
    {
      var axisZ = forward.Normalize();
      var axisX = Vector3D.Cross(upward, axisZ).Normalize();
      var axisY = Vector3D.Cross(axisZ, axisX);

      var result = Identity.Clone();

      result[0, 0] = axisX[0];
      result[1, 0] = axisY[0];
      result[2, 0] = axisZ[0];
      result[0, 1] = axisX[1];
      result[1, 1] = axisY[1];
      result[2, 1] = axisZ[1];
      result[0, 2] = axisX[2];
      result[1, 2] = axisY[2];
      result[2, 2] = axisZ[2];
      result[0, 3] = -axisX.DotProduct(position);
      result[1, 3] = -axisY.DotProduct(position);
      result[2, 3] = -axisZ.DotProduct(position);

      return result;
    }
  }
}
