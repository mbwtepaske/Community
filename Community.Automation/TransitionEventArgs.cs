using System;

namespace Community.Automation
{
  public class TransitionEventArgs<TState, TTrigger> : EventArgs
  {
    public TState State
    {
      get;
      private set;
    }

    public TTrigger Trigger
    {
      get;
      private set;
    }

    public Object TriggerParameter
    {
      get;
      private set;
    }

    public TransitionEventArgs(TState state, TTrigger trigger, Object triggerParameter = null)
    {
      State = state;
      Trigger = trigger;
      TriggerParameter = triggerParameter;
    }
  }
}
