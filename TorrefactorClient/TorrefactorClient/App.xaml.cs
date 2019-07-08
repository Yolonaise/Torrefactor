using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TorrefactorClient.Services;

namespace TorrefactorClient
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public IKernel Kernel { get; private set; }

    private void StartUp(object sender, StartupEventArgs args)
    {
      Kernel = new Ninject.StandardKernel(new ApplicationModule());

      SplashScreen mainView = new SplashScreen();
      mainView.Show();
    }
  }
}
