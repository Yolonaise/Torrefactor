using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Context;

namespace TorrefactorApi.WebContext
{
  public class TorrefactorContext
  {
    public Application App { get; set; }
    public User User { get; set; }
  }
}
