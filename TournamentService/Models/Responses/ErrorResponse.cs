namespace TournamentService.Models.Responses
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