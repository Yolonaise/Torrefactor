using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Repository.Model;
using TorrefactorApi.Repository.Repos;

namespace TorrefactorApi.Service
{
  public interface IUserListener : IListener
  {
    void OnUserCreated(User user);
    void OnUserConnect(User user);
  }

  public interface IUserService : IBaseService<IUserListener>
  {
    Task<IActionResult> RegisterUser(User user);
    Task<IActionResult> SignUser(string login, string password);
  }
}
