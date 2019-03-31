using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using TournamentService.Models;
using TournamentService.Services.Interfaces;

namespace TournamentService
{
  public class AuthenticationEventsHandlers
  {
    public static Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
      context.Response.Headers.Clear();
      context.Response.ContentType = MediaTypeNames.Application.Json;
      context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
      return Task.CompletedTask;
    }

    public static Task OnRedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
      context.Response.Headers.Clear();
      context.Response.ContentType = MediaTypeNames.Application.Json;
      context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
      return Task.CompletedTask;
    }

    public static async Task OnSignedIn(CookieSignedInContext context)
    {
      var userRepository = context.HttpContext.RequestServices.GetService<IUserRepository>();

      var user = new User
      {
        Name = context.Principal.Identity.Name
      };

      if (await userRepository.UserExists(user.Name))
      {
        return;
      }

      foreach (var claim in context.Principal.Claims)
      {
        switch (claim.Type)
        {
          case ClaimTypes.NameIdentifier:
            user.NameIdentifier = claim.Value;
            break;
          case ClaimTypes.Email:
            user.Email = claim.Value;
            break;
          case GitHubAuthenticationConstants.Claims.Name:
            user.GitHubName = claim.Value;
            break;
        }
      }

      await userRepository.Add(user);
    }
  }
}