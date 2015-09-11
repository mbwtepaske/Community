namespace System.Collections
{
  using Generic;
  using Linq;

  public sealed class TreeNode // : IList<TreeNode>
  {
    internal readonly TreeNodeCollection Nodes;

    public TreeNode Parent
    {
      get;
    }

    public TreeNode Root
    {
      get;
    }

    public Tree Tree
    {
      get;
    }

    internal TreeNode(Tree tree, TreeNode parent = null)
    {
      Nodes = new TreeNodeCollection(this);
      Parent = parent;
      Root = parent?.Root;
      Tree = tree;
    }

    /// <summary>
    /// Enumerates the ancestry of this node, towards the root-node.
    /// </summary>
    public IEnumerable<TreeNode> Ancestors(Boolean includeSelf = false)
    {
      for (var current = includeSelf ? Parent : this; current != Parent; current = current.Parent)
      {
        yield return current;
      }
    }

    /// <summary>
    /// Enumerates the children of this node.
    /// </summary>
    public IEnumerable<TreeNode> Children() => Nodes.AsEnumerable();

    /// <summary>
    /// Enumerates the siblings of this node.
    /// </summary>
    public IEnumerable<TreeNode> Siblings() => Parent?.Nodes.Where(node => !ReferenceEquals(node, this));
  }
}