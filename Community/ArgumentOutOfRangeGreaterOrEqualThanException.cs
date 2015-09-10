namespace System
{
  /// <summary>
  /// The exception that is thrown when the value of an argument is greater or equal.
  /// </summary>
  public class ArgumentOutOfRangeGreaterOrEqualThanException : ArgumentOutOfRangeException
  {
    public ArgumentOutOfRangeGreaterOrEqualThanException(String parameterName, Object other)
      : base(parameterName, String.Format(Exceptions.ArgumentOutOfRangeGreaterOrEqual, parameterName, other))
    {
    }
  }
}