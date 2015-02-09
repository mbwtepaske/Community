﻿using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Spatial
{
  using Collections;
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

    public Domain Domain
    {
      get;
      private set;
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

    public IReadOnlyCollection<SpatialTreeNode<TValue>> Nodes
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

    protected SpatialTreeNode(SpatialTree<TValue> tree, Domain domain)
      : this(tree, null, domain)
    {
    }

    public SpatialTreeNode(SpatialTree<TValue> tree, SpatialTreeNode<TValue> parent, Domain domain)
    {
      if (tree == null)
      {
        throw new ArgumentNullException("tree");
      }

      if (domain == null)
      {
        throw new ArgumentNullException("domain");
      }
      
      Domain = domain;
      NodeList = new Collection<SpatialTreeNode<TValue>>();
      Nodes = new ReadOnlyCollection<SpatialTreeNode<TValue>>(NodeList);
      Parent = parent;
      Tree = tree;
    }

    public IEnumerable<SpatialTreeNode<TValue>> Ancestry()
    {
      var current = this;

      while (current != null)
      {
        yield return current = current.Parent;
      }
    }

    public virtual void Clear()
    {
      foreach (var node in Enumerate().Reverse().Where(node => node.NodeList.Any()))
      {
        node.NodeList.Clear();
      }
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
    public virtual IEnumerable<SpatialTreeNode<TValue>> Split(params Int32[] divisionsPerDimensions)
    {
      if (divisionsPerDimensions.Length != Tree.Dimensions)
      {
        throw new ArgumentOutOfRangeException("divisionsPerDimensions");
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

        var minimum = Vector.Build.Dense(Tree.Dimensions);
        var maximum = Vector.Build.Dense(Tree.Dimensions);

        for (var dimensionIndex = 0; dimensionIndex < Tree.Dimensions; dimensionIndex++)
        {
          minimum[dimensionIndex] = values[dimensionIndex][offsets[dimensionIndex] + 0];
          maximum[dimensionIndex] = values[dimensionIndex][offsets[dimensionIndex] + 1];
        }

        var domain = new Domain(Interpolation.Linear(Domain.Minimum, Domain.Maximum, minimum), Interpolation.Linear(Domain.Minimum, Domain.Maximum, maximum));
        
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
        => current.Domain.Minimum.Zip(position, (left, right) => left.CompareTo(right)).All(value => value <= 0)
        && current.Domain.Maximum.Zip(position, (left, right) => left.CompareTo(right)).All(value => value >= 0));
    }

    public override String ToString()
    {
      return String.Format("L{0}, [{1} - {2}], [{3}]"
        , Level
        , Domain.Minimum
        , Domain.Maximum
        , String.Join("-", Indices));
    }
  }
}