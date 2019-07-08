using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Context;

namespace TorrefactorClient.Services
{
  public class ApplicationModule : NinjectModule
  {
    public override void Load()
    {
      Bind<IInitializable>().To<UserContext>();
      Bind<IInitializable>().To<ApplicationContext>();
    }
  }
}
