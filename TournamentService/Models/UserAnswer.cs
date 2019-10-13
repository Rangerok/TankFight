using System.ComponentModel.DataAnnotations;

namespace TournamentService.Models
{
  public class UserAnswer
  {
    public string Language { get; set; }

    [StringLength(12000, ErrorMessage = "Слишком длинный код.")]
    public string Code { get; set; }
  }
}