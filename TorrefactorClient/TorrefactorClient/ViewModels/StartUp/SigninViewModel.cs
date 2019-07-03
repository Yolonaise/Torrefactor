using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TorrefactorClient.Helpers.Security;
using TorrefactorClient.Helpers.Ui;
using TorrefactorClient.Rest;
using TorrefactorClient.Rest.Models.Response;
using TorrefactorClient.Services;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class SigninViewModel : BaseViewModel
  {
    private string _errorMessage;
    private bool _isSignin;
    private bool _isVisible;
    private string _username;
    private string _email;
    private IRegistrationListener _listener;

    public CmdBinding<object> CommandSignin { get; set; }
    public CmdBinding CommandLogin { get; set; }

    public bool IsVisible
    {
      get { return _isVisible; }
      set { _isVisible = value; Notify(); }
    }

    public bool IsSignin
    {
      get { return _isSignin; }
      set { _isSignin = value; Notify(); }
    }

    public string ErrorMessage
    {
      get { return _errorMessage; }
      set { _errorMessage = value; Notify(); }
    }

    public string Username
    {
      get { return _username; }
      set { _username = value; Notify(); }
    }

    public string Email
    {
      get { return _email; }
      set { _email = value; Notify(); }
    }

    public SigninViewModel(IRegistrationListener listener)
    {
      _listener = listener;
      CommandSignin = new CmdBinding<object>(Sign);
    }

    private async void Sign(object obj)
    {
      var pwdBox = obj as PasswordBox;
      var pwd = pwdBox == null ? new SecureString() : pwdBox.SecurePassword;
      ErrorMessage = null;
      IsSignin = true;

      try
      {
        var client = new TorrefactorRestClient(string.Empty);
        var result = await client.Signin(Username, Email, SecurityHelper.convertToUNSecureString(pwd));

        if (!result.IsSuccessful)
          ErrorMessage = string.IsNullOrEmpty(result.Content) ? result.ErrorMessage : result.Content;
        else if (_listener != null)
        {
          _listener.OnResgistrationDone(this, JsonConvert.DeserializeObject<LoginResponse>(result.Content));
        }
      }
      finally
      {
        IsSignin = false;
      }
    }
  }
}
