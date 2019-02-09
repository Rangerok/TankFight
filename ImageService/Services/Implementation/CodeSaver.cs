using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageService.Models;
using ImageService.Services.Interfaces;

namespace ImageService.Services.Implementation
{
  public class CodeSaver : ICodeSaver
  {
    public async Task<string> Save(string code, LanguageConfiguration configuration)
    {
      if (string.IsNullOrWhiteSpace(code))
      {
        throw new ArgumentException("Код пользователя пустой.", nameof(code));
      }

      if (configuration == null)
      {
        throw new ArgumentNullException(nameof(configuration));
      }

      var solutionPath = Path.Combine("bots", configuration.BuildId);
      Directory.CreateDirectory(solutionPath);

      var answerFile = Path.Combine(solutionPath, configuration.AnswerFileName);
      using (var fileStream = File.Create(answerFile))
      {
        var bytes = Encoding.UTF8.GetBytes(code);

        await fileStream.WriteAsync(bytes);
      }

      var files = Directory.GetFiles(configuration.RunnerPath);
      foreach (var file in files)
      {
        var fileName = Path.GetFileName(file);
        var copyFile = Path.Combine(solutionPath, fileName);
        File.Copy(file, copyFile);
      }

      return solutionPath;
    }
  }
}