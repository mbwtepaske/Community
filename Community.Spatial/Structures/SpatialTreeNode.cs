namespace System.Spatial
{
  using Collections.Generic;
  using Collections.ObjectModel;
  using Linq;

  public class SpatialTreeNode<TValue>
  {
    /// <summary>
    /// Gets the indices where the 
    /// </summary>
    public Int32[] Indices
    {
      get
      {
        var indices = new Int32[Level];
        var currentIndex = indices.Length - 1;
        
        for (var current = this; current.Parent != null; current = current.Parent)
        {
          indices[currentIndex--] = current.Parent.Nodes.Indices(current).Single();
        }

        return indices.ToArray();
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

    /// <summary>
    /// Gets a read-only collection containing the child <see cref="T:SpatialTreeNode"/>s of this instance.
    /// </summary>
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

    protected SpatialTreeNode(SpatialTree<TValue> tree, Box box)
      : this(tree, null, box)
    {
    }

    public SpatialTreeNode(SpatialTree<TValue> tree, SpatialTreeNode<TValue> parent, Box box)
    {
      if (tree == null)
      {
        throw new ArgumentNullException(nameof(tree));
      }

      if (box == null)
      {
        throw new ArgumentNullException(nameof(box));
      }
      
      Box = box;
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
      var current = this;

      while (current != null)
      {
        yield return current = current.Parent;
      }

      return this;
    }

    public IEnumerable<TValue> Enumerate(Func<TValue, Boolean> continuationPredicate, Boolean includeSelf = true)
    {
      if (continuationPredicate == null)
      {
        throw new ArgumentNullException(nameof(continuationPredicate));
      }

      return Enumerate(node => continuationPredicate(node.Value), includeSelf).Select(node => node.Value);
    }

    public IEnumerable<SpatialTreeNode<TValue>> Enumerate(Func<SpatialTreeNode<TValue>, Boolean> continuationPredicate, Boolean includeSelf = true)
    {
      if (continuationPredicate == null)
      {
        throw new ArgumentNullException(nameof(continuationPredicate));
      }

      var queue = new Queue<SpatialTreeNode<TValue>>();

      if (includeSelf)
      {
        if (continuationPredicate(this))
        {
          queue.Enqueue(this);
        }
      }
      else if (NodeList != null)
      {
        NodeList.Where(continuationPredicate).Invoke(queue.Enqueue);
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
    public virtual IEnumerable<SpatialTreeNode<TValue>> Split(params Int32[] divisionsPerDimensions)
    {
      if (divisionsPerDimensions.Length != Tree.Dimensions)
      {
        throw new ArgumentOutOfRangeException(nameof(divisionsPerDimensions));
      }

      return Split(divisionsPerDimensions.Select(division => Enumerable.Range(1, division).Select(index => index / Convert.ToDouble(division)).Take(division - 1).ToArray()).ToArray());
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

        var minimum = new Vector(Tree.Dimensions, dimensionIndex => values[dimensionIndex][offsets[dimensionIndex] + 0]);
        var maximum = new Vector(Tree.Dimensions, dimensionIndex => values[dimensionIndex][offsets[dimensionIndex] + 1]);
        var domain = new Box(Interpolation.Linear(Box.Minimum, Box.Maximum, minimum), Interpolation.Linear(Box.Minimum, Box.Maximum, maximum));
        
        result.Add(Tree.CreateNodeInternal(this, domain));
      }

      return result.ToArray();
    }

    /// <summary>
    /// Splits the node into the specified divisions.
    /// </summary>
    public virtual SpatialTreeNode<TValue>[] Split(params Vector[] divisionsPerDimension)
    {
      return Split(Enumerable.Range(0, Tree.Dimensions).Select(index => index < divisionsPerDimension.Length ? divisionsPerDimension[index].ToArray() : new Double[0]).ToArray());
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
        throw new ArgumentNullException(nameof(continuationPredicate));
      }

      if (divisionsPerDimensionsProvider == null)
      {
        throw new ArgumentNullException(nameof(divisionsPerDimensionsProvider));
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
      return Enumerate(current => Collision.Contains(current.Box, position));
    }
    
    public override String ToString()
    {
      return String.Format("L{0}, [{1} - {2}], [{3}]"
        , Level
        , Box.Minimum
        , Box.Maximum
        , String.Join("-", Indices));
    }

    public static  implicit operator TValue(SpatialTreeNode<TValue> treeNode)
    {
      return treeNode.Value;
    }
  }
}