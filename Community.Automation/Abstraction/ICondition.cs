using System;

namespace Community.Automation.Abstraction
{
  public interface ICondition
  {
    Boolean Execute(Object parameter);
  }
}
