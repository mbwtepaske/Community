using Community;

namespace System
{
  using Diagnostics;
  using Diagnostics.CodeAnalysis;

  /// <summary>
  /// Represents an class that invokes the an action when the instance is disposed.
  /// </summary>
  public sealed class ActionDisposable : IDisposable
  {
    /// <summary>
    /// Gets the action which is invoked when this instance is disposed.
    /// </summary>
    public Action Action
    {
      get;
    }

    /// <summary>
    /// Gets whether this object has been disposed.
    /// </summary>
    public Boolean IsDisposed
    {
      get;
      private set;
    }

    /// <summary>
    /// Disposes the object.
    /// </summary>
    [DebuggerNonUserCode]
    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public void Dispose()
    {
      if (IsDisposed)
      {
        throw new InvalidOperationException(Exceptions.ObjectAlreadyDisposed);
      }

      try
      {
        Action?.Invoke();
      }
      finally
      {
        IsDisposed = true;
      }
    }

    /// <summary>
    /// Initializes an instance of <see cref="T:System.ActionDisposable"/>.
    /// </summary>
    /// <param name="action">
    /// An action which is invoked when this instance is disposed, this can be null.
    /// </param>
    [DebuggerNonUserCode]
    public ActionDisposable(Action action)
    {
      Action = action;
    }
  }

  public static class IDisposableExtensions
  {
    public static Action AsAction(this IDisposable disposable) => disposable.Dispose;
  }
}
