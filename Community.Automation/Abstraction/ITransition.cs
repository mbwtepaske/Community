using System;
using System.Collections.Generic;

namespace Community.Automation.Abstraction
{
  public interface ITransition<TState, TTrigger>
  {
    IEnumerable<IAction> Actions
    {
      get;
    }
    
    ICondition Condition
    {
      get;
    }
    
    IState<TState, TTrigger> Source
    {
      get;
    }

    IState<TState, TTrigger> Target
    {
      get;
    }

    Boolean Execute(Object parameter);
  }
}
