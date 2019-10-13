using System.ComponentModel.DataAnnotations;

namespace TournamentService.Models
{
  public class NamedUserAnswer : UserAnswer
  {
    [Required(ErrorMessage = "Нет имени.")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Имя бота должно содержать только буквы и цифры.")]
    [StringLength(15, ErrorMessage = "Имя бота должно быть от {2} до {1} символов.", MinimumLength = 5)]
    public string Name { get; set; }
  }
}