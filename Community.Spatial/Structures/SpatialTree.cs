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

      Dimensions = domain.Center.Size;
      Root = CreateNodeInternal(null, domain);
    }

    //public SpatialTree(Int32 dimensions, Vector minimum, Vector maximum)
    //{
    //  if (dimensions < 1)
    //  {
    //    throw new ArgumentException("dimensions must be greater than zero");
    //  }

    //  if (minimum == null)
    //  {
    //    throw new ArgumentNullException("minimum");
    //  }

    //  if (maximum == null)
    //  {
    //    throw new ArgumentNullException("maximum");
    //  }

    //  Dimensions = dimensions;
    //  Root = CreateNodeInternal(null, minimum, maximum);
    //}

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