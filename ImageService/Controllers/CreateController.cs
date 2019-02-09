﻿using System;
using System.Net;
using System.Threading.Tasks;
using ImageService.Models;
using ImageService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageService.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class CreateController : Controller
  {
    private readonly IImageCreator imageCreator;
    private readonly ILogger<CreateController> logger;

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody]CreateArgs args)
    {
      if (args == null)
      {
        return this.BadRequest();
      }

      try
      {
        var imageTag = await imageCreator.CreateImage(args.Language, args.Code);

        return this.Ok(new { Tag = imageTag });
      }
      catch (Exception ex)
      {
        this.logger.LogWarning(ex, "Не удалось создать образ из кода.");
        return this.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public CreateController(IImageCreator imageCreator, ILogger<CreateController> logger)
    {
      this.imageCreator = imageCreator;
      this.logger = logger;
    }
  }
}