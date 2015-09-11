using System.Collections;
using System.Runtime.CompilerServices;

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

    public Vector Center => Interpolation.Linear(Minimum, Maximum, 0.5D);

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

    public ICollection<SpatialTreeNode<TValue>> Nodes
    {
      get;
      private set;
    }

    internal IList<SpatialTreeNode<TValue>> NodeList
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
      NodeList = new Collection<SpatialTreeNode<TValue>>();
      Nodes = new ReadOnlyCollection<SpatialTreeNode<TValue>>(NodeList);
      Parent = parent;
      Tree = tree;
    }

    /// <summary>
    /// Clears the node from its sub-nodes. 
    /// </summary>
    /// <returns></returns>
    public SpatialTreeNode<TValue> Clear()
    {
      foreach (var node in Enumerate().Reverse())
      {
        if (node.NodeList.Any())
        {
          NodeList.Clear();
        }
      }

      return this;
    }

    public IEnumerable<SpatialTreeNode<TValue>> Enumerate()
    {
      return Enumerate(node => true);
    }

    public IEnumerable<SpatialTreeNode<TValue>> Enumerate(Func<SpatialTreeNode<TValue>, Boolean> continuationPredicate)
    {
      if (continuationPredicate == null)
      {
        throw new ArgumentNullException("continuationPredicate");
      }

      var queue = new Queue<SpatialTreeNode<TValue>>();

      if (continuationPredicate(this))
      {
        queue.Enqueue(this);
      }

      while (queue.Count > 0)
      {
        var current = queue.Dequeue();

        yield return current;

        current.NodeList.Where(continuationPredicate).Invoke(queue.Enqueue);
      }
    }

    /// <summary>
    /// Splits the node into the specified divisions.
    /// </summary>
    public virtual IEnumerable<SpatialTreeNode<TValue>> Split(params Double[] divisions)
    {
      return Split(Enumerable.Repeat(divisions, Tree.Dimensions).ToArray());
    }

    /// <summary>
    /// Splits the node into the specified divisions.
    /// </summary>
    public virtual SpatialTreeNode<TValue>[] Split(Double[][] divisionsPerDimensions)
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
        .Aggregate((aggregate, current) => aggregate * current);

      var offsets = new Int32[Tree.Dimensions];
      var result = new List<SpatialTreeNode<TValue>>();

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

        var node = Tree.CreateNodeInternal(this
          , Interpolation.Linear(Minimum, Maximum, minimum)
          , Interpolation.Linear(Minimum, Maximum, maximum));

        //NodeList.Add(node);
        
        result.Add(node);
      }

      return result.ToArray();
    }

    /// <summary>
    /// Continues to split nodes into the specified divisions until the continuation-predicate returns false.
    /// </summary>
    public virtual SpatialTreeNode<TValue>[] Split(Func<SpatialTreeNode<TValue>, Boolean> continuationPredicate, params Double[] divisions)
    {
      return Split(continuationPredicate, node => Enumerable.Repeat(divisions, Tree.Dimensions).ToArray());
    }

    /// <summary>
    /// Continues to split nodes into the specified divisions until the continuation-predicate returns false.
    /// </summary>
    public virtual SpatialTreeNode<TValue>[] Split(Func<SpatialTreeNode<TValue>, Boolean> continuationPredicate, Double[][] divisionsPerDimensions)
    {
      return Split(continuationPredicate, node => divisionsPerDimensions);
    }

    /// <summary>
    /// Continues to split nodes into the specified divisions until the continuation-predicate returns false.
    /// </summary>
    public virtual SpatialTreeNode<TValue>[] Split(Func<SpatialTreeNode<TValue>, Boolean> continuationPredicate, Func<SpatialTreeNode<TValue>, Double[][]> divisionsPerDimensionsProvider)
    {
      if (continuationPredicate == null)
      {
        throw new ArgumentNullException("continuationPredicate");
      }

      if (divisionsPerDimensionsProvider == null)
      {
        throw new ArgumentNullException("divisionsPerDimensionsProvider");
      }

      var queue = new Queue<SpatialTreeNode<TValue>>();
      var result = new List<SpatialTreeNode<TValue>>();

      if (continuationPredicate(this))
      {
        queue.Enqueue(this);
        result.Add(this);
      }

      while (queue.Count > 0)
      {
        var current = queue.Dequeue();

        foreach (var node in current.Split(divisionsPerDimensionsProvider(current)))
        {
          if (continuationPredicate(node))
          {
            queue.Enqueue(node);
          }

          result.Add(node);
        }
      }

      return result.ToArray();
    }

    public virtual IEnumerable<SpatialTreeNode<TValue>> Traverse(Vector position)
    {
      return Enumerate(current 
        => current.Minimum.Zip(position, (left, right) => left.CompareTo(right)).All(value => value <= 0)
        && current.Maximum.Zip(position, (left, right) => left.CompareTo(right)).All(value => value >= 0));
    }

    public override String ToString()
    {
      return String.Format("L{0}, [{1} - {2}], [{3}]"
        , Level
        , Minimum
        , Maximum
        , String.Join("-", Address));
    }
  }
}