using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace System
{
  public interface IObjectPool<T> 
    where T : class
  {
    Int32 Capacity
    {
      get;
    }

    Int32 Count
    {
      get;
    }

    Boolean TryStore(T item);

    Boolean TryTake(out T element);
  }

  /// <summary>
  /// Generic implementation of object pooling pattern.
  /// </summary>
  public sealed class ObjectPool<T> : IObjectPool<T>
    where T : class
  {
    public enum Behavior
    {
      /// <summary>
      /// First-in, First-out.
      /// </summary>
      FIFO,

      /// <summary>
      /// First-in, Last-out.
      /// </summary>
      FILO,
    }
    
    public const Int32 DefaultCapacity = 4;
    
    public Int32 Capacity
    {
      get;
    }

    public Int32 Count => _items.Count;

    private readonly ICollection _items;
    private readonly Action<T> _itemStorer;
    private readonly Func<T> _itemTaker;

    /// <summary>
    /// Initializes a <see cref="T:System.ObjectPool`1" />.
    /// </summary>
    /// <param name="capacity">The maximum amount of elements that can be stored.</param>
    /// <param name="behavior">The behavior in which the object-pool stores and takes the elements.</param>
    public ObjectPool(Int32 capacity = DefaultCapacity, Behavior behavior = Behavior.FILO)
    {
      if (capacity < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(capacity));
      }

      Capacity = capacity;

      switch (behavior)
      {
        case Behavior.FIFO:
          var queue = new Queue<T>(capacity);
          
          _itemStorer = queue.Enqueue;
          _itemTaker = queue.Dequeue;
          _items = queue;
          break;

        case Behavior.FILO:
          var stack = new Stack<T>(capacity);

          _itemStorer = stack.Push;
          _itemTaker = stack.Pop;
          _items = stack;
          break;

        default:
          throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null);
      }
    }

    /// <summary>
    /// Returns an element from the <see cref="T:System.ObjectPool`1" />.
    /// 
    /// <para>
    /// Exceptions:
    /// <see cref="T:System.InvalidOperationException" />: <see cref="T:System.ObjectPool`1" /> is Empty.
    /// </para>
    /// </summary>
    public T Take()
    {
      var item = default(T);

      if (!TryTake(out item))
      {
        throw new InvalidOperationException(Exceptions.OBJECT_POOL_EMPTY);
      }

      return item;
    }

    /// <summary>
    /// Returns an element from the <see cref="T:System.ObjectPool`1" /> or returns null.
    /// </summary>
    public T TakeOrDefault()
    {
      var item = default(T);
      
      return TryTake(out item) ? item : null;
    }

    /// <summary>
    /// Attempts to remove an element from the <see cref="T:System.ObjectPool`1" />.
    /// </summary>
    /// <returns>
    /// Returns true when the item was removed from the <see cref="T:System.ObjectPool`1" />.
    /// </returns>
    public Boolean TryTake(out T item)
    {
      if (Count > 0)
      {
        item = _itemTaker();

        return true;
      }

      item = null;

      return false;
    }

    public void Store(T item)
    {
      if (!TryStore(item))
      {
        throw new InvalidOperationException(Exceptions.OBJECT_POOL_FULL);
      }
    }

    /// <summary>
    /// Attempts to add an element to the <see cref="T:System.ObjectPool`1" />.
    /// </summary>
    /// <returns>
    /// Returns true when the item was successfully added to the <see cref="T:System.ObjectPool`1" />.
    /// </returns>
    public Boolean TryStore(T item)
    {
      if (Count < Capacity)
      {
        _itemStorer(item);

        return true;
      }
      
      return false;
    }
  }
}
