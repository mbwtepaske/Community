using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  /// <summary>
  /// Defines a serie of collision detection methods.
  /// </summary>
  public interface ICollidable
  {
    /// <summary>
    /// Determines the collision-type the specified point is contained by the collidable.
    /// </summary>
    CollisionType Test(Vector point);
  }

  /// <summary>
  /// Defines a method of collision detection methods.
  /// </summary>
  public interface ICollidable<in TCollidable> : ICollidable where TCollidable : ICollidable
  {
    /// <summary>
    /// Determine whether the collidable contains the other collidable.
    /// </summary>
    CollisionType Test(TCollidable other);
  }
}