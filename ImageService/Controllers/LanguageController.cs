using System.Net;
using System.Threading.Tasks;
using ImageService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageService.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class LanguageController : Controller
  {
    private readonly ILanguageReader languageReader;
    private readonly ILogger<LanguageController> logger;

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult GetAll()
    {
      var languages = this.languageReader.ReadAll();

      if (languages.Length == 0)
      {
        this.logger.LogWarning("Не найдено языков.");
        return this.StatusCode((int)HttpStatusCode.InternalServerError);
      }

      return Ok(languages);
    }

    public LanguageController(ILanguageReader languageReader, ILogger<LanguageController> logger)
    {
      this.logger = logger;
      this.languageReader = languageReader;
    }
  }
}