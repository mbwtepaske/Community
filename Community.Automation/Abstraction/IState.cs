using System;
using System.Collections.Generic;

namespace Community.Automation.Abstraction
{
  public interface IState<TState, TTrigger>
  {
    TransitionBehavior Behavior
    {
      get;
    }

    TState Data
    {
      get;
    }

    IList<IAction> EntryActions
    {
      get;
    }

    IList<IAction> ExitActions
    {
      get;
    }

    IState<TState, TTrigger> InitialState
    {
      get;
      set;
    }

    IState<TState, TTrigger> RestoreState
    {
      get;
      set;
    }

    Int32 Level
    {
      get;
    }

    IStateMachine<TState, TTrigger> StateMachine
    {
      get;
    }

    ICollection<IState<TState, TTrigger>> SubStates
    {
      get;
    }

    IState<TState, TTrigger> SuperState
    {
      get;
    }

    IDictionary<TTrigger, ITransition<TState, TTrigger>> Transitions
    {
      get;
    }

    void Enter(Object parameter);
    void Exit(Object parameter);

    Boolean Execute(TTrigger trigger, Object parameter = null);
  }
}
