using System;
using System.Collections.Generic;
using System.Linq;

namespace Community.Mathematics.Structures
{
  public class SpatialTree<T>
  {
    public Int32 Dimensions
    {
      get;
      private set;
    }

    public Int32 DivisionCount
    {
      get;
      private set;
    }

    public SpatialTreeNode<T> Root
    {
      get;
      private set;
    }

    public SpatialTree(Int32 dimensions, Vector minimum, Vector maximum)
    {
      if (dimensions < 2)
      {
        throw new ArgumentException("dimensions must be greater than 1");
      }

      Dimensions = dimensions;
      DivisionCount = (Int32)Math.Pow(2, dimensions);
      Root = new SpatialTreeNode<T>(this, minimum, maximum);
    }

    public void Expand(Vector position)
    {
      throw new NotImplementedException();
    }
  }

  public class SpatialTreeNode<T>
  {
    private static readonly SpatialTreeNode<T>[] EmptyNodes = new SpatialTreeNode<T>[0];

    public Vector Maximum
    {
      get;
      private set;
    }

    public Vector Minimum
    {
      get;
      private set;
    }

    public SpatialTreeNode<T>[] Nodes
    {
      get;
      private set;
    }

    public SpatialTreeNode<T> Parent
    {
      get;
      private set;
    }

    public SpatialTree<T> Tree
    {
      get;
      private set;
    }

    public T Value
    {
      get;
      set;
    }

    public SpatialTreeNode(SpatialTree<T> tree, Vector minimum, Vector maximum)
    {
      if (tree == null)
      {
        throw new ArgumentNullException("tree");
      }

      if (minimum == null)
      {
        throw new ArgumentNullException("minimum");
      }

      if (minimum.Size < 1)
      {
        throw new ArgumentException("minimum count must be greater than zero");
      }

      if (maximum == null)
      {
        throw new ArgumentNullException("maximum");
      }

      if (maximum.Size < 1)
      {
        throw new ArgumentException("maximum count must be greater than zero");
      }

      if (minimum.Size != maximum.Size)
      {
        throw new ArgumentException("minimum and maximum must have the same count");
      }

      Maximum = maximum;
      Minimum = minimum;
      Nodes = EmptyNodes;
      Tree = tree;
    }

    public void Divide(Int32 divisionsPerDimension = 2)
    {
      Divide(Enumerable.Repeat(divisionsPerDimension, Tree.Dimensions).ToArray());
    }

    public void Divide(Int32[] divisionsPerDimensions)
    {
      if (divisionsPerDimensions.Length != Tree.Dimensions)
      {
        throw new ArgumentException("divisionsPerDimensions must be ");
      }

      Nodes = new SpatialTreeNode<T>[Tree.DivisionCount];

      for (var index = 0; index < Tree.DivisionCount; index++)
      {
        var dimension = index / 2;
        var halfSize = (Maximum[dimension] - Minimum[dimension]) / 2D;


        Nodes[index] = new SpatialTreeNode<T>(Tree, null, null);
      }
    }
  }
}
