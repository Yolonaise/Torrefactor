using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Context;

namespace TorrefactorApi.Service
{
  public interface ITorrefactorContext
  {
    User CurrentUser { get; set; }
    Application CurrentApplication { get; set; }
  }
}
