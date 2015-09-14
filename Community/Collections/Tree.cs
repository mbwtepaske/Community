namespace System.Collections
{
  public sealed class Tree
  {
    public TreeNode Root
    {
      get;
    }

    public Tree()
    {
      Root = new TreeNode(this);
    }
  }
}
