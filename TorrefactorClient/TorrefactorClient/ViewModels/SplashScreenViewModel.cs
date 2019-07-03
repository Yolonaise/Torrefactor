﻿using System;
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

    public void OnResgistrationDone(object sender, LoginResponse response)
    {
      HideRegistration();
    }

    public void OnRegistrationfailed(object sender)
    {
      //Fuck Off
    }

    public void OnLoadingDone(object sender)
    {
    }

    public void OnLoadingfailed(object sender)
    {
    }

    public void OnStepDone(object sender, LoadingDoneEventArgs args)
    {
    }
  }
}
