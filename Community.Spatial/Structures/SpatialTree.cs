namespace System.Spatial
{
  using Collections.Generic;
  using Linq;

  public class SpatialTree<TValue>
  {
    public Int32 Dimensions
    {
      get;
      private set;
    }

    public SpatialTreeNode<TValue> Root
    {
      get;
      private set;
    }

    public SpatialTree(Int32 dimensions, Vector minimum, Vector maximum)
    {
      if (dimensions < 1)
      {
        throw new ArgumentException("dimensions must be greater than zero");
      }

      if (minimum == null)
      {
        throw new ArgumentNullException("minimum");
      }

      if (maximum == null)
      {
        throw new ArgumentNullException("maximum");
      }

      Dimensions = dimensions;
      Root = CreateNodeInternal(null, minimum, maximum);
    }

    internal SpatialTreeNode<TValue> CreateNodeInternal(SpatialTreeNode<TValue> parent, Vector minimum, Vector maximum)
    {
      return CreateNode(parent, minimum, maximum);
    }

    protected virtual SpatialTreeNode<TValue> CreateNode(SpatialTreeNode<TValue> parent, Vector minimum, Vector maximum)
    {
      var node = new SpatialTreeNode<TValue>(this, parent, minimum, maximum);

      return parent.Nodes.Add(node) ? node : null;
    }
  }
}
