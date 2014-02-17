namespace System
{
  using Linq;
  using Linq.Expressions;

  /// <summary>
  /// Contains various static methods for generic purposes, like validation of arguments.
  /// </summary>
  public static class Assert
  {
    /// <summary>
    /// Throws a <typeparamref name="TException"/> when the <param name="expression" /> returns true.
    /// </summary>
    public static void Throw<TException, TObject>(TObject instance, Expression<Predicate<TObject>> expression, params Object[] arguments)
      where TException : Exception
    {
      if (expression.Compile().Invoke(instance))
      {
        throw Activator.CreateInstance(typeof(TException), arguments) as Exception;
      }
    }

    /// <summary>
    /// Throws <typeparamref name="TException"/> when the object is null.
    /// </summary>
    public static void ThrowIfNull<TException>(Object instance, params Object[] arguments)
      where TException : Exception
    {
      Throw<TException, Object>(instance, @object => @object == null, arguments);
    }

    /// <summary>
    /// Throws <typeparamref name="TException"/> when the string is null or empty.
    /// </summary>
    public static void ThrowIfNullOrEmpty<TException>(String instance, params Object[] arguments)
      where TException : Exception
    {
      Throw<TException, String>(instance, @object => String.IsNullOrEmpty(@object), arguments);
    }

    /// <summary>
    /// Throws <typeparamref name="TException"/> when the string is null, empty or contains only white-spaces characters.
    /// </summary>
    public static void ThrowIfNullOrEmpty<TException>(String instance, params Object[] arguments)
      where TException : Exception
    {
      Throw<TException, String>(instance, @object => String.IsNullOrWhiteSpace(@object), arguments);
    }
  }
}
