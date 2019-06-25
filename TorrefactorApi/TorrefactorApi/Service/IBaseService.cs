using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorrefactorApi.Service
{

  public interface IUserListener
  {
  }

  public interface IBaseService<TListener> where TListener : IUserListener
  {
    IList<TListener> Listeners { get; set; }

    void Register(TListener listener);
    void UnRgister(TListener listener);
  }
}
