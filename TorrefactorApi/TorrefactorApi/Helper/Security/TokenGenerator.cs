using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorApi.Context;

namespace TorrefactorApi.Helper.Security
{
  public static class TokenGenerator
  {
    public const string USERNAME = "username";
    public const string PASSWORD = "password";
    public const string EXPIRY_DATE = "expiryDate";

    public enum TokenStatus
    {
      Empty = 0,
      WrongToken = 0,
      Expired = 1,
      Ok = 2
    }

    private const int secondsTokenValidity = 3600;

    public static string Generate(string username, string password, string key)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(key));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
      var header = new JwtHeader(credentials);
      var payload = new JwtPayload { { USERNAME, username }, { EXPIRY_DATE, DateTime.Now.AddSeconds(secondsTokenValidity).ToString() }, { PASSWORD, password } };
      var secToken = new JwtSecurityToken(header, payload);
      var handler = new JwtSecurityTokenHandler();

      return handler.WriteToken(secToken);
    }

    public static JObject ReadToken(string token)
    {
      var result = new JObject();

      var handler = new JwtSecurityTokenHandler();
      var tokenObj = handler.ReadJwtToken(token);

      object stringTemp;
      if (tokenObj.Payload.TryGetValue(USERNAME, out stringTemp))
        result.Add(new JProperty(USERNAME, stringTemp));

      if (tokenObj.Payload.TryGetValue(PASSWORD, out stringTemp))
        result.Add(new JProperty(USERNAME, stringTemp));

      if (tokenObj.Payload.TryGetValue(EXPIRY_DATE, out stringTemp))
        result.Add(new JProperty(EXPIRY_DATE, stringTemp));

      return result;
    }
  }
}
