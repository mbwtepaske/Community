namespace System
{
  /// <summary>
  /// The exception that is thrown when the value of an argument is less or equal.
  /// </summary>
  public class ArgumentOutOfRangeLessOrEqualThanException : ArgumentOutOfRangeException
  {
    public ArgumentOutOfRangeLessOrEqualThanException(String parameterName, Object other)
      : base(parameterName, String.Format(Exceptions.ArgumentOutOfRangeLessOrEqual, parameterName, other))
    {
    }
  }
}