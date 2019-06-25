using System.Collections.Generic;
using System.Threading.Tasks;

namespace TorrefactorApi.Repository.Repos
{
  public interface IUserRepo
  {
    IEnumerable<Model.User> GetUsers();
    Model.User GetUserByEmail(string email);
    Model.User GetUserByUsername(string username);
    Task CreateUser(Model.User user);
  }
}
