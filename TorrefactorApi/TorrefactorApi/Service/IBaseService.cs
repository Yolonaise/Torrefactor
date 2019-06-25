using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrefactorApi.Service
{

  public interface IListener
  {
  }

  public interface IBaseService<TListener> where TListener : IListener
  {
    IList<TListener> Listeners { get; set; }

    void Register(TListener listener);
    void UnRgister(TListener listener);
  }
}
