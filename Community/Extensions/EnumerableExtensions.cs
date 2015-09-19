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
    public static IEnumerable<TElement> Append<TElement>(this IEnumerable<TElement> source, params TElement[] elements) => Append(source, elements as IEnumerable<TElement>);

    /// <summary>
    /// Appends the specified elements after the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Append<TElement>(this IEnumerable<TElement> source, IEnumerable<TElement> elements) => source.Concat(elements);

    /// <summary>
    /// Prepends the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Prepend<TElement>(this IEnumerable<TElement> source, params TElement[] elements) => Prepend(source, elements as IEnumerable<TElement>);

    /// <summary>
    /// Prepends the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Prepend<TElement>(this IEnumerable<TElement> source, IEnumerable<TElement> elements) => elements.Concat(source);

    /// <summary>
    /// Invokes the specified action for each of the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static void Invoke<TElement>(this IEnumerable<TElement> source, Action<TElement> action)
    {
      foreach (var element in source)
      {
        action(element);
      }
    }

    /// <summary>
    /// Formats the specified elements before the source elements.
    /// </summary>
    [DebuggerStepThrough]
    public static String Format<TElement>(this IEnumerable<TElement> source, String format) => String.Format(format, source.ToArray());

    /// <summary>
    /// Repeats each element in the collection a specific amount of times.
    /// </summary>
    [DebuggerStepThrough]
    public static IEnumerable<TElement> Repeat<TElement>(this IEnumerable<TElement> source, Int32 count)
    {
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
    public static IEnumerable<TElement> RepeatSequence<TElement>(this IEnumerable<TElement> source, Int32 count) => Enumerable.Repeat(source, count).SelectMany(s => s);
  }
}
