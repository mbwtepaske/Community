namespace System
{
  using Diagnostics;

	public static class ObjectExtensions
  {
    /// <summary>
    /// Returns false the specified instance is null (Nothing in Visual Basic).
    /// </summary>
    [DebuggerNonUserCode]
    public static Boolean IsNotNull(this Object instance)
    {
      return !Equals(instance, null);
    }

    /// <summary>
    /// Returns true the specified instance is null (Nothing in Visual Basic).
    /// </summary>
    [DebuggerNonUserCode]
    public static Boolean IsNull(this Object instance)
    {
      return Equals(instance, null);
    }
	}
}