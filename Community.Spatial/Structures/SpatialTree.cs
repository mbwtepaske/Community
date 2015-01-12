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

    public SpatialTree(Domain domain)
    {
      if (domain == null)
      {
        throw new ArgumentNullException("domain");
      }

      Dimensions = domain.Center.Count;
      Root = CreateNodeInternal(null, domain);
    }

    internal SpatialTreeNode<TValue> CreateNodeInternal(SpatialTreeNode<TValue> parent, Domain domain)
    {
      var node = CreateNode(parent, domain);

      if (parent != null)
      {
        parent.NodeList.Add(node);
      }

      return node;
    }

    protected virtual SpatialTreeNode<TValue> CreateNode(SpatialTreeNode<TValue> parent, Domain domain)
    {
      return new SpatialTreeNode<TValue>(this, parent, domain);
    }
  }
}