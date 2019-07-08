using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Rest.Models.Response;

namespace TorrefactorClient.Services
{
  public interface IRegistrationListener
  {
    Task OnResgistrationDone(object sender, LoginResponse response);
    Task OnRegistrationfailed(object sender);
  }
}
