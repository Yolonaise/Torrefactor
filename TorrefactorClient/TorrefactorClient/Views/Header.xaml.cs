﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TorrefactorClient.ViewModels;

namespace TorrefactorClient.Views
{
  public partial class Header : Grid
  {


    public string Title
    {
      get { return (string)GetValue(TitleProperty); }
      set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(Header), new PropertyMetadata("Title", new PropertyChangedCallback(OnTitlechanged)));

    private static void OnTitlechanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Header h = d as Header;
      if (h == null)
        return;

      string t = e.NewValue as string;
      if (t == null)
        return;

      h.title.Text = t.ToUpper();
    }

    public Header()
    {
      InitializeComponent();
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
      App.Current.MainWindow.Close();
    }

    private void SettingsClick(object sender, RoutedEventArgs e)
    {
      if (MainViewModel.Instance.Menu != null)
        if (MainViewModel.Instance.Menu.Options != null)
          if (MainViewModel.Instance.Menu.Options.Count > 0)
            MainViewModel.Instance.Menu.SelectedFeature = MainViewModel.Instance.Menu.Options[0];
    }
  }
}
