using System.Threading.Tasks;
using Refit;
using TournamentService.Models;

namespace TournamentService.HttpClients
{
  public interface IImageClient
  {
    [Post("/api/image")]
    Task<ImageInfo> Create([Body] UserAnswer userAnswer);

    [Delete("/api/image")]
    Task Delete(string imageTag);
  }
}