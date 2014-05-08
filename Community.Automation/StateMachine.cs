using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Community.Automation
{
  using Abstraction;
  using Configuration;

  public class StateMachine<TState, TTrigger> : IStateMachine<TState, TTrigger>
  {
    private IState<TState, TTrigger> _activeState;
    private IState<TState, TTrigger> _initialState;

    public IState<TState, TTrigger> ActiveState
    {
      get
      {
        return _activeState;
      }
      set
      {
        _activeState = value;
      }
    }

    public IState<TState, TTrigger> InitialState
    {
      get
      {
        return _initialState;
      }
      set
      {
        if (_initialState != value)
        {
          RaisePropertyChanging();

          _initialState = value;

          RaisePropertyChanged();
        }
      }
    }

    public ICollection<IState<TState, TTrigger>> States
    {
      get;
      private set;
    }

    public StateMachine(ICollection<IState<TState, TTrigger>> states)
    {
      States = states;
    }

    public event EventHandler<TransitionEventArgs<TState, TTrigger>> TransitionBegin;

    public event EventHandler<TransitionCompletionEventArgs<TState, TTrigger>> TransitionCompletion;

    public event EventHandler<TransitionEventArgs<TState, TTrigger>> TransitionDeclination;

    public event EventHandler<TransitionFailureEventArgs<TState, TTrigger>> TransitionFailure;

    public void Execute(TTrigger trigger, Object parameter = null)
    {
      if (InitialState == null)
      {
        throw new InvalidOperationException(StateMachine_Exceptions.INITIAL_STATE_IS_NULL);
      }

      var activeState = ActiveState ?? InitialState;

      activeState.Execute(trigger, parameter);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void RaisePropertyChanged([CallerMemberName] String propertyName = null)
    {
      var handler = PropertyChanged;

      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    protected virtual void RaisePropertyChanging([CallerMemberName] String propertyName = null)
    {
      var handler = PropertyChanging;

      if (handler != null)
      {
        handler(this, new PropertyChangingEventArgs(propertyName));
      }
    }
  }
}
