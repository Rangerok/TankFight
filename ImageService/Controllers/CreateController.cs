using System;
using System.Net;
using System.Threading.Tasks;
using ImageService.Models;
using ImageService.Settings;
using ImageService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ImageService.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class CreateController : Controller
  {
    private readonly IImageCreator imageCreator;
    private readonly ILanguageReader languageReader;
    private readonly ILogger<CreateController> logger;

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody]CreateImageArgs imageArgs)
    {
      if (imageArgs == null)
      {
        return this.BadRequest();
      }
     
      try
      {
        var language = this.languageReader.Read(imageArgs.Language);

        if (language == null)
        {
          this.logger.LogWarning("Запрос с неизвестным языком.");
          return BadRequest();
        }

        var imageTag = await imageCreator.CreateImage(language, imageArgs.Code);

        return this.Ok(new { Tag = imageTag });
      }
      catch (Exception ex)
      {
        this.logger.LogWarning(ex, "Не удалось создать образ из кода.");
        return this.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public CreateController(IImageCreator imageCreator, 
      ILanguageReader languageReader, 
      ILogger<CreateController> logger)
    {
      this.imageCreator = imageCreator;
      this.logger = logger;
      this.languageReader = languageReader;
    }
  }
}