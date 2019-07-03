using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TorrefactorClient.Helpers.Ui;
using TorrefactorClient.Models.Ui;
using TorrefactorClient.ViewModels.Ui;

namespace TorrefactorClient.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
    private static MainViewModel _instance;
    private MenuViewModel _menu;
    private FrameworkElement _currentFeature;

    public static MainViewModel Instance
    {
      get
      {
        if (_instance == null)
          _instance = new MainViewModel();

        return _instance;
      }
    }

    public MenuViewModel Menu
    {
      get { return _menu; }
      set
      {
        _menu = value;
        if (_menu != null)
          _menu.Command = new CmdBinding<MenuFeature>(FeatureChange);
        Notify();
      }
    }

    public FrameworkElement CurrentFeature
    {
      get { return _currentFeature; }
      set { _currentFeature = value; Notify(); }
    }

    private MainViewModel()
    {
      Menu = new MenuViewModel();
      CurrentFeature = Menu.Features[0].View;
    }

    private void FeatureChange(MenuFeature obj)
    {
      if (obj == null)
        return;
      
      CurrentFeature = obj.View;
    }
  }
}
