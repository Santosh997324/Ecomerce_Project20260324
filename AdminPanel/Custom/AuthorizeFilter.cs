using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

public class AuthorizeFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Session.GetString("JwtToken");

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToActionResult("Login", "Security", null);
            return;
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                context.HttpContext.Session.Clear();

                context.Result = new RedirectToActionResult(
                    "Login",
                    "Security",
                    null);

                return;
            }
        }
        catch
        {
            context.HttpContext.Session.Clear();

            context.Result = new RedirectToActionResult(
                "Login",
                "Security",
                null);

            return;
        }

        base.OnActionExecuting(context);
    }
}