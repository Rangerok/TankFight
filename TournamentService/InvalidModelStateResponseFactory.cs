using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TournamentService.Models;

namespace TournamentService
{
  public static class InvalidModelStateResponseFactory
  {
    public static IActionResult ReturnErrorResponse(ActionContext context)
    {
      var errorMessage = string.Join(" ", context
        .ModelState
        .Values
        .SelectMany(x => x.Errors)
        .Select(x => x.ErrorMessage));

      return new BadRequestObjectResult(new ErrorResponse(errorMessage));
    }
  }
}