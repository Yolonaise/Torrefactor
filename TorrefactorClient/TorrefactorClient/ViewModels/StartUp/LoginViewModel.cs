using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Helpers.Ui;
using TorrefactorClient.Rest;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class LoginViewModel : BaseViewModel
  {
    private string _errorMessage;
    private string _logInfo;
    private string _password;
    private bool _isLogin;
    private bool _isVisible;

    public CmdBinding CommandLogin { get; set; }
    public CmdBinding CommandCreateAccount { get; set; }

    public bool IsVisible
    {
      get { return _isVisible; }
      set { _isVisible = value; Notify(); }
    }

    public bool IsLogin
    {
      get { return _isLogin; }
      set { _isLogin = value; Notify(); }
    }

    public string ErrorMessage
    {
      get { return _errorMessage; }
      set { _errorMessage = value; Notify(); }
    }

    public string LogInfo
    {
      get { return _logInfo; }
      set { _logInfo = value; Notify(); }
    }

    public string Password
    {
      get { return _password; }
      set { _password = value; Notify(); }
    }

    public LoginViewModel()
    {
      CommandLogin = new CmdBinding(LogAccount);
    }

    private async void LogAccount()
    {
      ErrorMessage = null;
      IsLogin = true;

      try
      {
        var client = new TorrefactorRestClient(string.Empty);
        var result = await client.Login(_logInfo, _password);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
          ErrorMessage = result.Content;

      }
      finally
      {
        IsLogin = false;
      }
    }
  }
}
