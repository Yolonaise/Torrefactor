using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TorrefactorClient.Services;

namespace TorrefactorClient.Context
{
  public class ApplicationContext : IInitializable
  {
    public bool AtStartUp => true;

    public async Task Initialize(ILoadingListener l)
    {
      l.OnStepStart(this, new LoadingDoneEventArgs("Application is making some gearing stuff"));
      await Delay();
      l.OnStepDone(this, new LoadingDoneEventArgs("Application can start"));
    }

    private async Task Delay()
    {
      await Task.Delay(2000);
    }
  }
}
