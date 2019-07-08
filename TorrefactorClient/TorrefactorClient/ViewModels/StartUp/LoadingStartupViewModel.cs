using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Services;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class LoadingStartupViewModel : BaseViewModel, ILoadingListener
  {
    private bool _isVisible;
    private ILoadingListener _listener;
    private string _currentTitle;
    private int _n;
    private int _total;

    public bool IsVisible
    {
      get { return _isVisible; }
      set { _isVisible = value; Notify(); }
    }

    public string CurrentTitle
    {
      get { return _currentTitle; }
      set { _currentTitle = value; Notify(); }
    }

    public LoadingStartupViewModel(ILoadingListener listener)
    {
      _listener = listener;
    }

    internal async Task Start()
    {
      try
      {
        _n = 0;
        var ss = ((App)App.Current).Kernel.GetAll<Services.IInitializable>();
        _total = ss.Count();

        foreach (var s in ss)
          await s.Initialize(this);

        OnLoadingDone(this);
      }
      catch (Exception e)
      {
        OnLoadingfailed(this);
      }
    }

    public void OnLoadingDone(object sender)
    {
      _listener.OnLoadingDone(this);
    }

    public void OnLoadingfailed(object sender)
    {
      _listener.OnLoadingfailed(this);
    }

    public void OnStepDone(object sender, LoadingDoneEventArgs args)
    {
      _listener.OnStepDone(sender, args);
    }

    public void OnStepStart(object sender, LoadingDoneEventArgs args)
    {
      CurrentTitle = $"{sender.GetType().Name} - {args.NewTitle} ({++_n}/{_total})       - Nearing the ending view and will see the sky";
      _listener.OnStepStart(sender, args);
    }
  }
}
