namespace System
{
  /// <summary>
  /// The exception that is thrown when the value of an argument is too small.
  /// </summary>
  public class ArgumentOutOfRangeTooSmallException : ArgumentOutOfRangeException
  {
    public ArgumentOutOfRangeTooSmallException(String parameterName)
      : base(parameterName, String.Format(Exceptions.ArgumentOutOfRangeTooSmall, parameterName))
    {
    }
  }
}