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
      Assert.ThrowIfNull<NullReferenceException>(instance);
      Assert.ThrowIfNull(expression, "expression");
      Assert.ThrowArgument(expression.Body, exp => exp.NodeType != ExpressionType.MemberAccess, "expression body must be a member accessor");

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