using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  /// <summary>
  /// Represents a volume in cartesian space, exposing collision testing.
  /// </summary>
  public abstract class Volume : ICollidable
  {
    public readonly Int32 Dimensions;

    protected Volume(Int32 dimensions)
    {
      Dimensions = dimensions;
    }

    public abstract CollisionType Test(Vector point);
  }
}