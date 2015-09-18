namespace System
{
  using Diagnostics;

  /// <summary>
  /// Contains various static methods for generic purposes, like validation of arguments.
  /// </summary>
  internal static class ThrowHelper
  {
    private static readonly Predicate<Object> IsNullPredicate = @object => @object == null;

    /// <summary>
    /// Throws <see cref="T:System.ArgumentException" /> when the instance is null.
    /// </summary>
    [DebuggerNonUserCode]
    public static void Argument<TObject>(this TObject instance, Predicate<TObject> predicate, String message, String parameterName = null, Exception innerException = null) => Throw<ArgumentException, TObject>(instance, predicate, message, parameterName, innerException);

    /// <summary>
    /// Throws <see cref="T:System.ArgumentNullException" /> when the instance is null.
    /// </summary>
    [DebuggerNonUserCode]
    public static void ArgumentNull(this Object instance, params Object[] arguments) => Throw<ArgumentNullException, Object>(instance, IsNullPredicate, arguments);

    /// <summary>
    /// Throws <see cref="T:System.ArgumentException" /> when the string is null or empty.
    /// </summary>
    [DebuggerNonUserCode]
    public static void NullOrEmpty(this String instance, params Object[] arguments) => Throw<ArgumentException, String>(instance, String.IsNullOrEmpty, arguments);

    /// <summary>
    /// Throws <see cref="T:System.ArgumentException" /> when the string is null, empty or contains only white-spaces characters.
    /// </summary>
    [DebuggerNonUserCode]
    public static void NullOrWhiteSpace(this String instance, params Object[] arguments) => Throw<ArgumentException, String>(instance, String.IsNullOrWhiteSpace, arguments);

    /// <summary>
    /// Throws <see cref="T:System.NullReferenceException"/> when the instance is null.
    /// </summary>
    [DebuggerNonUserCode]
    public static void NullReference<TObject>(this TObject instance, String parameterName) where TObject : class => Throw<NullReferenceException, Object>(instance, IsNullPredicate, $"{parameterName} ({typeof(TObject).FullName})");

    /// <summary>
    /// Throws a <typeparamref name="TException"/> when the <param name="predicate" /> returns true.
    /// </summary>
    [DebuggerNonUserCode]
    public static void Throw<TException, TObject>(TObject instance, Predicate<TObject> predicate, params Object[] arguments) where TException : Exception
    {
      if (predicate(instance))
    {
        throw (TException) Activator.CreateInstance(typeof(TException), arguments);
      }
    }
  }
}
