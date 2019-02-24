using MongoDB.Bson.Serialization.Attributes;

namespace TournamentService.Models
{
  [BsonIgnoreExtraElements]
  public class User
  {
    public string Name { get; set; }
    public Bot[] Bots { get; set; } = new Bot[0];
  }
}