using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TorrefactorClient.Helpers.Ui
{
  public class CmdBinding : ICommand
  {
    private Action _a;
    public event EventHandler CanExecuteChanged;

    public CmdBinding(Action a)
    {
      _a = a;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (_a == null)
        return;

      _a();
    }
  }

  public class CmdBinding<T> : ICommand
  {
    private Action<T> _a;
    public event EventHandler CanExecuteChanged;

    public CmdBinding(Action<T> a)
    {
      _a = a;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (_a == null)
        return;

      if (parameter == null)
        return;

      if (!(parameter is T))
        return;

      _a((T)parameter);
    }
  }
}
