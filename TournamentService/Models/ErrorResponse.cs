namespace TournamentService.Models
{
  public class ErrorResponse
  {
    public string Error { get; set; }

    public ErrorResponse(string error)
    {
      this.Error = error;
    }
  }
}