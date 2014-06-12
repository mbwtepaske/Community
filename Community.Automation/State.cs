using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Community.Automation
{
  using Abstraction;

  internal class State<TState, TTrigger> : IState<TState, TTrigger>
  {
    public TransitionBehavior Behavior
    {
      get;
      private set;
    }

    public TState Data
    {
      get;
      private set;
    }

    public IList<IAction> EntryActions
    {
      get;
      private set;
    }

    public IList<IAction> ExitActions
    {
      get;
      private set;
    }

    public IState<TState, TTrigger> InitialState
    {
      get;
      set;
    }

    public IState<TState, TTrigger> RestoreState
    {
      get;
      set;
    }

    public Int32 Level
    {
      get
      {
        var parent = SuperState;

        return parent != null
          ? parent.Level + 1
          : 0;
      }
    }

    public IStateMachine<TState, TTrigger> StateMachine
    {
      get;
      private set;
    }

    public ICollection<IState<TState, TTrigger>> SubStates
    {
      get;
      private set;
    }

    public IState<TState, TTrigger> SuperState
    {
      get;
      private set;
    }

    public IDictionary<TTrigger, ITransition<TState, TTrigger>> Transitions
    {
      get;
      private set;
    }

    public State(IStateMachine<TState, TTrigger> stateMachine, IEnumerable<IState<TState, TTrigger>> subStates, IState<TState, TTrigger> superState)
    {
      StateMachine = stateMachine;
      SubStates = new ObservableCollection<IState<TState, TTrigger>>(subStates);
      SuperState = superState;
    }

    public void Enter(Object parameter)
    {
    }

    public void Exit(Object parameter)
    {
    }

    public bool Execute(TTrigger trigger, object parameter = null)
    {
      return false;
    }
  }
}
