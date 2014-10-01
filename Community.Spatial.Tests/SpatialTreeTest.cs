using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Spatial.Tests
{
  using Collections.Generic;
  using Linq;

  [TestClass]
  public class SpatialTreeTest
  {
    [TestMethod]
    public void SplitTest()
    {
      for (var dimension = 1; dimension <= 4; dimension++)
      {
        var spatialTree = new SpatialTree<Object>(dimension, new Vector(dimension, 1D), new Vector(dimension, 2D));

        spatialTree.Root.Split(0.25D, 0.80D);


      }
    }
  }
}
