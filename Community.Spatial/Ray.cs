namespace System.Spatial
{
  public class Ray
  {
    public readonly Vector Position;
    public readonly Vector Direction;

    public Ray(Vector position, Vector direction)
    {
      if (position == null)
      {
        throw new ArgumentNullException("position");
      }

      if (direction == null)
      {
        throw new ArgumentNullException("direction");
      }

      if (position.Count != direction.Count)
      {
        throw new ArgumentDimensionMismatchException("direction", position.Count);
      }

      Position = position;
      Direction = direction;
    }

    public override String ToString()
    {
      return String.Format("Position: {0}, Direction: {1}", Position.ToString("N6"), Direction.ToString("N6"));
    }

    public static Ray FromPositionAndTarget(Vector position, Vector target)
    {
      return new Ray(position, target.Subtract(position).Normalize());
    }
  }
}
