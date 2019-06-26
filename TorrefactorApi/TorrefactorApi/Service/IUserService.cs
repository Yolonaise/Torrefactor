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
    void OnUserCreated(ITorrefactorContext context, User user);
    void OnUserConnect(ITorrefactorContext context, User user);
  }

  public interface IUserService : IBaseService<IUserListener>
  {
    Task<IActionResult> RegisterUser(ITorrefactorContext context, User user);
    Task<IActionResult> SignUser(ITorrefactorContext context, string login, string password);
  }
}
