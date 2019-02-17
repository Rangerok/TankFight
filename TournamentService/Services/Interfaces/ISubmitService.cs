using System.Threading.Tasks;
using TournamentService.Models;

namespace TournamentService.Services.Interfaces
{
  public interface ISubmitService
  {
    Task Submit(string userName, UserAnswer userAnswer);
    Task<string> SubmitTest(UserAnswer userAnswer);
  }
}