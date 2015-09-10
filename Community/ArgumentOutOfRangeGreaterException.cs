namespace System
{
  /// <summary>
  /// The exception that is thrown when the value of an argument is greater than allowed.
  /// </summary>
  public class ArgumentOutOfRangeGreaterException : ArgumentOutOfRangeException
  {
    public ArgumentOutOfRangeGreaterException(String parameterName, Object other)
      : base(parameterName, String.Format(Exceptions.ArgumentOutOfRangeGreater, parameterName, other))
    {
    }
  }
}