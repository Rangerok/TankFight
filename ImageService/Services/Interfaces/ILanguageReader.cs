using ImageService.Models;

namespace ImageService.Services.Interfaces
{
  public interface ILanguageReader
  {
    Language[] ReadAll();
    Language Read(string language);
  }
}