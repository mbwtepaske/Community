namespace System
{
  using ComponentModel;

  /// <summary>
  /// Hides the <see cref="T:System.Object" />-methods 'Equals', 'GetHashCode', 'GetType' and 'ToString' from the Visual Studio intellisence.
  /// </summary>
  /// <resharper>
  /// Default configuration displays hidden members. 
  /// </resharper>
  public interface IHideObject
  {
    [EditorBrowsable(EditorBrowsableState.Never)]
    Boolean Equals(Object obj);

    [EditorBrowsable(EditorBrowsableState.Never)]
    Int32 GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    Type GetType();

    [EditorBrowsable(EditorBrowsableState.Never)]
    String ToString();
  }
}
