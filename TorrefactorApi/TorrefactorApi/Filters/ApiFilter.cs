using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Attributes;
using TorrefactorApi.Context;

namespace TorrefactorApi.Filters
{
  public class ApiFilter : IAsyncActionFilter
  {
    private readonly UserDbContext _context;

    public ApiFilter(UserDbContext context)
    {
      _context = context;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      await _context.SaveChangesAsync();
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
        {
          context.Result = new BadRequestObjectResult("Api is Required");
          return false;
        }

        var app = _context.Applications.FirstOrDefault(a => a.ApiKey == apiKey);
        if (app == null)
        {
          context.Result = new BadRequestObjectResult("Api key not exist");
          return false;
        }

        attr.App = app;
      }
      return true;
    }
  }
}
