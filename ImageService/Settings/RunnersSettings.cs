using System.Collections.Generic;

namespace ImageService.Settings
{
  public class RunnersSettings
  {
    public List<string> SupportedLanguages { get; set; }
    public Dictionary<string, string> AnswerFileNames { get; set; }
  }
}
