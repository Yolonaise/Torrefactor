using System.IdentityModel.Tokens.Jwt;
using TorrefactorApi.Repository.Model;

namespace TorrefactorApi.Service.Implementation
{
  public class TokenService : IUserListener
  {
    private readonly JwtSecurityTokenHandler _handler;

    public TokenService()
    {
      _handler = new JwtSecurityTokenHandler();
    }

    public void OnUserConnect(User user)
    {
      
    }

    public void OnUserCreated(User user)
    {
    }
  }
}
