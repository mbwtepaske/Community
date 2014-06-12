using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Automation.Abstraction
{
  public interface IAction
  {
    void Execute(Object parameter);
  }
}
