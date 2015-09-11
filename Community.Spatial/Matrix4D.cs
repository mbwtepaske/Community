namespace System.Spatial
{
  using Linq;

  /// <summary>
  /// Represents a 4 x 4 matrix.
  /// </summary>
  public static class Matrix4D
  {
    public const Int32 Order = 4;

    public static Matrix Create(Func<Int32, Int32, Double> valueFactory)
    {
      return Matrix.Build.Dense(Order, Order, valueFactory);
    }

    public static Plane[] Frustum(Matrix matrix)
    {
      if (matrix == null)
      {
        throw new ArgumentNullException(nameof(matrix));
      }

      if (matrix.ColumnCount != Order || matrix.RowCount != Order)
      {
        throw new ArgumentException("matrix is not a 4 x 4 matrix");
      }

      var columns = Enumerable.Range(0, Order).Select(matrix.GetColumn).ToArray();
      
      var minimumX = new Plane((columns[3] + columns[0]).ToArray()).Normalize();  // Left-plane
      var maximumX = new Plane((columns[3] - columns[0]).ToArray()).Normalize();  // Right-plane
      var minimumY = new Plane((columns[3] + columns[1]).ToArray()).Normalize();  // Bottom-plane
      var maximumY = new Plane((columns[3] - columns[1]).ToArray()).Normalize();  // Top-plane
      var minimumZ = new Plane(columns[2].ToArray()).Normalize();                 // Near-plane
      var maximumZ = new Plane((columns[3] - columns[2]).ToArray()).Normalize();  // Far-plane

      return new[]
      { 
        minimumX, maximumX, 
        minimumY, maximumY, 
        minimumZ, maximumZ 
      };
    }

    public static Double Determinant(Matrix matrix)
    {
      if (matrix == null)
      {
        throw new ArgumentNullException(nameof(matrix));
      }

      if (matrix.ColumnCount != Order || matrix.RowCount != Order)
      {
        throw new DimensionMismatchException();
      }

      var num1 = (matrix[2, 2] * matrix[3, 3] - matrix[2, 3] * matrix[3, 2]);
      var num2 = (matrix[2, 1] * matrix[3, 3] - matrix[2, 3] * matrix[3, 1]);
      var num3 = (matrix[2, 1] * matrix[3, 2] - matrix[2, 2] * matrix[3, 1]);
      var num4 = (matrix[2, 0] * matrix[3, 3] - matrix[2, 3] * matrix[3, 0]);
      var num5 = (matrix[2, 0] * matrix[3, 2] - matrix[2, 2] * matrix[3, 0]);
      var num6 = (matrix[2, 0] * matrix[3, 1] - matrix[2, 1] * matrix[3, 0]);

      return  matrix[0, 0] * (matrix[1, 1] * num1 - matrix[1, 2] * num2 + matrix[1, 3] * num3) 
        -     matrix[0, 1] * (matrix[1, 0] * num1 - matrix[1, 2] * num4 + matrix[1, 3] * num5) 
        +     matrix[0, 2] * (matrix[1, 0] * num2 - matrix[1, 1] * num4 + matrix[1, 3] * num6) 
        -     matrix[0, 3] * (matrix[1, 0] * num3 - matrix[1, 1] * num5 + matrix[1, 2] * num6);
    }

    /// <summary>
    /// Creates a 4 x 4 identity-matrix.
    /// </summary>
    public static Matrix Identity()
    {
      return Matrix.Build.DenseIdentity(Order);
    }

    public static Matrix Inverse(Matrix matrix)
    {
      if (matrix == null)
      {
        throw new ArgumentNullException(nameof(matrix));
      }

      if (matrix.ColumnCount != Order || matrix.RowCount != Order)
      {
        throw new DimensionMismatchException();
      }

      var num1 = (matrix[2, 0] * matrix[3, 1] - matrix[2, 1] * matrix[3, 0]);
      var num2 = (matrix[2, 0] * matrix[3, 2] - matrix[2, 2] * matrix[3, 0]);
      var num3 = (matrix[2, 3] * matrix[3, 0] - matrix[2, 0] * matrix[3, 3]);
      var num4 = (matrix[2, 1] * matrix[3, 2] - matrix[2, 2] * matrix[3, 1]);
      var num5 = (matrix[2, 3] * matrix[3, 1] - matrix[2, 1] * matrix[3, 3]);
      var num6 = (matrix[2, 2] * matrix[3, 3] - matrix[2, 3] * matrix[3, 2]);
      var num7 = (matrix[1, 1] * num6 + matrix[1, 2] * num5 + matrix[1, 3] * num4);
      var num8 = (matrix[1, 0] * num6 + matrix[1, 2] * num3 + matrix[1, 3] * num2);
      var num9 = (matrix[1, 0] * -num5 + matrix[1, 1] * num3 + matrix[1, 3] * num1);
      var num10 = (matrix[1, 0] * num4 + matrix[1, 1] * -num2 + matrix[1, 2] * num1);
      var num11 = (matrix[0, 0] * num7 - matrix[0, 1] * num8 + matrix[0, 2] * num9 - matrix[0, 3] * num10);
      
      if (Math.Abs(num11).CompareTo(0D) == 0)
      {
        return Matrix.Build.Dense(Order, Order);
      }

      var result = Matrix.Build.SameAs(matrix);

      var num12 = 1D / num11;
      var num13 = (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]);
      var num14 = (matrix[0, 0] * matrix[1, 2] - matrix[0, 2] * matrix[1, 0]);
      var num15 = (matrix[0, 3] * matrix[1, 0] - matrix[0, 0] * matrix[1, 3]);
      var num16 = (matrix[0, 1] * matrix[1, 2] - matrix[0, 2] * matrix[1, 1]);
      var num17 = (matrix[0, 3] * matrix[1, 1] - matrix[0, 1] * matrix[1, 3]);
      var num18 = (matrix[0, 2] * matrix[1, 3] - matrix[0, 3] * matrix[1, 2]);
      var num19 = (matrix[0, 1] * num6 + matrix[0, 2] * num5 + matrix[0, 3] * num4);
      var num20 = (matrix[0, 0] * num6 + matrix[0, 2] * num3 + matrix[0, 3] * num2);
      var num21 = (matrix[0, 0] * -num5 + matrix[0, 1] * num3 + matrix[0, 3] * num1);
      var num22 = (matrix[0, 0] * num4 + matrix[0, 1] * -num2 + matrix[0, 2] * num1);
      var num23 = (matrix[3, 1] * num18 + matrix[3, 2] * num17 + matrix[3, 3] * num16);
      var num24 = (matrix[3, 0] * num18 + matrix[3, 2] * num15 + matrix[3, 3] * num14);
      var num25 = (matrix[3, 0] * -num17 + matrix[3, 1] * num15 + matrix[3, 3] * num13);
      var num26 = (matrix[3, 0] * num16 + matrix[3, 1] * -num14 + matrix[3, 2] * num13);
      var num27 = (matrix[2, 1] * num18 + matrix[2, 2] * num17 + matrix[2, 3] * num16);
      var num28 = (matrix[2, 0] * num18 + matrix[2, 2] * num15 + matrix[2, 3] * num14);
      var num29 = (matrix[2, 0] * -num17 + matrix[2, 1] * num15 + matrix[2, 3] * num13);
      var num30 = (matrix[2, 0] * num16 + matrix[2, 1] * -num14 + matrix[2, 2] * num13);

      result[0, 0] = num7 * num12;
      result[0, 1] = -num19 * num12;
      result[0, 2] = num23 * num12;
      result[0, 3] = -num27 * num12;
      result[1, 0] = -num8 * num12;
      result[1, 1] = num20 * num12;
      result[1, 2] = -num24 * num12;
      result[1, 3] = num28 * num12;
      result[2, 0] = num9 * num12;
      result[2, 1] = -num21 * num12;
      result[2, 2] = num25 * num12;
      result[2, 3] = -num29 * num12;
      result[3, 0] = -num10 * num12;
      result[3, 1] = num22 * num12;
      result[3, 2] = -num26 * num12;
      result[3, 3] = num30 * num12;

      return result;
    }

    public static Matrix PerspectiveFieldOfView(Double fieldOfView, Double aspectRatio, Double frontPlaneDistance, Double backPlaneDistance)
    {
      if (fieldOfView <= 0D || fieldOfView >= Math.PI)
      {
        throw new ArgumentOutOfRangeException(nameof(fieldOfView));
      }

      if (frontPlaneDistance <= 0D)
      {
        throw new ArgumentOutOfRangeException(nameof(frontPlaneDistance));
      }

      if (backPlaneDistance <= 0D)
      {
        throw new ArgumentOutOfRangeException(nameof(backPlaneDistance));
      }

      if (frontPlaneDistance >= backPlaneDistance)
      {
        throw new ArgumentOutOfRangeException(nameof(frontPlaneDistance));
      }

      var scaleY = 1D / Math.Tan(fieldOfView);

      var result = Matrix.Build.Dense(Order, Order);

      result[0, 0] = scaleY / aspectRatio;
      result[1, 1] = scaleY;
      result[2, 2] = backPlaneDistance / (backPlaneDistance - frontPlaneDistance);
      result[2, 3] = 1D;
      result[3, 2] = -frontPlaneDistance * backPlaneDistance / (backPlaneDistance - frontPlaneDistance);

      return result;
    }

    /// <summary>
    /// Creates a 4 x 4 left-handed projection matrix.
    /// </summary>
    public static Matrix Perspective(Double frontPlaneWidth, Double frontPlaneHeight, Double frontPlaneDistance, Double backPlaneDistance)
    {
      if (frontPlaneDistance <= 0D)
      {
        throw new ArgumentOutOfRangeException(nameof(frontPlaneDistance));
      }

      if (backPlaneDistance <= 0D)
      {
        throw new ArgumentOutOfRangeException(nameof(backPlaneDistance));
      }

      if (frontPlaneDistance >= backPlaneDistance)
      {
        throw new ArgumentOutOfRangeException(nameof(frontPlaneDistance));
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
