using System.Threading.Tasks;
using ImageService.Models;

namespace ImageService.Services.Interfaces
{
  public interface IImageCreator
  {
    Task<ImageInfo> CreateImage(Language language, string code);
  }
}