namespace System
{
  using Diagnostics;
  using Linq.Expressions;

	public static class ObjectExtensions
  {
    /// <summary>
    /// Returns the name of the accessed member.
    /// </summary>
    [DebuggerNonUserCode]
	  public static String GetPropertyName<TObject, TMember>(this TObject instance, Expression<Func<TObject, TMember>> expression)
	  {
      if (expression == null)
      {
        throw new ArgumentNullException("expression");
      }

      if (expression.NodeType != ExpressionType.MemberAccess)
      {
        throw new ArgumentException("expression body must be a member accessor");
      }

      return (expression.Body as MemberExpression).Member.Name;
	  }

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