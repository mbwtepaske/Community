namespace System
{
  /// <summary>
  /// The exception that is thrown when the value of an argument is less than allowed.
  /// </summary>
  public class ArgumentOutOfRangeLessException : ArgumentOutOfRangeException
  {
    public ArgumentOutOfRangeLessException(String parameterName, Object other)
      : base(parameterName, String.Format(Exceptions.ArgumentOutOfRangeLess, parameterName, other))
    {
    }
  }
}