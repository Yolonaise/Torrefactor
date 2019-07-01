using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TorrefactorClient.Helpers.Ui;
using TorrefactorClient.Rest;

namespace TorrefactorClient.ViewModels.StartUp
{
  public class SigninViewModel : BaseViewModel
  {
    private string _errorMessage;
    private bool _isSignin;
    private bool _isVisible;
    private string _username;
    private string _password;
    private string _email;

    public CmdBinding CommandSignin { get; set; }
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

    public string Password
    {
      get { return _password; }
      set { _password = value; Notify(); }
    }

    public SigninViewModel()
    {
      CommandSignin = new CmdBinding(Sign);
    }

    private async void Sign()
    {
      ErrorMessage = null;
      IsSignin = true;

      try
      {
        var client = new TorrefactorRestClient(string.Empty);
        var result = await client.Signin(Username, Email, Password);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
          ErrorMessage = result.Content;

      }
      finally
      {
        IsSignin = false;
      }
    }
  }
}
