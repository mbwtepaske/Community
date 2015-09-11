using System.Collections.ObjectModel;

namespace System.Collections
{
  public class TreeNodeCollection : Collection<TreeNode>
  {
    internal readonly TreeNode Owner;

    internal TreeNodeCollection(TreeNode owner)
    {
      Owner = owner;
    }
  }
}