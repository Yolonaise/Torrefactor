using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TorrefactorClient.ViewModels
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void Notify([CallerMemberName] string propertyName = "")
    {
      if (PropertyChanged == null)
        return;

      PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
