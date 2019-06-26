using System;

namespace TorrefactorApi.Attributes
{
  [AttributeUsage(AttributeTargets.Method)]
  public class RestrictionAttribute : System.Attribute
  {
    public bool NeedApi { get; }
    public bool NeedToken { get; }

    public RestrictionAttribute(bool needApi, bool needToken)
    {
      NeedApi = needToken ? needToken : needApi;
      NeedToken = needToken;
    }
  }
}
