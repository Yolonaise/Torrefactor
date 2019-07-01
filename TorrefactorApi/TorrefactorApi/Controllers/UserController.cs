using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TorrefactorApi.Attributes;
using TorrefactorApi.Repository.Model;
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
    public async Task<IActionResult> Create([FromServices] ITorrefactorContext context, [FromBody] User user)
    {
      try
      {
        return await _service.RegisterUser(context, user);
      }
      catch(Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    [Route("login")]
    [Restriction(needApi: true, needToken: false)]
    public async Task<IActionResult> login([FromServices] ITorrefactorContext context, [FromQuery] string loginfo, [FromQuery] string password)
    {
      try
      {
        return await _service.SignUser(context, loginfo, password);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //[HttpGet]
    //[Route("keepAlive")]
    //[Restriction(needApi: true, needToken: true)]
    //public async Task<IActionResult> KeepAlive([FromServices] ITorrefactorContext context, [FromHeader] string token)
    //{
    //  try
    //  {
        
    //  }
    //  catch (Exception e)
    //  {
    //    return BadRequest(e.Message);
    //  }
    //}
  }
}