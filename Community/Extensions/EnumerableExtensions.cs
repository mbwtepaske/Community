namespace System.Collections.Generic
{
  using Diagnostics;
  using Linq;
  using Linq.Expressions;
  using Text;

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
      return Append(source, elements);
    }

    /// <summary>
    /// Appends the specified elements after the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Append<TElement>(this IEnumerable<TElement> source, IEnumerable<TElement> elements)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");
      Assert.ThrowIfNull<ArgumentNullException>(elements, "elements");

      return Enumerable.Concat(source, elements);
    }

    /// <summary>
    /// Prepends the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Prepend<TElement>(this IEnumerable<TElement> source, params TElement[] elements)
    {
      return Prepend(source, elements);
    }

    /// <summary>
    /// Prepends the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Prepend<TElement>(this IEnumerable<TElement> source, IEnumerable<TElement> elements)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");
      Assert.ThrowIfNull<ArgumentNullException>(elements, "elements");

      return Enumerable.Concat(elements, source);
    }
    
    /// <summary>
    /// Invokes the specified action for each element in the collection.
    /// </summary>
    [DebuggerStepThrough]
    public static void ForEach<TElement>(this IEnumerable<TElement> source, Action<TElement> action)
    {
      Assert.ThrowIfNull<NullReferenceException>(source, "source");
      Assert.ThrowIfNull<ArgumentNullException>(action, "action");

      foreach (var element in source)
      {
        action.Invoke(element);
      }
    }
	}
}
