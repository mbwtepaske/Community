using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Collections.Generic
{
  //public class PriorityQueue<T> // : ICollection<T>, IReadOnlyCollection<T>
  //{
  //  public delegate IComparable PrioritySelector(T value);
  //  //public delegate TPriority PrioritySelector<out TPriority>(T value) where TPriority : IComparable, IComparable<TPriority>;

  //  private class PriorityComparer : IComparer<T>
  //  {
  //    private readonly PrioritySelector PrioritySelector;

  //    public PriorityComparer(PrioritySelector prioritySelector)
  //    {
  //      PrioritySelector = prioritySelector;
  //    }

  //    public Int32 Compare(T x, T y) => PrioritySelector(x).CompareTo(PrioritySelector(y));
  //  }

  //  private readonly IComparer<T> _comparer;
  //  private readonly IDictionary<IComparable, Queue<T>> _queueDictionary = new Dictionary<IComparable, Queue<T>>();

  //  public PriorityQueue(PrioritySelector prioritySelector) : this(new PriorityComparer(prioritySelector))
  //  {
  //  }

  //  public PriorityQueue(IComparer<T> comparer)
  //  {
  //    _comparer = comparer;
  //  }
  //}

  /// <summary>
  /// A generic, thread-unsafe, priority queue.
  /// </summary>
  public class PriorityQueue<T, TPriority> //: IReadOnlyCollection<T>
    where TPriority : IComparable<TPriority>
  {
    private class Queue : Queue<T>
    {
    }

    private class QueuePool
    {
      public static readonly QueuePool Domain = new QueuePool();

      private readonly ConcurrentBag<Queue> _pool = new ConcurrentBag<Queue>();

      private QueuePool()
      {
      }

      public Queue Allocate()
      {
        var result = default(Queue);

        return _pool.TryTake(out result) ? result : new Queue();
      }

      public void Return(Queue queue) => _pool.Add(queue);
    }

    private readonly IDictionary<TPriority, Queue> _queueDictionary = new SortedDictionary<TPriority, Queue>();
    private readonly Stack<Queue> _queueStack = new Stack<Queue>();

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.PriorityQueue`2" />.
    /// </summary>
    public Int32 Count
    {
      get;
      private set;
    }

    public void Add(TPriority priority, T item)
    {
      var queue = default(Queue);

      if (!_queueDictionary.TryGetValue(priority, out queue))
      {
        _queueDictionary.Add(priority, queue = QueuePool.Domain.Allocate());
      }


    }

    public Boolean TryGet(out T item)
    {
      if (Count > 0)
      {
        var queue = _queueStack.Peek();

        item = queue.Dequeue();

        Count--;

        if (queue.Any())
        {
          _queueStack.Pop();
        }

        return true;
      }

      item = default(T);

      return false;
    }
  }
}