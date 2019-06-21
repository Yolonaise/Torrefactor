using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrefactorClient.Models.Ui
{
  public class MenuFeature : BaseModel
  {
    private bool _isSelected;
    private PackIconKind _icon;
    private string _title;

    public PackIconKind Icon
    {
      get { return _icon; }
      set { _icon = value; Notify(); }
    }

    public string Title
    {
      get { return _title; }
      set { _title = value; Notify(); }
    }

    public bool IsSelected
    {
      get { return _isSelected; }
      set { _isSelected = value; Notify(); }
    }
  }
}
