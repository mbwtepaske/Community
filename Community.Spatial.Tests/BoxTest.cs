using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Spatial.Tests
{
  [TestClass]
  public class BoxTest
  {
    [TestMethod]
    public void Corner2DTest()
    {
      var boxVolume = new Box(Vector2D.Create(-1.25, -1.5), Vector2D.Create(0.75, 0.50));
      var corners = boxVolume.Corners;

      Assert.AreEqual(corners[0], Vector2D.Create(boxVolume.Minimum[0], boxVolume.Minimum[1]));
      Assert.AreEqual(corners[1], Vector2D.Create(boxVolume.Maximum[0], boxVolume.Minimum[1]));
      Assert.AreEqual(corners[2], Vector2D.Create(boxVolume.Minimum[0], boxVolume.Maximum[1]));
      Assert.AreEqual(corners[3], Vector2D.Create(boxVolume.Maximum[0], boxVolume.Maximum[1]));
    }

    [TestMethod]
    public void Corner3DTest()
    {
      var boxVolume = new Box(Vector3D.Create(-1.25, -1.5, -1.65), Vector3D.Create(0.75, 0.50, 0.25));
      var corners = boxVolume.Corners;

      Assert.AreEqual(corners[0], Vector3D.Create(boxVolume.Minimum[0], boxVolume.Minimum[1], boxVolume.Minimum[2]));
      Assert.AreEqual(corners[1], Vector3D.Create(boxVolume.Maximum[0], boxVolume.Minimum[1], boxVolume.Minimum[2]));
      Assert.AreEqual(corners[2], Vector3D.Create(boxVolume.Minimum[0], boxVolume.Maximum[1], boxVolume.Minimum[2]));
      Assert.AreEqual(corners[3], Vector3D.Create(boxVolume.Maximum[0], boxVolume.Maximum[1], boxVolume.Minimum[2]));
      Assert.AreEqual(corners[4], Vector3D.Create(boxVolume.Minimum[0], boxVolume.Minimum[1], boxVolume.Maximum[2]));
      Assert.AreEqual(corners[5], Vector3D.Create(boxVolume.Maximum[0], boxVolume.Minimum[1], boxVolume.Maximum[2]));
      Assert.AreEqual(corners[6], Vector3D.Create(boxVolume.Minimum[0], boxVolume.Maximum[1], boxVolume.Maximum[2]));
      Assert.AreEqual(corners[7], Vector3D.Create(boxVolume.Maximum[0], boxVolume.Maximum[1], boxVolume.Maximum[2]));
    }
  }
}
