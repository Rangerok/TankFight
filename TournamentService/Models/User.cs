namespace TournamentService.Models
{
  public class User
  {
    public string Name { get; set; }
    public Bot[] Bots { get; set; } = new Bot[0];
  }
}