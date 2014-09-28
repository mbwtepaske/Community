namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Linq;

  public partial struct Matrix : IMatrix<Matrix>
  {
    public Double M11;
    public Double M12;
    public Double M13;
    public Double M14;
    public Double M21;
    public Double M22;
    public Double M23;
    public Double M24;
    public Double M31;
    public Double M32;
    public Double M33;
    public Double M34;
    public Double M41;
    public Double M42;
    public Double M43;
    public Double M44;

    public Int32 Columns
    {
      get
      {
        return 4;
      }
    }

    public Int32 Rows
    {
      get
      {
        return 4;
      }
    }

    public Double this[Int32 index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return M11;

          case 1:
            return M12;

          case 2:
            return M13;

          case 3:
            return M14;

          case 4:
            return M21;

          case 5:
            return M22;

          case 6:
            return M23;

          case 7:
            return M24;

          case 8:
            return M31;

          case 9:
            return M32;

          case 10:
            return M33;

          case 11:
            return M34;

          case 12:
            return M41;

          case 13:
            return M42;

          case 14:
            return M43;

          case 15:
            return M44;

          default:
            throw new ArgumentOutOfRangeException("index");
        }
      }
      set
      {
        switch (index)
        {
          default:
            throw new ArgumentOutOfRangeException("index");
        }
      }
    }
   
    public Double this[Int32 column, Int32 row]
    {
      get
      {
        return this[column + Rows * Columns];
      }
      set
      {
        this[column + Rows * Columns] = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Matrix"/> struct.
    /// </summary>
    /// <param name="m11">The value to assign at row 1 column 1 of the matrix.</param>
    /// <param name="m12">The value to assign at row 1 column 2 of the matrix.</param>
    /// <param name="m13">The value to assign at row 1 column 3 of the matrix.</param>
    /// <param name="m14">The value to assign at row 1 column 4 of the matrix.</param>
    /// <param name="m21">The value to assign at row 2 column 1 of the matrix.</param>
    /// <param name="m22">The value to assign at row 2 column 2 of the matrix.</param>
    /// <param name="m23">The value to assign at row 2 column 3 of the matrix.</param>
    /// <param name="m24">The value to assign at row 2 column 4 of the matrix.</param>
    /// <param name="m31">The value to assign at row 3 column 1 of the matrix.</param>
    /// <param name="m32">The value to assign at row 3 column 2 of the matrix.</param>
    /// <param name="m33">The value to assign at row 3 column 3 of the matrix.</param>
    /// <param name="m34">The value to assign at row 3 column 4 of the matrix.</param>
    /// <param name="m41">The value to assign at row 4 column 1 of the matrix.</param>
    /// <param name="m42">The value to assign at row 4 column 2 of the matrix.</param>
    /// <param name="m43">The value to assign at row 4 column 3 of the matrix.</param>
    /// <param name="m44">The value to assign at row 4 column 4 of the matrix.</param>
    public Matrix(
      Double m11 = 0D, Double m12 = 0D, Double m13 = 0D, Double m14 = 0D, 
      Double m21 = 0D, Double m22 = 0D, Double m23 = 0D, Double m24 = 0D, 
      Double m31 = 0D, Double m32 = 0D, Double m33 = 0D, Double m34 = 0D, 
      Double m41 = 0D, Double m42 = 0D, Double m43 = 0D, Double m44 = 0D)
    {
      M11 = m11;  M12 = m12;  M13 = m13;  M14 = m14;
      M21 = m21;  M22 = m22;  M23 = m23;  M24 = m24;
      M31 = m31;  M32 = m32;  M33 = m33;  M34 = m34;
      M41 = m41;  M42 = m42;  M43 = m43;  M44 = m44;
    }

    public Boolean Equals(Matrix other)
    {
      return Equals(other, 0D);
    }

    public Boolean Equals(Matrix other, Double tolerance)
    {
      return Math.Abs(M11 - other.M11) <= tolerance
        && Math.Abs(M12 - other.M12) <= tolerance
        && Math.Abs(M13 - other.M13) <= tolerance
        && Math.Abs(M14 - other.M14) <= tolerance
        && Math.Abs(M21 - other.M21) <= tolerance
        && Math.Abs(M22 - other.M22) <= tolerance
        && Math.Abs(M23 - other.M23) <= tolerance
        && Math.Abs(M24 - other.M24) <= tolerance
        && Math.Abs(M31 - other.M31) <= tolerance
        && Math.Abs(M32 - other.M32) <= tolerance
        && Math.Abs(M33 - other.M33) <= tolerance
        && Math.Abs(M34 - other.M34) <= tolerance
        && Math.Abs(M41 - other.M41) <= tolerance
        && Math.Abs(M42 - other.M42) <= tolerance
        && Math.Abs(M43 - other.M43) <= tolerance
        && Math.Abs(M44 - other.M44) <= tolerance;
    }

    public IEnumerator<Double> GetEnumerator()
    {
      yield return M11;
      yield return M12;
      yield return M13;
      yield return M14;
      yield return M21;
      yield return M22;
      yield return M23;
      yield return M24;
      yield return M31;
      yield return M32;
      yield return M33;
      yield return M34;
      yield return M41;
      yield return M42;
      yield return M43;
      yield return M44;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override Int32 GetHashCode()
    {
      return 
          M11.GetHashCode() ^ M12.GetHashCode() ^ M13.GetHashCode() ^ M14.GetHashCode()
        ^ M21.GetHashCode() ^ M22.GetHashCode() ^ M23.GetHashCode() ^ M24.GetHashCode()
        ^ M31.GetHashCode() ^ M32.GetHashCode() ^ M33.GetHashCode() ^ M34.GetHashCode()
        ^ M41.GetHashCode() ^ M42.GetHashCode() ^ M43.GetHashCode() ^ M44.GetHashCode();
    }
  }
}