namespace System
{
  using Diagnostics;

  public static class EventHandlerExtensions
  {
    [DebuggerNonUserCode]
    public static void Raise(this EventHandler eventHandler, Object sender)
    {
      Raise(eventHandler, sender, EventArgs.Empty);
    }

    [DebuggerNonUserCode]
    public static void Raise(this EventHandler eventHandler, Object sender, EventArgs eventArgs)
    {
      if (eventHandler != null)
      {
        eventHandler.Invoke(sender, eventArgs);
      }
    }

    [DebuggerNonUserCode]
    public static void Raise<TEventArgs>(this EventHandler<TEventArgs> eventHandler, Object sender, TEventArgs eventArgs) where TEventArgs : EventArgs
    {
      Raise(eventHandler, sender, eventArgs);
    }
  }
}
