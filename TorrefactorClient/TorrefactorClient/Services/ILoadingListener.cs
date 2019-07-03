using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrefactorClient.Services
{
  public class LoadingDoneEventArgs
  {
    public DateTime Start { get; }
    public int Percent { get; }
    public string OldTitle { get; }
    public string NewTitle { get; }
  }

  public interface ILoadingListener
  {
    void OnLoadingDone(object sender);
    void OnLoadingfailed(object sender);
    void OnStepDone(object sender, LoadingDoneEventArgs args);
  }
}
