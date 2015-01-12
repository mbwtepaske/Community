namespace System.Spatial
{
  public class Plane2D : Plane<Vector2D>
  {
    public Plane2D(Vector2D direction, Double distance = 0D)
      : base(direction, distance)
    {
    }

    public Plane2D(Vector2D point, Vector2D direction)
      : base(point, direction)
    {
    }
  }
}