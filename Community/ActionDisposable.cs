using Community;

namespace System
{
  using Diagnostics;

  /// <summary>
  /// Represents an class that invokes the an action when the instance is disposed.
  /// </summary>
  public class ActionDisposable : IDisposable
  {
    /// <summary>
    /// Gets the action which is invoked when this instance is disposed.
    /// </summary>
    public Action Action
    {
      get;
      private set;
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
    public void Dispose()
    {
      if (IsDisposed)
      {
        throw new InvalidOperationException(Exceptions.OBJECT_ALREADY_DISPOSED);
      }

      try
      {
        if (Action != null)
        {
          Action.Invoke();
        }
      }
      finally
      {
        IsDisposed = true;
      }
    }

    /// <summary>
    /// Initializes an instance of <see cref="System.ActionDisposable"/>.
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
}
