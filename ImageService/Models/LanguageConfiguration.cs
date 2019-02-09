using System;

namespace ImageService.Models
{
  public class LanguageConfiguration
  {
    private static readonly string[] AnswerFileNames = new[]
    {
      "main.go", //Go
      "Program.cs",  //Csharp
      "bot.py", //Python
      "bot.js", //JavaScript
    };

    private static readonly string[] Runners = new[]
    {
      "runners/go", //Go
      "runners/csharp", //Csharp
      "runners/python", //Python
      "runners/javascript" //JavaScript
    };

    public string BuildId { get; }
    public string AnswerFileName { get; }
    public string RunnerPath { get; }

    public static LanguageConfiguration Build(SupportedLanguages language)
    {
      var buildId = Guid.NewGuid().ToString();

      return new LanguageConfiguration(AnswerFileNames[(int)language], Runners[(int) language], buildId);
    }

    private LanguageConfiguration(string answerFileName, string runnerPath, string buildId)
    {
      AnswerFileName = answerFileName;
      RunnerPath = runnerPath;
      BuildId = buildId;
    }
  }
}