namespace System.Collections.Generic
{
  using Diagnostics;
  using Linq;

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
        throw new ArgumentNullException("source");
      }

      var index = 0;

      foreach (var item in source)
      {
        if (ReferenceEquals(item, element))
        {
          yield return index;
        }

        index++;
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
        throw new ArgumentNullException("source");
      }

      if (action == null)
      {
        throw new ArgumentNullException("action");
      }

      foreach (var element in source)
      {
        action.Invoke(element);
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
  }
}
