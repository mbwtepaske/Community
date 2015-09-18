namespace System
{
  using Diagnostics;

  public static class CloneableExtensions
  {
    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    [DebuggerStepThrough]
    public static T Clone<T>(this ICloneable cloneable) => (T)cloneable.Clone();
  }
}
