namespace System.Spatial
{
  public class Plane3D : Plane<Vector3D>
  {
    public Plane3D(Vector3D direction, Double distance = 0D)
      : base(direction, distance)
    {
    }

    public Plane3D(Vector3D point, Vector3D direction)
      : base(point, direction)
    {
    }
  }
}