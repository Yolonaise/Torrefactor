using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrefactorClient.Rest.Models.Response
{
  public class LoginResponse
  {
    [JsonProperty(PropertyName = "token")]
    public string Token { get; set; }
  }
}
