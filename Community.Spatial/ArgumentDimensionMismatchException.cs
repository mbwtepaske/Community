namespace System.Spatial
{
  /// <summary>
  /// The exception that is thrown when the dimension of one of the arguments does not match or is invalid.
  /// </summary>
  public class ArgumentDimensionMismatchException : ArgumentException
  {
    public ArgumentDimensionMismatchException(String parameterName, Int32? expectedValue = null)
      : base(FormatMessage(parameterName, expectedValue))
    {
    }

    private static String FormatMessage(String parameterName, Int32? expectedValue)
    {
      return String.Format(expectedValue.HasValue 
        ? Exceptions.ArgumentVectorDimensionMismatchExpecting 
        : Exceptions.ArgumentVectorDimensionMismatch
        , parameterName
        , expectedValue);
    }
  }
}