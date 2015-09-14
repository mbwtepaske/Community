namespace System
{
  using Diagnostics;

  public static class ObjectExtensions
  {
    /// <summary>
    /// Returns true the object is the same as the default value (Nothing in Visual Basic). This method is intended for value-types, for reference-types you should use <see cref="IsNull"/>.
    /// </summary>
    [DebuggerNonUserCode]
    public static Boolean IsDefault<T>(this T instance) => !Equals(instance, default(T));

    /// <summary>
    /// Returns true when the object is not null (Nothing in Visual Basic).
    /// </summary>
    [DebuggerNonUserCode]
    public static Boolean IsNotNull<T>(this T instance) where T : class => !ReferenceEquals(instance, null);

    /// <summary>
    /// Returns true when the object is null (Nothing in Visual Basic).
    /// </summary>
    [DebuggerNonUserCode]
    public static Boolean IsNull<T>(this T instance) where T : class => ReferenceEquals(instance, null);
  }
}