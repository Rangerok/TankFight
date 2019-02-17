using System.Threading.Tasks;
using TournamentService.Models;

namespace TournamentService.Services.Interfaces
{
  public interface ITestBotsRepository
  {
    Task<Bot[]> GetAllExpired();
    Task Add(Bot bot);
    Task Remove(Bot bot);
  }
}