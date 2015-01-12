namespace System.Spatial
{
  public class Plane4D : Plane<Vector4D>
  {
    public Plane4D(Vector4D direction, Double distance = 0D)
      : base(direction, distance)
    {
    }

    public Plane4D(Vector4D point, Vector4D direction)
      : base(point, direction)
    {
    }
  }
}