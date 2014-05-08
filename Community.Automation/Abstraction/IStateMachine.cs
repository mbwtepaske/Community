using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Community.Automation.Abstraction
{
  using Configuration;

  public interface IStateMachine<TState, TTrigger> : INotifyPropertyChanged, INotifyPropertyChanging
  {
    IState<TState, TTrigger> ActiveState
    {
      get;
      set;
    }

    IState<TState, TTrigger> InitialState
    {
      get;
      set;
    }

    ICollection<IState<TState, TTrigger>> States
    {
      get;
    }

    /// <summary>
    /// Invoked when a transition exists for the trigger.
    /// </summary>
    event EventHandler<TransitionEventArgs<TState, TTrigger>> TransitionBegin;

    /// <summary>
    /// Invoked when a transition has successfully switched states.
    /// </summary>
    event EventHandler<TransitionCompletionEventArgs<TState, TTrigger>> TransitionCompletion;

    /// <summary>
    /// Invoked when no transition exists for the trigger.
    /// </summary>
    event EventHandler<TransitionEventArgs<TState, TTrigger>> TransitionDeclination;

    /// <summary>
    /// Invoked when a transition has resulted into an exception.
    /// </summary>
    event EventHandler<TransitionFailureEventArgs<TState, TTrigger>> TransitionFailure;

    void Execute(TTrigger trigger, Object parameter = null);
  }
}
