using Xunit;

namespace System.Spatial.Tests
{
  using Collections.Generic;
  using Linq;

  public class SpatialTreeTest
  {
    [Fact(DisplayName = "Spatial Tree: Split")]
    public void SplitTest()
    {
      var spatialTree = new SpatialTree<Object>(3, new Vector3(0D), new Vector3(1D));

      spatialTree.Split(spatialTree.Root, new Double[][]
      {
        new [] { 0.5D },
        new [] { 0.5D },
        new [] { 0.5D }
      });


    }
  }
}
