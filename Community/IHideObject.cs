namespace System
{
  using ComponentModel;

  /// <summary>
  /// Hides the <see cref="T:System.Object" />-members from the Visual Studio intellisence:
  /// <para> - Equals</para>
  /// <para> - GetHashCode</para>
  /// <para> - GetType</para>
  /// <para> - ToString</para>
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