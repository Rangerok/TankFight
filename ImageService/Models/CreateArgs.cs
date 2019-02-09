using Newtonsoft.Json;

namespace ImageService.Models
{
  public class CreateArgs
  {
    [JsonProperty("language")]
    public SupportedLanguages Language { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }
  }
}