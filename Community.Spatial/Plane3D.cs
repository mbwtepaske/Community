namespace System.Spatial
{
  using Linq;

  public static class Plane3D
  {
    public static Vector Intersection(Plane plane1, Plane plane2, Plane plane3)
    {
      return 
        -plane1.Distance * Vector3D.Cross(plane2.Normal, plane3.Normal) / plane1.Normal.DotProduct(Vector3D.Cross(plane2.Normal, plane3.Normal)) 
        -plane2.Distance * Vector3D.Cross(plane3.Normal, plane1.Normal) / plane2.Normal.DotProduct(Vector3D.Cross(plane3.Normal, plane1.Normal)) 
        -plane3.Distance * Vector3D.Cross(plane1.Normal, plane2.Normal) / plane3.Normal.DotProduct(Vector3D.Cross(plane1.Normal, plane2.Normal));
    }
  }
}