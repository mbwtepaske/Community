namespace System.Spatial
{
  /// <summary>
  /// Represents a spherical volume.
  /// </summary>
  public sealed class Sphere : Volume
  {
    public readonly Vector Center;
    public readonly Vector Radii;

    public Sphere(Vector center, Double radius = 1D) 
      : this(center, center != null ? new Vector(center.Count, radius) : null)
    {
    }

    public Sphere(Vector center, Vector radii)
    {
      if (center == null)
      {
        throw new ArgumentNullException("center");
      }

      if (radii == null)
      {
        throw new ArgumentNullException("radii");
      }

      Center = center;
      Radii = radii;
    }
  }
}