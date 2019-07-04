using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Services;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class LoadingStartupViewModel : BaseViewModel
  {
    private bool _isVisible;
    private ILoadingListener _listener;

    public bool IsVisible
    {
      get { return _isVisible; }
      set { _isVisible = value; Notify(); }
    }

    public LoadingStartupViewModel(ILoadingListener listener)
    {
      _listener = listener;
    }

    internal void Start()
    {
    }
  }
}
