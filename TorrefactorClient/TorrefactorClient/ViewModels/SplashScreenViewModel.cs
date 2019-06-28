using System;
using TorrefactorClient.Helpers.Ui;
using TorrefactorClient.ViewModels.StartUp;
using TorrefactorClient.Views.Startup;

namespace TorrefactorClient.ViewModels
{
  public class SplashScreenViewModel : BaseViewModel
  {
    private static SplashScreenViewModel _instance;

    private SigninViewModel _signinDataContext;
    private LoginViewModel _loginDataContext;

    public CmdBinding CommandClose { get; set; }
    
    public SigninViewModel SigninDataContext
    {
      get { return _signinDataContext; }
    }

    public LoginViewModel LoginDataContext
    {
      get { return _loginDataContext; }
    }

    public static SplashScreenViewModel Instance
    {
      get
      {
        if (_instance == null)
          _instance = new SplashScreenViewModel();

        return _instance;
      }
    }

    private SplashScreenViewModel()
    {
      CommandClose = new CmdBinding(Close);

      _signinDataContext = new SigninViewModel();
      _loginDataContext = new LoginViewModel();

      _signinDataContext.CommandLogin = new CmdBinding(ShowLogin);
      _loginDataContext.CommandCreateAccount = new CmdBinding(ShowSignin);

      ShowLogin();
    }

    private void ShowSignin()
    {
      LoginDataContext.IsVisible = false;
      SigninDataContext.IsVisible = true;
    }

    private void ShowLogin()
    {
      SigninDataContext.IsVisible = false;
      LoginDataContext.IsVisible = true;
    }

    private void Close()
    {
      App.Current.Shutdown();
    }
  }
}
