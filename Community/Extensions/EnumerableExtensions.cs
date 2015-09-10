
namespace System.Linq
{
  using Collections.Generic;
  using Diagnostics;

  /// <summary>
  /// Provides a set of static methods for querying objects that implement <see cref="T:System.Collections.Generic.IEnumerable`1" />.
  /// </summary>
  public static class EnumerableExtensions
  {
    /// <summary>
    /// Appends the specified elements after the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Append<TElement>(this IEnumerable<TElement> source, params TElement[] elements)
    {
      return Append(source, elements as IEnumerable<TElement>);
    }

    /// <summary>
    /// Appends the specified elements after the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Append<TElement>(this IEnumerable<TElement> source, IEnumerable<TElement> elements)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");
      Assert.ThrowIfNull<ArgumentNullException>(elements, "elements");

      return source.Concat(elements);
    }

    /// <summary>
    /// Partitions the source-collection using a fixed value to specify how long each partition is.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<IEnumerable<TSource>> Partition<TSource>(this IEnumerable<TSource> source, Int32 partitionSize)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");

      if (partitionSize < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(partitionSize));
      }

      var list = new List<TSource>(partitionSize);
      var counter = 0;

      foreach (var current in source)
      {
        if (counter++ < partitionSize)
        {
          list.Add(current);
        }
        else
        {
          yield return list.ToArray();

          list.Clear();
          counter = 0;
        }
      }
    }

    /// <summary>
    /// Partitions the source-collection using a predicate to specify where a partition stops (by returning false).
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<IEnumerable<TSource>> Partition<TSource>(this IEnumerable<TSource> source, Func<TSource, Boolean> partitioner)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");
      Assert.ThrowIfNull<ArgumentNullException>(partitioner, "continuePredicate");

      var list = new List<TSource>();

      foreach (var current in source)
      {
        if (partitioner(current))
        {
          list.Add(current);
        }
        else
        {
          yield return list.ToArray();

          list.Clear();
        }
      }
    }

    /// <summary>
    /// Returns the element at a specified index in a sequence or a specified default value if the index is out of range.
    /// </summary>
    [DebuggerStepThrough]
    public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, Int32 index, TSource defaultValue)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");
      
      if (index >= 0)
      {
        var list = source as IList<TSource>;

        if (list != null)
        {
          if (index < list.Count)
          {
            return list[index];
          }
        }
        else
        {
          using (var enumerator = source.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              if (index-- == 0)
              {
                return enumerator.Current;
              }
            }
          }
        }
      }

      return defaultValue;
    }

    /// <summary>
    /// Formats the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static String Format<TElement>(this IEnumerable<TElement> source, String format)
    {
      if (source == null)
      {
        throw new NullReferenceException("source");
      }

      return String.Format(format, source.ToArray());
    }

    /// <summary>
    /// Returns a collection of indices where the specified element is within the collection.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<Int32> Indices<TElement>(this IEnumerable<TElement> source, TElement element) where TElement : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      using (var enumerator = source.GetEnumerator())
      {
        for (var index = 0; enumerator.MoveNext(); index++)
        {
          if (ReferenceEquals(enumerator.Current, element))
          {
            yield return index;
          }
        }
      }
    }

    /// <summary>
    /// Invokes a specific action for each element in the collection.
    /// </summary>
    [DebuggerStepThrough]
    public static void Invoke<TElement>(this IEnumerable<TElement> source, Action<TElement> action)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      foreach (var element in source)
      {
        action.Invoke(element);
      }
    }

    /// <summary>
    /// Invokes a specific action for each element in the collection.
    /// </summary>
    [DebuggerStepThrough]
    public static void Invoke<TElement>(this IEnumerable<TElement> source, Action<TElement, Int32> action)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      using (var enumerator = source.GetEnumerator())
      {
        for (var index = 0; enumerator.MoveNext(); index++)
        {
          action.Invoke(enumerator.Current, index);
        }
      }
    }

    /// <summary>
    /// Generates a sequence of indices that are the module/divisor of each value in the source.
    /// </summary>
    /// <example>
    /// If the input is [4, 3, 2], the result will be:
    /// 0, 0, 0, 1, 0, 0, 2, 0, 0, 3, 0, 0,
    /// 0, 1, 0, 1, 1, 0, 2, 1, 0, 3, 1, 0,
    /// 0, 2, 0, 1, 2, 0, 2, 2, 0, 3, 2, 0,
    /// 0, 0, 1, 1, 0, 1, 2, 0, 1, 3, 0, 1,
    /// 0, 1, 1, 1, 1, 1, 2, 1, 1, 3, 1, 1,
    /// 0, 2, 1, 1, 2, 1, 2, 2, 1, 3, 2, 1
    /// </example>
    [DebuggerStepThrough]
    public static IEnumerable<Int32> Modulo(this IEnumerable<Int32> source)
    {
      if (source == null)
      {
        throw new NullReferenceException("dimensions");
      }

      var multiply = new Func<Int32, Int32, Int32>((current, next) => current * next);

      var dimensions = source as Int32[] ?? source.ToArray();
      var count = Math.Max(dimensions.Aggregate(multiply), 0);

      for (var index = 0; index < count; index++)
      {
        for (var dimension = 1; dimension <= dimensions.Length; dimension++)
        {
          var divisor = dimension > 1 ? dimensions.Take(dimension - 1).Aggregate(multiply) : 1;

          yield return (index / divisor) % dimensions[dimension - 1];
        }
      }
    }

    /// <summary>
    /// Prepends the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Prepend<TElement>(this IEnumerable<TElement> source, params TElement[] elements)
    {
      return Prepend(source, elements as IEnumerable<TElement>);
    }

    /// <summary>
    /// Prepends the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Prepend<TElement>(this IEnumerable<TElement> source, IEnumerable<TElement> elements)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");
      Assert.ThrowIfNull<ArgumentNullException>(elements, "elements");

      return elements.Concat(source);
    }

    /// <summary>
    /// Repeats each element in the collection a specific amount of times.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Repeat<TElement>(this IEnumerable<TElement> source, Int32 count)
    {
      if (source == null)
      {
        throw new NullReferenceException("source");
      }

      if (count < 0)
      {
        throw new ArgumentException("count must be greater or equal to zero");
      }

      foreach (var element in source)
      {
        for (var index = 0; index < count; index++)
        {
          yield return element;
        }
      }
    }

    /// <summary>
    /// Repeats the sequence a specific amount of times.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> RepeatSequence<TElement>(this IEnumerable<TElement> source, Int32 count)
    {
      if (source == null)
      {
        throw new NullReferenceException("source");
      }

      if (count < 0)
      {
        throw new ArgumentException("count must be greater or equal to zero");
      }

      for (var index = 0; index < count; index++)
      {
        foreach (var element in source)
        {
          yield return element;
        }
      }
    }

    /// <summary>
    /// Determines whether all elements of a sequence yield the same value with the specified selector.
    /// </summary>
    [DebuggerStepThrough]
    public static Boolean Same<TElement, TValue>(this IEnumerable<TElement> source, Func<TElement, TValue> selector)
    {
      return Same(source, selector, null);
    }

    /// <summary>
    /// Determines whether all elements of a sequence yield the same value with the specified selector.
    /// </summary>
    [DebuggerStepThrough]
    public static Boolean Same<TElement, TValue>(this IEnumerable<TElement> source, Func<TElement, TValue> selector, IEqualityComparer<TValue> comparer)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (selector == null)
      {
        throw new ArgumentNullException(nameof(selector));
      }

      comparer = comparer ?? EqualityComparer<TValue>.Default;

      using (var enumerator = source.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          var firstValue = selector(enumerator.Current);

          while (enumerator.MoveNext())
          {
            var currentValue = selector(enumerator.Current);

            if (comparer.GetHashCode(firstValue) != comparer.GetHashCode(currentValue) && !comparer.Equals(firstValue, currentValue))
            {
              return false;
            }
          }
        }
      }

      return true;
    }
  }
}
