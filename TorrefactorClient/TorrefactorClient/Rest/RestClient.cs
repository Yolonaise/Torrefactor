using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrefactorClient.Rest.Models.Request;

namespace TorrefactorClient.Rest
{
  public class TorrefactorRestClient : RestClient
  {
    const string apiKey = "1234ApiKey";
    const string apiSecret = "ZE09ZE0CKOPZEOkopkcs09SE";

    const string baseUrl = @"http://localhost:62513";
    private string _token;

    public TorrefactorRestClient(string token)
    {
      BaseUrl = new Uri(baseUrl);
      _token = token;
    }

    private RestRequest GetRequest(Method method, string endpoint)
    {
      var request = new RestRequest(endpoint, method, DataFormat.Json);

      if (!string.IsNullOrEmpty(apiKey))
        request.AddHeader(Headers.API_KEY, apiKey);

      if (!string.IsNullOrEmpty(_token))
        request.AddHeader(Headers.TOKEN, _token);

      return request;
    }

    public async Task<IRestResponse> Signin(string username, string email, string password)
    {
      var request = GetRequest(Method.POST, "api/user/signin");

      request.AddJsonBody(new SigninRequest
      {
        Email = email,
        Username = username,
        Password = password
      });

      return await ExecutePostTaskAsync(request);
    }

    public async Task<IRestResponse> Login(string login, string password)
    {
      var request = GetRequest(Method.POST,
        $"/api/user/login?{QueryParameters.LOGIN}={login}&{QueryParameters.PASSWORD}={password}");

      return await ExecutePostTaskAsync(request);
    }
  }
}
