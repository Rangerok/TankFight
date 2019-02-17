using System.Threading.Tasks;
using Refit;
using TournamentService.Models;

namespace TournamentService.HttpClients
{
  internal interface IImageClient
  {
    [Post("/api/image")]
    Task<ImageInfo> Create([Body] CreateImageArgs args);
  }
}