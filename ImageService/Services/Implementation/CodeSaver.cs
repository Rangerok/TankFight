using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageService.Exceptions;
using ImageService.Models;
using ImageService.Services.Interfaces;
using ImageService.Settings;
using Microsoft.Extensions.Options;

namespace ImageService.Services.Implementation
{
  public class CodeSaver : ICodeSaver
  {
    private readonly RunnersSettings runnersSettings;

    public async Task<string> Save(string code, Language language, string buildId)
    {
      if (string.IsNullOrWhiteSpace(code))
      {
        throw new ArgumentException("Код пользователя пустой.", nameof(code));
      }

      if (string.IsNullOrWhiteSpace(buildId))
      {
        throw new ArgumentException("Пустой buildId", nameof(buildId));
      }

      var solutionPath = Path.Combine(runnersSettings.SolutionsFolderName, buildId);
      Directory.CreateDirectory(solutionPath);

      try
      {
        await this.CreateAnswerFile(solutionPath, language.AnswerFile, code);
        this.CopyRunnerFiles(solutionPath, language.Id);
      }
      catch (Exception ex)
      {
        throw new CodeNotSavedException($"Не удалось сохранить код для языка {language}", ex);
      }

      return solutionPath;
    }

    private async Task CreateAnswerFile(string solutionPath, string answerFileName, string code)
    {
      var answerFile = Path.Combine(solutionPath, answerFileName);
      using (var fileStream = File.Create(answerFile))
      {
        var bytes = Encoding.UTF8.GetBytes(code);

        await fileStream.WriteAsync(bytes);
      }
    }

    private void CopyRunnerFiles(string solutionPath, string language)
    {
      var runnerPath = Path.Combine(runnersSettings.RunnersFolderName, language, runnersSettings.BaseFilesFolderName);
      var files = Directory.GetFiles(runnerPath);
      foreach (var file in files)
      {
        var fileName = Path.GetFileName(file);
        var copyFile = Path.Combine(solutionPath, fileName);
        File.Copy(file, copyFile);
      }
    }

    public CodeSaver(IOptions<RunnersSettings> settings)
    {
      this.runnersSettings = settings.Value;
    }
  }
}