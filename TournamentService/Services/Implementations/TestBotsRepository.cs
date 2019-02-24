using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TournamentService.Exceptions;
using TournamentService.Models;
using TournamentService.Services.Interfaces;

namespace TournamentService.Services.Implementations
{
  public class TestBotsRepository : ITestBotsRepository
  {
    private readonly TimeSpan botsLifeTimeInSeconds = TimeSpan.FromSeconds(120);
    private readonly IMongoCollection<Bot> bots;

    public async Task<Bot[]> GetAllExpired()
    {
      var filter = Builders<Bot>
        .Filter
        .Lte(x => x.AddedAt, DateTime.UtcNow - this.botsLifeTimeInSeconds);

      return (await this.bots
        .Find(filter)
        .ToListAsync())
        .ToArray();
    }

    public Task Add(Bot bot)
    {
      if (bot == null)
      {
        throw new ArgumentNullException(nameof(bot));
      }

      try
      {
        return this.bots
          .InsertOneAsync(bot);
      }
      catch (MongoDuplicateKeyException ex)
      {
        throw new BotAlreadyExistsException($"Бот с тэгом {bot.Tag} уже существует.", ex);
      }
    }

    public Task Remove(Bot bot)
    {
      if (bot == null)
      {
        throw new ArgumentNullException(nameof(bot));
      }

      var filter = Builders<Bot>.Filter.Eq(x => x.Tag, bot.Tag);

      return this.bots
        .DeleteOneAsync(filter);
    }

    public TestBotsRepository(IMongoCollection<Bot> bots)
    {
      this.bots = bots;

      var indexModel = new CreateIndexModel<Bot>(
        Builders<Bot>
          .IndexKeys
          .Descending(x => x.Tag),
        new CreateIndexOptions {Unique = true});

      this.bots.Indexes.CreateOne(indexModel);
    }
  }
}