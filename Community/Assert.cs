namespace System
{
  using Diagnostics;

  /// <summary>
  /// Contains various static methods for generic purposes, like validation of arguments.
  /// </summary>
  public static class Assert
  {
    /// <summary>
    /// Throws a <typeparamref name="TException"/> when the <param name="predicate" /> returns true.
    /// </summary>
    [DebuggerNonUserCode]
    public static void Throw<TException, TObject>(TObject instance, Predicate<TObject> predicate, params Object[] arguments)
      where TException : Exception
    {
      if (predicate(instance))
      {
        throw (Exception)Activator.CreateInstance(typeof(TException), arguments);
      }
    }

    private static readonly Predicate<Object> IsNull = @object => @object == null;

    /// <summary>
    /// Throws <typeparamref name="TException"/> when the object is null.
    /// </summary>
    [DebuggerNonUserCode]
    public static void ThrowIfNull<TException>(Object instance, params Object[] arguments)
      where TException : Exception
    {
      Throw<TException, Object>(instance, IsNull, arguments);
    }

    /// <summary>
    /// Throws <see cref="T:ArgumentNullException"/> when the object is null.
    /// </summary>
    [DebuggerNonUserCode]
    public static void ThrowIfNull(Object instance, params Object[] arguments)
    {
      Throw<ArgumentNullException, Object>(instance, IsNull, arguments);
    }

    /// <summary>
    /// Throws <typeparamref name="TException"/> when the string is null or empty.
    /// </summary>
    [DebuggerNonUserCode]
    public static void ThrowIfNullOrEmpty<TException>(String instance, params Object[] arguments)
      where TException : Exception
    {
      Throw<TException, String>(instance, String.IsNullOrEmpty, arguments);
    }

    /// <summary>
    /// Throws <typeparamref name="TException"/> when the string is null, empty or contains only white-spaces characters.
    /// </summary>
    [DebuggerNonUserCode]
    public static void ThrowIfNullOrWhiteSpace<TException>(String instance, params Object[] arguments)
      where TException : Exception
    {
      Throw<TException, String>(instance, String.IsNullOrWhiteSpace, arguments);
    }
  }
}
