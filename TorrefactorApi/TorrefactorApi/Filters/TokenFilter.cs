using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TorrefactorApi.Attributes;
using TorrefactorApi.Context;

namespace TorrefactorApi.Filters
{
  public class TokenFilter : IAsyncActionFilter
  {
    private readonly UserDbContext _context;

    public TokenFilter(UserDbContext context)
    {
      _context = context;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

      if (CheckToken(context, controllerActionDescriptor == null ? null : controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)))
      {
        var result = await next();
      }
    }

    private bool CheckToken(ActionExecutingContext context, Object[] attributes)
    {
      RestrictionAttribute attr = null;

      foreach (var a in attributes)
        if (a is RestrictionAttribute)
          attr = a as RestrictionAttribute;

      if (attr != null && attr.NeedToken)
      {
        if(attr.App == null)
        {
          context.Result = new BadRequestObjectResult("Api is Required");
          return false;
        }

        StringValues token = string.Empty;
        if (!context.HttpContext.Request.Headers.TryGetValue("apiKey", out token))
        {
          context.Result = new BadRequestObjectResult("Token is Required");
          return false;
        }
      }

      return true;
    }
  }
}
