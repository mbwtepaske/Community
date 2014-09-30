namespace System.Spatial
{
  using Collections.Generic;
  using Linq;

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

    public SpatialTree(Int32 dimensions, Vector minimum, Vector maximum)
    {
      if (dimensions < 1)
      {
        throw new ArgumentException("dimensions must be greater than zero");
      }

      if (minimum == null)
      {
        throw new ArgumentNullException("minimum");
      }

      if (maximum == null)
      {
        throw new ArgumentNullException("maximum");
      }

      Dimensions = dimensions;
      Root = new SpatialTreeNode<TValue>(this, minimum, maximum);
    }

    public virtual void Expand()
    {
    }

    public virtual void Split(SpatialTreeNode<TValue> node, Double[][] divisionsPerDimensions)
    {
      if (node == null)
      {
        throw new ArgumentNullException("node");
      }

      if (divisionsPerDimensions.Length != Dimensions)
      {
        throw new ArgumentException("divisionsPerDimensions must have the same amount as the dimensions of the spatial tree");
      }

      if (divisionsPerDimensions.SelectMany(dimension => dimension).Any(division => division <= 0D || division >= 1D))
      {
        throw new ArgumentException("divisionsPerDimensions may only contain values greater than zero and smaller than one");
      }

      var values = divisionsPerDimensions
        .Select(divisions => divisions
          .OrderBy(division => division)
          .Distinct()
          .Prepend(0D)
          .Append(1D)
          .ToArray())
        .ToArray();
      
      // Calculate the number of nodes to generate
      var count = values
        .Select(divisions => divisions.Length - 1)
        .Aggregate((result, current) => result * current);

      var nodes = new SpatialTreeNode<TValue>[count];
      var offsets = new Int32[Dimensions];

      for (var index = 0; index < count; index++, offsets[0]++)
      {
        if (offsets[0] == values[0].Length - 1)
        {
          for (var divisionIndex = 0; divisionIndex < Dimensions; divisionIndex++)
          {
            if (offsets[divisionIndex] == values[divisionIndex].Length - 1)
            {
              offsets[divisionIndex] = 0;

              if (divisionIndex < Dimensions - 1)
              {
                offsets[divisionIndex + 1]++;
              }
            }
          }
        }

        var minimum = new Vector(Dimensions);
        var maximum = new Vector(Dimensions);

        for (var dimensionIndex = 0; dimensionIndex < Dimensions; dimensionIndex++)
        {
          minimum[dimensionIndex] = values[dimensionIndex][offsets[dimensionIndex] + 0];
          maximum[dimensionIndex] = values[dimensionIndex][offsets[dimensionIndex] + 1];
        }

        nodes[index] = new SpatialTreeNode<TValue>(this
          , Interpolation.Linear(node.Minimum, node.Maximum, minimum)
          , Interpolation.Linear(node.Minimum, node.Maximum, maximum))
        {
          Parent = node
        };
      }

      node.Nodes = nodes.ToArray();
    }
  }

  public class SpatialTreeNode<TValue>
  {
    public Int32 Level
    {
      get
      {
        return Parent != null
          ? Parent.Level + 1
          : 0;
      }
    }

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

    public SpatialTreeNode<TValue>[] Nodes
    {
      get;
      set;
    }

    public SpatialTreeNode<TValue> Parent
    {
      get;
      set;
    }

    public SpatialTree<TValue> Tree
    {
      get;
      private set;
    }

    public TValue Value
    {
      get;
      set;
    }

    public SpatialTreeNode(SpatialTree<TValue> tree, Vector minimum, Vector maximum)
    {
      if (tree == null)
      {
        throw new ArgumentNullException("tree");
      }

      if (minimum == null)
      {
        throw new ArgumentNullException("minimum");
      }

      if (minimum.Size != tree.Dimensions)
      {
        throw new ArgumentException("minimum size must be equal to the dimensions of the spatial tree");
      }

      if (maximum == null)
      {
        throw new ArgumentNullException("maximum");
      }

      if (maximum.Size != tree.Dimensions)
      {
        throw new ArgumentException("maximum size must be equal to the dimensions of the spatial tree");
      }

      Maximum = maximum;
      Minimum = minimum;
      Tree = tree;
    }

    public void Split(params Double[] divisionsPerDimensions)
    {
      Tree.Split(this, Enumerable.Repeat(divisionsPerDimensions, Tree.Dimensions).ToArray());
    }

    public void Split(IEnumerable<IEnumerable<Double>> divisionsPerDimensions)
    {
      Tree.Split(this, divisionsPerDimensions.Select(Enumerable.ToArray).ToArray());
    }
  }
}
