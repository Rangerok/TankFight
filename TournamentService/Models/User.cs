using MongoDB.Bson.Serialization.Attributes;

namespace TournamentService.Models
{
  [BsonIgnoreExtraElements]
  public class User
  {
    public string Name { get; set; }
    public string GitHubName { get; set; }
    public string NameIdentifier { get; set; }
    public string Email { get; set; }
    public Bot[] Bots { get; set; } = new Bot[0];
  }
}