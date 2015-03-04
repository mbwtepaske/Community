namespace System.Spatial
{
  /// <summary>
  /// The exception that is thrown when the dimension of one of the arguments does not match or is invalid.
  /// </summary>
  public class DimensionMismatchException : ArgumentException
  {
    public DimensionMismatchException(Exception innerException = null)
      : base(String.Empty, null, innerException)
    {
    }

    public DimensionMismatchException(String parameterName, Exception innerException = null)
      : base(String.Empty, parameterName, innerException)
    {
    }

    public DimensionMismatchException(String message, String parameterName, Exception innerException = null)
      : base(message, parameterName, innerException)
    {
    }
  }
}