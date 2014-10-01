namespace System.Spatial
{
  using Collections.Generic;
  using Collections.ObjectModel;
  using Linq;

  public class SpatialTreeNode<TValue>
  {
    public Int32[] Address
    {
      get
      {
        var address = new List<Int32>(Level);

        for (var current = this; current.Parent != null; current = current.Parent)
        {
          address.Insert(0, current.Parent.Nodes.Indices(current).Single());
        }

        return address.ToArray();
      }
    }

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

    public HashSet<SpatialTreeNode<TValue>> Nodes
    {
      get;
      private set;
    }

    public SpatialTreeNode<TValue> Parent
    {
      get;
      private set;
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

    protected SpatialTreeNode(SpatialTree<TValue> tree, Vector minimum, Vector maximum) : this(tree, null, minimum, maximum)
    {
    }

    public SpatialTreeNode(SpatialTree<TValue> tree, SpatialTreeNode<TValue> parent, Vector minimum, Vector maximum)
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
      Nodes = new HashSet<SpatialTreeNode<TValue>>();
      Parent = parent;
      Tree = tree;
    }

    public virtual void Split(params Double[] divisions)
    {
      Split(Enumerable.Repeat(divisions, Tree.Dimensions).ToArray());
    }

    public virtual void Split(Double[][] divisionsPerDimensions)
    {
      if (divisionsPerDimensions.Length != Tree.Dimensions)
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
      var offsets = new Int32[Tree.Dimensions];

      for (var index = 0; index < count; index++, offsets[0]++)
      {
        if (offsets[0] == values[0].Length - 1)
        {
          for (var divisionIndex = 0; divisionIndex < Tree.Dimensions; divisionIndex++)
          {
            if (offsets[divisionIndex] == values[divisionIndex].Length - 1)
            {
              offsets[divisionIndex] = 0;

              if (divisionIndex < Tree.Dimensions - 1)
              {
                offsets[divisionIndex + 1]++;
              }
            }
          }
        }

        var minimum = new Vector(Tree.Dimensions);
        var maximum = new Vector(Tree.Dimensions);

        for (var dimensionIndex = 0; dimensionIndex < Tree.Dimensions; dimensionIndex++)
        {
          minimum[dimensionIndex] = values[dimensionIndex][offsets[dimensionIndex] + 0];
          maximum[dimensionIndex] = values[dimensionIndex][offsets[dimensionIndex] + 1];
        }

        Nodes.Add(Tree.CreateNodeInternal(this
          , Interpolation.Linear(Minimum, Maximum, minimum)
          , Interpolation.Linear(Minimum, Maximum, maximum)));
      }

      node.Nodes = nodes.ToArray();
      Tree.Split(this, divisionsPerDimensions.Select(Enumerable.ToArray).ToArray());
    }

    public override String ToString()
    {
      return base.ToString();
    }
  }
}