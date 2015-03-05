namespace System
{
  /// <summary>
  /// The exception that is thrown when the value of an argument is too large.
  /// </summary>
  public class ArgumentOutOfRangeTooLargeException : ArgumentOutOfRangeException
  {
    public ArgumentOutOfRangeTooLargeException(String parameterName)
      : base(parameterName, String.Format(Exceptions.ArgumentOutOfRangeTooLarge, parameterName))
    {
    }
  }
}