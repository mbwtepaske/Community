namespace System.Spatial
{
  /// <summary>
  /// Operations and constants for <see cref="T:Vector"/>s that only contain 4 dimensions.
  /// </summary>
  public static class Vector4D
  {
    public const Int32 Size = 4;

    public static readonly Vector UnitX = Create(1D, 0D, 0D, 0D);
    public static readonly Vector UnitY = Create(0D, 1D, 0D, 0D);
    public static readonly Vector UnitZ = Create(0D, 0D, 1D, 0D);
    public static readonly Vector UnitW = Create(0D, 0D, 0D, 1D);
    public static readonly Vector Zero = Create();

    public static Vector Create(Double defaultValue = 0D)
    {
      return new Vector(Size, defaultValue);
    }

    public static Vector Create(Double x, Double y, Double z, Double w)
    {
      return new Vector(new [] { x, y, z, w });
    }
  }
}