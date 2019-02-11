using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageService.Models;
using ImageService.Services.Interfaces;
using ImageService.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ImageService.Services.Implementation
{
  public class LanguageReader : ILanguageReader
  {
    private readonly RunnersSettings runnersSettings;

    public Language[] ReadAll()
    {
      var languageNames = Directory
        .GetDirectories(this.runnersSettings.RunnersFolderName)
        .Select(Path.GetFileNameWithoutExtension)
        .ToArray();

      var languages = new List<Language>();
      foreach (var languageName in languageNames)
      {
        var language = this.Read(languageName);

        if (language != null)
        {
          languages.Add(language);
        }
      }

      return languages.ToArray();
    }

    public Language Read(string languageName)
    {
      if (string.IsNullOrWhiteSpace(languageName))
      {
        throw new ArgumentException($"Недопустимый {languageName}", nameof(languageName));
      }

      try
      {
        var templateFile = Path.Combine(runnersSettings.RunnersFolderName, languageName, this.runnersSettings.TemplateFile);
        var languageFile = Path.Combine(runnersSettings.RunnersFolderName, languageName, this.runnersSettings.LanguageFile);

        var template = File.ReadAllText(templateFile);
        var language = JsonConvert.DeserializeObject<Language>(File.ReadAllText(languageFile));

        language.Template = template;
        return language;
      }
      catch
      {
        return null;
      }
    }

    public LanguageReader(IOptions<RunnersSettings> runnersSettings)
    {
      this.runnersSettings = runnersSettings.Value;
    }
  }
}