using System;

namespace Community.Automation
{
  public class TransitionCompletionEventArgs<TState, TTrigger> : TransitionEventArgs<TState, TTrigger>
  {
    public TState NewState
    {
      get;
      private set;
    }

    public TransitionCompletionEventArgs(TState state, TState newState, TTrigger trigger, Object triggerParameter = null)
      : base(state, trigger, triggerParameter)
    {
      NewState = newState;
    }
  }
}
