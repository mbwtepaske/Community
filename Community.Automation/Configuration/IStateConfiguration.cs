using System;
using System.Collections.Generic;

namespace Community.Automation.Configuration
{
  using Abstraction;

  public interface IStateConfiguration<TState, TTrigger>
  {
    IState<TState, TTrigger> State
    {
      get;
    }
      
    IStateMachine<TState, TTrigger> StateMachine
    {
      get;
    }


  }
}
