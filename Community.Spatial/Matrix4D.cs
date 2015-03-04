using MathNet.Numerics.LinearAlgebra;

using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  using Linq;

  /// <summary>
  /// Represents a 4 x 4 matrix.
  /// </summary>
  public static class Matrix4D
  {
    public const Int32 Order = 4;

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

      var columns = Enumerable.Range(0, Order).Select(matrix.Column).ToArray();
      
      var minimumX = Vector.Build.DenseOfVector((columns[3] + columns[0])).Normalize();
      var maximumX = Vector.Build.DenseOfVector((columns[3] - columns[0])).Normalize();
      var minimumY = Vector.Build.DenseOfVector((columns[3] + columns[1])).Normalize();
      var maximumY = Vector.Build.DenseOfVector((columns[3] - columns[1])).Normalize();
      var minimumZ = Vector.Build.DenseOfVector(columns[2]).Normalize();
      var maximumZ = Vector.Build.DenseOfVector((columns[3] - columns[2])).Normalize();

      return new[]
      { 
        minimumX, maximumX, 
        minimumY, maximumY, 
        minimumZ, maximumZ 
      };
    }

    /// <summary>
    /// Creates a 4 x 4 identity-matrix.
    /// </summary>
    public static Matrix Identity()
    {
      return Matrix.Build.DenseIdentity(Order);
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

      var result = Matrix.Build.Dense(Order, Order);
      
      result[0, 0] = 2D * frontPlaneDistance / frontPlaneWidth;
      result[1, 1] = 2D * frontPlaneDistance / frontPlaneHeight;
      result[2, 2] = backPlaneDistance / (backPlaneDistance - frontPlaneDistance);
      result[2, 3] = 1D;
      result[3, 2] = frontPlaneDistance * backPlaneDistance / (frontPlaneDistance - backPlaneDistance);

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

      var result = Identity();

      result.At(0, 0, xx + cos * (1D - xx));
      result.At(0, 1, xy - cos * xy + sin * z);
      result.At(0, 2, xz - cos * xz - sin * y);

      result.At(1, 0, xy - cos * xy - sin * z);
      result.At(1, 1, yy + cos * (1D - yy));
      result.At(1, 2, yz - cos * yz + sin * x);

      result.At(2, 0, xz - cos * xz + sin * y);
      result.At(2, 1, yz - cos * yz - sin * x);
      result.At(2, 2, zz + cos * (1D - zz));

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

      var result = Identity();

      result.At(0, 0, 1D - 2D * (yy + zz));
      result.At(0, 1, 2D * (xy + zw));
      result.At(0, 2, 2D * (zx - yw));
      result.At(1, 0, 2D * (xy - zw));
      result.At(1, 1, 1D - 2D * (zz + xx));
      result.At(1, 2, 2D * (yz + xw));
      result.At(2, 0, 2D * (zx + yw));
      result.At(2, 1, 2D * (yz - xw));
      result.At(2, 2, 1D - 2D * (yy + xx));

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
      Assert.ThrowIfNull(scale, "scale");
      Assert.ThrowArgument(scale, s => s.Count != Vector3D.Size);

      var scaleX = scale[0];
      var scaleY = scale[1];
      var scaleZ = scale[2];

      var result = Identity();

      result.At(0, 0, scaleX);
      result.At(1, 1, scaleY);
      result.At(2, 2, scaleZ);

      return result;
    }

    /// <summary>
    /// Create a 4x4 translation matrix, using a vector as offset.
    /// </summary>
    public static Matrix Translate(Vector translation)
    {
      var result = Identity();

      for (var index = 0; index < translation.Count; index++)
      {
        result[3, index] = translation[index];
      }

      return result;
    }

    public static Matrix View(Vector position, Vector forward, Vector upward)
    {
      var axisZ = forward.Normalize();
      var axisX = Vector3D.Cross(upward, axisZ).Normalize();
      var axisY = Vector3D.Cross(axisZ, axisX);

      var result = Identity();

      result[0, 0] = axisX[0];
      result[0, 1] = axisY[0];
      result[0, 2] = axisZ[0];
      result[1, 0] = axisX[1];
      result[1, 1] = axisY[1];
      result[1, 2] = axisZ[1];
      result[2, 0] = axisX[2];
      result[2, 1] = axisY[2];
      result[2, 2] = axisZ[2];
      result[3, 0] = -axisX.DotProduct(position);
      result[3, 1] = -axisY.DotProduct(position);
      result[3, 2] = -axisZ.DotProduct(position);

      return result;
    }
  }
}
