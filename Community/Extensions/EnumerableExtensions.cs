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
    /// Formats  the specified elements before the source elements.
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
	}
}
