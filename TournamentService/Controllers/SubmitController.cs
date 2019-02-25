using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentService.Exceptions;
using TournamentService.Models;
using TournamentService.Services.Interfaces;

namespace TournamentService.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class SubmitController : Controller
  {
    private readonly ILogger<SubmitController> logger;
    private readonly ISubmitService submitService;

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> SubmitTest([FromBody] UserAnswer userAnswer)
    {
      if (userAnswer == null)
      {
        return this.BadRequest();
      }

      try
      {
        var battleId = await this.submitService.SubmitTest(userAnswer);

        return this.Ok(battleId);
      }
      catch (ImageNotCreatedException ex)
      {
        this.logger.LogWarning(ex, "Не удалось создать образ для тестового боя.");
        return this.BadRequest(new {Error = "Не удалось создать образ, попробуйте еще раз."});
      }
      catch (BattleNotStartedException ex)
      {
        this.logger.LogWarning(ex, "Не удалось запустить бой для тестового боя.");
        return this.BadRequest(new {Error = "Не удалось запустить бой, попробуйте еще раз."});
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, "Не получилось запустить тестовый бой");
        return this.StatusCode((int)HttpStatusCode.InternalServerError, new {Error = "Произошла ошибка, попробуйте еще раз."});
      }
    }

    public SubmitController(ISubmitService submitService, 
      ILogger<SubmitController> logger)
    {
      this.submitService = submitService;
      this.logger = logger;
    }
  }
}