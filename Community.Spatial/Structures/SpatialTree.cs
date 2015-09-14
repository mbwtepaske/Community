namespace System.Spatial
{
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

    public SpatialTree(Box box)
    {
      if (box == null)
      {
        throw new ArgumentNullException(nameof(box));
      }

      Dimensions = box.Center.Count;
      Root = CreateNodeInternal(null, box);
    }

    internal SpatialTreeNode<TValue> CreateNodeInternal(SpatialTreeNode<TValue> parent, Box box)
    {
      var node = CreateNode(parent, box);

      if (parent != null)
      {
        parent.NodeList.Add(node);
      }

      return node;
    }

    protected virtual SpatialTreeNode<TValue> CreateNode(SpatialTreeNode<TValue> parent, Box box)
    {
      return new SpatialTreeNode<TValue>(this, parent, box);
    }
  }
}