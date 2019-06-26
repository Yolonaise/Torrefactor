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
using TorrefactorApi.Helper.Security;
using TorrefactorApi.Service;

namespace TorrefactorApi.Filters
{
  public class TokenFilter : IAsyncActionFilter
  {
    private readonly UserDbContext _context;
    private readonly ITorrefactorContext _tContext;

    public TokenFilter(UserDbContext context, ITorrefactorContext tContext)
    {
      _context = context;
      _tContext = tContext;
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
        //if (attr.App == null)
        //  return BadRequest(context, "Api is Required");

        StringValues token = string.Empty;
        if (!context.HttpContext.Request.Headers.TryGetValue("token", out token))
          return BadRequest(context, "The token is missing");

        var convertedToken = TokenGenerator.ReadToken(token);

        if (convertedToken == null || !convertedToken.HasValues)
          return BadRequest(context, "Token is empty");

        if (convertedToken.ContainsKey(TokenGenerator.USERNAME))
          return BadRequest(context, "Token is not valid");

        if (convertedToken.ContainsKey(TokenGenerator.PASSWORD))
          return BadRequest(context, "Token is not valid");

        if (convertedToken.ContainsKey(TokenGenerator.EXPIRY_DATE))
          return BadRequest(context, "Token is not valid");

        var u = _context.Users.FirstOrDefault(x => x.Username == (string)convertedToken[TokenGenerator.USERNAME]);
        if (u == null)
          return BadRequest(context, "TokenFilter - User not found");

        if (u.Password != (string)convertedToken[TokenGenerator.PASSWORD])
          return BadRequest(context, "TokenFilter - authentication invalid");

        try
        {
          DateTime expiryDate = Convert.ToDateTime((string)convertedToken[TokenGenerator.EXPIRY_DATE]);
          if (expiryDate < DateTime.Now)
            return BadRequest(context, "TokenFilter - token is expired");

          _tContext.CurrentUser = u;
        }
        catch (Exception e)
        {
          return BadRequest(context, "TokenFilter - " + e.Message);
        }
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
