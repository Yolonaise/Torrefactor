using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TorrefactorApi.Attributes;
using TorrefactorApi.Repository.Model;
using TorrefactorApi.Repository.Repos;
using TorrefactorApi.Service;

namespace TorrefactorApi.Controllers
{
  [ApiController]
  [Route("api/user")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
      _service = service;
    }

    [HttpPost]
    [Route("signin")]
    [Restriction(needApi: true, needToken: false)]
    public async Task<IActionResult> Create([FromBody] User user)
    {
      try
      {
        return await _service.RegisterUser(user);
      }
      catch(Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    [Route("login")]
    [Restriction(needApi: true, needToken: false)]
    public async Task<IActionResult> login([FromQuery] string login, [FromQuery] string password)
    {
      try
      {
        return await _service.SignUser(login, password);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}