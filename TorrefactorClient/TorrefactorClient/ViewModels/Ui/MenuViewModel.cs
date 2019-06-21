using System.Collections.ObjectModel;
using TorrefactorClient.Models.Ui;
using MaterialDesignThemes.Wpf;
using TorrefactorClient.Helpers.Ui;
using System;
using System.Windows.Input;
using System.Windows;
using TorrefactorClient.Views.Features;

namespace TorrefactorClient.ViewModels.Ui
{
  public class MenuViewModel : BaseViewModel
  {
    private ObservableCollection<MenuFeature> _features;
    private ObservableCollection<MenuFeature> _options;
    private MenuFeature _selectedFeature;
    private bool _isOpen;

    public CmdBinding<MenuFeature> Command { get; set; }
    public CmdBinding CommandOpen { get; protected set; }
    public CmdBinding CommandClose { get; protected set; }

    public ObservableCollection<MenuFeature> Features
    {
      get { return _features; }
      set { _features = value; Notify(); }
    }

    public ObservableCollection<MenuFeature> Options
    {
      get { return _options; }
      set { _options = value; Notify(); }
    }

    public bool IsOpen
    {
      get { return _isOpen; }
      set { _isOpen = value; Notify(); }
    }

    public MenuFeature SelectedFeature
    {
      get { return _selectedFeature; }
      set
      {
        if (value == _selectedFeature)
          return;

        if (_selectedFeature != null)
          _selectedFeature.IsSelected = false;

        _selectedFeature = null;
        Notify();

        _selectedFeature = value;

        if (_selectedFeature != null)
          _selectedFeature.IsSelected = true;

        if (Command != null)
          Command.Execute(SelectedFeature);

        Notify();
      }
    }

    public MenuViewModel()
    {
      CommandOpen = new CmdBinding(Open);
      CommandClose = new CmdBinding(Close);

      _features = new ObservableCollection<MenuFeature>();
      _options = new ObservableCollection<MenuFeature>();

      _features.Add(new MenuFeature { Icon = PackIconKind.ViewDashboard , Title = "Dashboard", View = new Home() });
      _features.Add(new MenuFeature { Icon = PackIconKind.ChartBubble , Title = "Charts", View = new Charts() });

      _options.Add(new MenuFeature { Icon = PackIconKind.Settings, Title = "Settings", View = new Parameter() });
    }

    private void Open()
    {
      IsOpen = true;
    }

    private void Close()
    {
      IsOpen = false;
    }
  }
}
