using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Context;

namespace TorrefactorApi.Service.Implementation
{
  public class TorrefactorContext : ITorrefactorContext
  {
    public User CurrentUser { get ; set; }
    public Application CurrentApplication { get; set; }
  }
}
