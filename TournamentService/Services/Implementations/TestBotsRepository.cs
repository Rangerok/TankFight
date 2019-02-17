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
    private readonly IMongoCollection<Bot> bots;

    public Task<Bot[]> GetAllExpired()
    {
      throw new System.NotImplementedException();
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
      throw new System.NotImplementedException();
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