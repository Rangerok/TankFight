using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TournamentService.Exceptions;
using TournamentService.Models;
using TournamentService.Models.Responses;
using TournamentService.Services.Interfaces;
using TournamentService.Settings;

namespace TournamentService.Controllers
{
  [ApiController]
  [Authorize]
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class SubmitController : Controller
  {
    private readonly ILogger<SubmitController> logger;
    private readonly ISubmitService submitService;
    private readonly IUserRepository userRepository;

    private readonly SubmitSettings submitSettings;

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetSubmitted()
    {
      try
      {
        var user = await this.userRepository.Get(this.User.Identity.Name);

        return this.Ok(new BotsResponse
        {
          Bots = user.Bots,
          MaxBotsCount = this.submitSettings.MaxBotsCount
        });
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, "Не получилось запросить всех ботов пользователя.");
        return this.StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Произошла ошибка, попробуйте еще раз."));
      }
    }

    [HttpPost]
    [RequestSizeLimit(1024 * 100)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Submit([FromBody] NamedUserAnswer userAnswer)
    {
      try
      {
        await this.submitService.Submit(this.User.Identity.Name, userAnswer);

        return this.Ok();
      }
      catch (ImageNotCreatedException ex)
      {
        this.logger.LogWarning(ex, "Не удалось создать образ.");
        return this.BadRequest(new ErrorResponse("Не удалось создать образ, попробуйте еще раз."));
      }
      catch (BotsCountExceededException ex)
      {
        this.logger.LogWarning(ex, "Превышен лимит на количество ботов.");
        return this.BadRequest(new ErrorResponse("Превышен лимит на количество ботов."));
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, "Не получилось отправить решение.");
        return this.StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Произошла ошибка, попробуйте еще раз."));
      }
    }

    [HttpPost]
    [RequestSizeLimit(1024 * 100)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> SubmitTest([FromBody] UserAnswer userAnswer)
    {
      try
      {
        var battleId = await this.submitService.SubmitTest(userAnswer);

        return this.Ok(battleId);
      }
      catch (ImageNotCreatedException ex)
      {
        this.logger.LogWarning(ex, "Не удалось создать образ для тестового боя.");
        return this.BadRequest(new ErrorResponse("Не удалось создать образ, попробуйте еще раз."));
      }
      catch (BattleNotStartedException ex)
      {
        this.logger.LogWarning(ex, "Не удалось запустить бой для тестового боя.");
        return this.BadRequest(new ErrorResponse("Не удалось запустить бой, попробуйте еще раз."));
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, "Не получилось запустить тестовый бой");
        return this.StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Произошла ошибка, попробуйте еще раз."));
      }
    }

    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> RemoveBot(string botName)
    {
      try
      {
        await this.userRepository.RemoveBot(this.User.Identity.Name, botName);

        return this.Ok();
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, "Не получилось удалить бота {bot}.", botName);
        return this.StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Не получилось удалить бота, попробуйте еще раз."));
      }
    }

    public SubmitController(ISubmitService submitService, 
      ILogger<SubmitController> logger,
      IUserRepository userRepository, 
      IOptions<SubmitSettings> submitSettings)
    {
      this.submitService = submitService;
      this.logger = logger;
      this.userRepository = userRepository;
      this.submitSettings = submitSettings.Value;
    }
  }
}