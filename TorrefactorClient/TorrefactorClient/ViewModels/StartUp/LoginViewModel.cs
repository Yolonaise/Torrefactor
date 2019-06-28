using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Helpers.Ui;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class LoginViewModel : BaseViewModel
  {
    private bool _isVisible;

    public CmdBinding CommandCreateAccount { get; set; }

    public bool IsVisible
    {
      get { return _isVisible; }
      set { _isVisible = value; Notify(); }
    }

    public LoginViewModel()
    {

    }
  }
}
