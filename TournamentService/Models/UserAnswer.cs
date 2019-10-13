using System.ComponentModel.DataAnnotations;

namespace TournamentService.Models
{
  public class UserAnswer
  {
    [Required(ErrorMessage = "Не выбран язык.")]
    public string Language { get; set; }

    [Required(ErrorMessage = "Не написан код.")]
    [StringLength(12000, ErrorMessage = "Слишком длинный код.")]
    public string Code { get; set; }
  }
}