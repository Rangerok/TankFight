using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentService.Extensions;

namespace TournamentService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : Controller
  {
    private readonly ILogger<AuthController> logger;

    //Запросить все провайдеры
    //[HttpGet("/signin")]
    //public async Task<IActionResult> SignIn() => this.Ok(await HttpContext.GetExternalProvidersAsync());

    [HttpGet]
    [Authorize]
    public IActionResult Auth() => this.Ok("Ok!");

    [HttpGet("signin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SignIn(string provider)
    {
      if (string.IsNullOrWhiteSpace(provider))
      {
        return this.BadRequest();
      }

      if (!await this.HttpContext.IsProviderSupportedAsync(provider))
      {
        this.logger.LogWarning($"Запрос на аутентификацию с неподдерживаемым провайдером: {provider}");
        return this.BadRequest();
      }

      return this.Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
    }

    [HttpGet("signout")]
    public IActionResult SignOut()
    {
      return this.SignOut(new AuthenticationProperties { RedirectUri = "/" },
        CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public AuthController(ILogger<AuthController> logger)
    {
      this.logger = logger;
    }
  }
}