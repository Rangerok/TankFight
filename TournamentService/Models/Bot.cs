using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TournamentService.Models
{
  [BsonIgnoreExtraElements]
  public class Bot
  {
    public string Name { get; set; }
    public string Tag { get; set; }
    public string Language { get; set; }
    public string Code { get; set; }
    public DateTime AddedAt { get; set; }
  }
}