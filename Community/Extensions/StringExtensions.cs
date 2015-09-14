namespace System
{
  using Diagnostics;

  public static class StringExtensions
  {
    /// <summary>
    /// Returns an instance of <typeparam name="TException" /> where the first argument is assumed to be the message-string.
    /// </summary>
    [DebuggerStepThrough]
    public static TException FormatException<TException>(this String instance, params Object[] arguments) where TException : Exception => (TException)Activator.CreateInstance(typeof(TException), FormatString(instance, null, arguments));

    /// <summary>
    /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
    /// </summary>
    [DebuggerStepThrough]
    public static String FormatString(this String instance, params Object[] arguments) => FormatString(instance, null, arguments);

    /// <summary>
    /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
    /// A specified parameter supplies culture-specific formatting information.
    /// </summary>
    [DebuggerStepThrough]
    public static String FormatString(this String instance, IFormatProvider formatProvider, params Object[] arguments) => String.Format(formatProvider, instance, arguments);

    /// <summary>
    /// Indicates whether the specified string is null or an System.String.Empty string.
    /// </summary>
    [DebuggerStepThrough]
    public static Boolean IfNullOrEmpty(this String instance) => String.IsNullOrEmpty(instance);

    /// <summary>
    /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    [DebuggerStepThrough]
    public static Boolean IsNullOrWhiteSpace(this String instance) => String.IsNullOrWhiteSpace(instance);

    /// <summary>
    /// Returns a String array that contains the substrings in this String that are delimited by elements of a specified Unicode character array. A parameter specifies whether to return empty array elements.
    /// </summary>
    [DebuggerStepThrough]
    public static String[] Split(this String instance, String separator, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries) => instance.Split(new[] { separator }, options);
  }
}
