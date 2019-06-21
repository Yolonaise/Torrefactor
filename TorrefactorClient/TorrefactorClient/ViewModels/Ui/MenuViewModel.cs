using System.Collections.ObjectModel;
using TorrefactorClient.Models.Ui;
using MaterialDesignThemes.Wpf;
using TorrefactorClient.Helpers.Ui;
using System;

namespace TorrefactorClient.ViewModels.Ui
{
  public class MenuViewModel : BaseViewModel
  {
    private ObservableCollection<MenuFeature> _features;
    private MenuFeature _selectedFeature;

    public ObservableCollection<MenuFeature> Features
    {
      get { return _features; }
      set { _features = value; Notify(); }
    }

    public MenuFeature SelectedFeature
    {
      get { return _selectedFeature; }
      set
      {
        if (_selectedFeature != null)
          _selectedFeature.IsSelected = false;

        _selectedFeature = value;

        if (_selectedFeature != null)
          _selectedFeature.IsSelected = true;

        Notify(); }
    }

    public MenuViewModel()
    {
      _features = new ObservableCollection<MenuFeature>();

      _features.Add(new MenuFeature { Icon = PackIconKind.ViewDashboard , Title = "Dashboard" });
      _features.Add(new MenuFeature { Icon = PackIconKind.ChartBubble , Title = "Charts" });
    }
  }
}
