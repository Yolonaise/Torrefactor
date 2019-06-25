using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Repository.Model;

namespace TorrefactorApi.Service.Implementation
{
  public class UserListennerA : IUserListener
  {
    public void OnUserConnect(User user)
    { 
    }

    public void OnUserCreated(User user)
    {
    }
  }

  public class UserListennerB : IUserListener
  {
    public void OnUserConnect(User user)
    {
    }

    public void OnUserCreated(User user)
    {
    }
  }

  public class UserListennerC : IUserListener
  {
    public void OnUserConnect(User user)
    {
    }

    public void OnUserCreated(User user)
    {
    }
  }
}
