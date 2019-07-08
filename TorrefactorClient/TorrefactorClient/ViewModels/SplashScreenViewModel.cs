using System;
using System.Threading.Tasks;
using TorrefactorClient.Helpers.Ui;
using TorrefactorClient.Rest.Models.Response;
using TorrefactorClient.Services;
using TorrefactorClient.ViewModels.StartUp;
using TorrefactorClient.Views.Startup;

namespace TorrefactorClient.ViewModels
{
  public class SplashScreenViewModel : BaseViewModel, IRegistrationListener, ILoadingListener
  {
    private static SplashScreenViewModel _instance;

    public CmdBinding CommandClose { get; set; }
    
    public SigninViewModel SigninDataContext { get; }

    public LoginViewModel LoginDataContext { get; }

    public LoadingStartupViewModel LoadingDataContext { get; }

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

      SigninDataContext = new SigninViewModel(this);
      LoginDataContext = new LoginViewModel(this);
      LoadingDataContext = new LoadingStartupViewModel(this);

      SigninDataContext.CommandLogin = new CmdBinding(ShowLogin);
      LoginDataContext.CommandCreateAccount = new CmdBinding(ShowSignin);

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

    private void HideRegistration()
    {
      SigninDataContext.IsVisible = false;
      LoginDataContext.IsVisible = false;
    }

    private void Close()
    {
      App.Current.Shutdown();
    }

    public async Task OnResgistrationDone(object sender, LoginResponse response)
    {
      HideRegistration();
      LoadingDataContext.IsVisible = true;
      await LoadingDataContext.Start();
    }

    public async Task OnRegistrationfailed(object sender)
    {
      //Fuck Off
    }

    public void OnLoadingDone(object sender)
    {
      var view = App.Current.MainWindow;
      view.Hide();

      App.Current.MainWindow = new MainWindow();
      App.Current.MainWindow.Show();

      view.Close();
    }

    public void OnLoadingfailed(object sender)
    {
      ShowLogin();
      LoadingDataContext.IsVisible = false;
    }

    public void OnStepDone(object sender, LoadingDoneEventArgs args)
    {
    }

    public void OnStepStart(object sender, LoadingDoneEventArgs args)
    {
    }
  }
}
