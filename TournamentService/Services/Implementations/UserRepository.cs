﻿using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TournamentService.Models;
using TournamentService.Services.Interfaces;

namespace TournamentService.Services.Implementations
{
  public class UserRepository : IUserRepository
  {
    private readonly IMongoCollection<User> users;

    public Task<User> Get(string userName)
    {
      if (string.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentException($"Invalid {userName}", nameof(userName));
      }

      return this.users
        .Find(x => x.Name == userName)
        .SingleOrDefaultAsync();
    }

    public async Task Add(User user)
    {
      if (user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      if (await this.UserExists(user.Name))
      {
        return;
      }

      await this.users
        .InsertOneAsync(user);
    }

    public async Task AddBot(string userName, Bot bot)
    {
      if (string.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentException($"Invalid {userName}", nameof(userName));
      }

      if (bot == null)
      {
        throw new ArgumentNullException(nameof(bot));
      }

      var update = Builders<User>.Update.AddToSet(x => x.Bots, bot);

      await this.users
        .UpdateOneAsync(
          x => x.Name == userName, 
          update, 
          new UpdateOptions{IsUpsert = true}
        );
    }

    public async Task<bool> UserExists(string userName)
    {
      if (string.IsNullOrWhiteSpace(userName))
      {
        return false;
      }

      return await this.users
               .CountDocumentsAsync(x => x.Name == userName) != 0;
    }

    public UserRepository(IMongoCollection<User> users)
    {
      this.users = users;

      var indexModel = new CreateIndexModel<User>(
        Builders<User>
          .IndexKeys
          .Descending(x => x.Name),
        new CreateIndexOptions { Unique = true });

      this.users.Indexes.CreateOne(indexModel);
    }
  }
}