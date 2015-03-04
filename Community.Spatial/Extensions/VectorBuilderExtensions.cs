namespace MathNet.Numerics.LinearAlgebra.Double
{
  /// <summary>
  /// Generic linear algebra type builder, for situations where a matrix or vector must be created in a generic way. Usage of generic builders should not be required in normal user code.
  /// </summary>
  public static class VectorBuilderExtensions
  {
    /// <summary>
    /// Create a dense vector of T that is directly bound to the specified array.
    /// </summary>
    public static Vector Dense(this VectorBuilder<System.Double> builder, params System.Double[] values)
    {
      return new DenseVector(values);
    }
  }
}