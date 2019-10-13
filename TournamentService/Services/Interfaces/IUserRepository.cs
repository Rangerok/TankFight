using System.Threading.Tasks;
using TournamentService.Models;

namespace TournamentService.Services.Interfaces
{
  public interface IUserRepository
  {
    Task<User> Get(string userName);
    Task Add(User user);
    Task AddBot(string userName, Bot bot);
    Task<int> GetBotsCount(string userName);
    Task<bool> UserExists(string userName);
  }
}