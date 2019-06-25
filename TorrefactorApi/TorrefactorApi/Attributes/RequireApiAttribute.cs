using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Context;

namespace TorrefactorApi.Attributes
{
  [AttributeUsage(AttributeTargets.Method)]
  public class RestrictionAttribute : System.Attribute
  {
    public bool NeedApi { get; }
    public bool NeedToken { get; }
    public Application App { get; set; } 

    public RestrictionAttribute(bool needApi, bool needToken)
    {
      NeedApi = needToken ? needToken : needApi;
      NeedToken = needToken;
      App = null;
    }
  }
}
