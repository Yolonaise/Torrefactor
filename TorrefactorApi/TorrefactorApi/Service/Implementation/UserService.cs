using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Repository.Model;
using TorrefactorApi.Repository.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace TorrefactorApi.Service.Implementation
{
  public class UserService : ControllerBase, IUserService
  {
    private readonly IUserRepo _repo;
    public IList<IUserListener> Listeners { get; set; }

    public UserService(IServiceProvider services, IUserRepo repo)
    {
      Listeners = new List<IUserListener>();

      foreach (var l in services.GetServices<IUserListener>())
        Register(l);

      _repo = repo;
    }

    public async Task<IActionResult> RegisterUser(ITorrefactorContext context, User user)
    {
      if (user == null)
        return BadRequest("the request is empty");
      
      if (string.IsNullOrEmpty(user.Username))
        return BadRequest("The username is empty");

      if (string.IsNullOrEmpty(user.Email))
        return BadRequest("The Email is empty");

      if (string.IsNullOrEmpty(user.Password))
        return BadRequest("The password is empty");
      
      if (_repo.GetUserByEmail(user.Email) != null)
        return BadRequest("The email is already taken");

      if (_repo.GetUserByUsername(user.Username) != null)
        return BadRequest("The username is already taken");

      await _repo.CreateUser(user);

      foreach (var l in Listeners)
        l.OnUserCreated(context, user);

      return Ok(user);
    }

    public async Task<IActionResult> SignUser(ITorrefactorContext context, string login, string password)
    {
      if (login == null)
        return BadRequest("the login information is empty");

      var user = _repo.GetUserByEmail(login);
      if (user == null)
      {
        user = _repo.GetUserByUsername(login);
        if (user == null)
          return BadRequest("No user found with current login information");
      }
      
      if(user.Password != password)
        return BadRequest("The password is incorrect");

      foreach (var l in Listeners)
        l.OnUserConnect(context, user);

      return Ok(user);
    }

    public void Register(IUserListener listener)
    {
      if (listener == null)
        return;

      Listeners.Add(listener);
    }

    public void UnRgister(IUserListener listener)
    {
      if (listener == null)
        return;

      if (!Listeners.Contains(listener))
        return;

      Listeners.Add(listener);
    }
  }
}
