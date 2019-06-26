using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Attributes;
using TorrefactorApi.Context;
using TorrefactorApi.Service;

namespace TorrefactorApi.Filters
{
  public class ApiFilter : IAsyncActionFilter
  {
    private readonly UserDbContext _context;
    private readonly ITorrefactorContext _tContext;

    public ApiFilter(UserDbContext context, ITorrefactorContext tContext)
    {
      _context = context;
      _tContext = tContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

      if (CheckApi(context, controllerActionDescriptor == null ? null : controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)))
      {
        var result = await next();
      }
    }

    private bool CheckApi(ActionExecutingContext context, Object[] attributes)
    {
      RestrictionAttribute attr = null;

      foreach (var a in attributes)
        if (a is RestrictionAttribute)
          attr = a as RestrictionAttribute;

      if (attr != null && attr.NeedApi)
      {
        StringValues apiKey = string.Empty;
        if (!context.HttpContext.Request.Headers.TryGetValue("apiKey", out apiKey))
          return BadRequest(context, "Api is Required");

        var app = _context.Applications.FirstOrDefault(a => a.ApiKey == apiKey);
        if (app == null)
          return BadRequest(context, "Api key not exist");

        _tContext.CurrentApplication = app;
      }
      return true;
    }

    private bool BadRequest(ActionExecutingContext context, string msg)
    {
      context.Result = new BadRequestObjectResult(msg);
      return false;
    }
  }
}
