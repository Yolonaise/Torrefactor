using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TorrefactorClient.Services;

namespace TorrefactorClient.Context
{
  public class UserContext : IInitializable
  {
    public bool AtStartUp => true;

    public async Task Initialize(ILoadingListener l)
    {
      l.OnStepStart(this, new LoadingDoneEventArgs("Loooking for your information duddish boy"));
      await Delay();
      l.OnStepDone(this, new LoadingDoneEventArgs("User known by the system"));
    }

    private async Task Delay()
    {
      await Task.Delay(2000);
    }
  }
}
