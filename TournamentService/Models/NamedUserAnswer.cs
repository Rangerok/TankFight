using System.ComponentModel.DataAnnotations;

namespace TournamentService.Models
{
  public class NamedUserAnswer : UserAnswer
  {
    [Required(ErrorMessage = "Нет имени.")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Имя бота должно содержать только буквы и цифры.")]
    public string Name { get; set; }
  }
}