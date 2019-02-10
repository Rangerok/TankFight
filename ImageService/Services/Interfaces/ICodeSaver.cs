using System.Threading.Tasks;
using ImageService.Models;

namespace ImageService.Services.Interfaces
{
  public interface ICodeSaver
  {
    Task<string> Save(string code, string language, string buildId);
  }
}