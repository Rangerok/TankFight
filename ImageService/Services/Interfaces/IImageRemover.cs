using System.Threading.Tasks;

namespace ImageService.Services.Interfaces
{
  public interface IImageRemover
  {
    Task RemoveImage(string imageTag);
  }
}