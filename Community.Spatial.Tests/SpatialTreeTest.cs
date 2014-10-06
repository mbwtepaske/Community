using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Spatial.Tests
{
  using Collections.Generic;
  using Linq;

  [TestClass]
  public class SpatialTreeTest
  {
    public static readonly Double[] TestDivisions =
    {
      0.5D, 
      0.75D
    };

    public const Int32 TestLevelCount = 3;

    [TestMethod]
    public void SplitTest()
    {
      for (var dimension = 1; dimension <= 4; dimension++)
      {
        var spatialTree = new SpatialTree<Object>(dimension, new Vector(dimension, -1D), new Vector(dimension, 1D));

        spatialTree.Root.Split(node => node.Level < TestLevelCount, TestDivisions);

        var current = spatialTree.Root;

        for (var level = 0; level < TestLevelCount; level++)
        {
          Assert.AreEqual(current.Nodes.Count, (Int32)Math.Pow(TestDivisions.Length + 1, dimension));

          current = current.Nodes.ElementAt(0);
        }
      }
    }

    [TestMethod]
    public void TraverseTest()
    {
      for (var dimension = 1; dimension <= 4; dimension++)
      {
        var spatialTree = new SpatialTree<Object>(dimension, new Vector(dimension, 1D), new Vector(dimension, 2D));

        spatialTree.Root.Split(0.25D, 0.80D);

        Assert.AreEqual(spatialTree.Root.Nodes.Count, (Int32)Math.Pow(3, dimension));

        var firstChildNode = spatialTree.Root.Nodes.ElementAt(0);

        Assert.IsNotNull(firstChildNode);

        firstChildNode.Split(0.5D);
      }
    }
  }
}
