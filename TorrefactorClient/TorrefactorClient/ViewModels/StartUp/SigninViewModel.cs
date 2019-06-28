using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Helpers.Ui;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class SigninViewModel : BaseViewModel
  {
    private bool _isVisible;

    public CmdBinding CommandLogin { get; set; }

    public bool IsVisible
    {
      get { return _isVisible; }
      set { _isVisible = value; Notify(); }
    }

    public SigninViewModel()
    {
    }
  }
}
