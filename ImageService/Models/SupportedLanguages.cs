using System.Runtime.Serialization;

namespace ImageService.Models
{
  public enum SupportedLanguages
  {
    [EnumMember(Value = "go")]
    Go,

    [EnumMember(Value = "csharp")]
    Csharp,

    [EnumMember(Value = "python")]
    Python,

    [EnumMember(Value = "javascript")]
    JavaScript
  }
}