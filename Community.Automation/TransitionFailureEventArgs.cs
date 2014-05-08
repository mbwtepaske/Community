using System;

namespace Community.Automation
{
  public class TransitionFailureEventArgs<TState, TTrigger> : TransitionEventArgs<TState, TTrigger>
  {
    public Exception Exception
    {
      get;
      private set;
    }

    public TransitionFailureEventArgs(TState state, Exception exception, TTrigger trigger, Object triggerParameter = null)
      : base(state, trigger, triggerParameter)
    {
      Exception = exception;
    }
  }
}
