﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TorrefactorClient.Helpers.Security;
using TorrefactorClient.Helpers.Ui;
using TorrefactorClient.Rest;
using TorrefactorClient.Rest.Models.Response;
using TorrefactorClient.Services;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class LoginViewModel : BaseViewModel
  {
    private string _errorMessage;
    private string _logInfo;
    private bool _isLogin;
    private bool _isVisible;
    private IRegistrationListener _listener;

    public CmdBinding<object> CommandLogin { get; set; }
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

    public LoginViewModel(IRegistrationListener listener)
    {
      _listener = listener;
      CommandLogin = new CmdBinding<object>(LogAccount);
    }

    private async void LogAccount(object obj)
    {
      var pwdBox = obj as PasswordBox;
      SecureString pwd = pwdBox == null ? new SecureString() : pwdBox.SecurePassword;


#if DEBUG
      if(string.IsNullOrEmpty(LogInfo))
        _logInfo = "arnaud.schaal9@gmail.com";
      if (pwd.Length == 0)
      {
        pwd = new SecureString();
        pwd.AppendChar('1');
        pwd.AppendChar('2');
        pwd.AppendChar('3');
        pwd.AppendChar('4');
        pwd.AppendChar('5');
        pwd.AppendChar('6');
        pwd.AppendChar('p');
        pwd.AppendChar('w');
        pwd.AppendChar('d');
      }
#endif

      ErrorMessage = null;
      IsLogin = true;

      try
      {
        var client = new TorrefactorRestClient(string.Empty);
        var result = await client.Login(LogInfo, SecurityHelper.convertToUNSecureString(pwd));

        if (!result.IsSuccessful)
          ErrorMessage = string.IsNullOrEmpty(result.Content) ? result.ErrorMessage : result.Content;
        else if (_listener != null)
          await _listener.OnResgistrationDone(this, JsonConvert.DeserializeObject<LoginResponse>(result.Content));
      }
      finally
      {
        IsLogin = false;
      }
    }
  }
}
