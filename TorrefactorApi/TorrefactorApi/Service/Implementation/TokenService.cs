using System.IdentityModel.Tokens.Jwt;
using TorrefactorApi.Helper.Security;
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

    public void OnUserConnect(ITorrefactorContext context, User user)
    {
      user.Token = TokenGenerator.Generate(user.Username, user.Password, context.CurrentApplication.ApplicationSecret);
    }

    public void OnUserCreated(ITorrefactorContext context, User user)
    {
      user.Token = TokenGenerator.Generate(user.Username, user.Password, context.CurrentApplication.ApplicationSecret);
    }
  }
}
