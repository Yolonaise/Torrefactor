using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrefactorClient.Services
{
  public interface IInitializable
  {
    bool AtStartUp { get; }

    Task Initialize(ILoadingListener l);
  }
}
