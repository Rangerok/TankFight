using System;
using System.Threading.Tasks;
using TournamentService.Exceptions;
using TournamentService.HttpClients;
using TournamentService.Models;
using TournamentService.Services.Interfaces;

namespace TournamentService.Services.Implementations
{
  public class SubmitService : ISubmitService
  {
    private const string TestImage = "vblz/tanks:randombot";

    private readonly IFightClient fightClient;
    private readonly IImageClient imageClient;
    private readonly ITestBotsRepository testBotsRepository;

    public Task Submit(string userName, UserAnswer userAnswer)
    {
      throw new System.NotImplementedException();
    }

    public async Task<string> SubmitTest(UserAnswer userAnswer)
    {
      if (userAnswer == null)
      {
        throw new ArgumentNullException(nameof(userAnswer));
      }

      ImageInfo imageInfo;
      try
      {
        imageInfo = await this.imageClient.Create(userAnswer);

        var bot = new Bot
        {
          Tag = imageInfo.Tag,
          Code = userAnswer.Code,
          Language = userAnswer.Language,
          AddedAt = DateTime.UtcNow
        };

        await this.testBotsRepository.Add(bot);
      }
      catch (Exception ex)
      {
        throw new ImageNotCreatedException("Не удалось создать образ", ex);
      }

      try
      {
        var battleInfo = await this.fightClient.StartNew(new []{ imageInfo.Tag, TestImage });
        return battleInfo.BattleId;
      }
      catch (Exception ex)
      {
        throw new BattleNotStartedException("Не удалось запустить бой", ex);
      }
    }

    public SubmitService(IFightClient fightClient,
      IImageClient imageClient, 
      ITestBotsRepository testBotsRepository)
    {
      this.fightClient = fightClient;
      this.imageClient = imageClient;
      this.testBotsRepository = testBotsRepository;
    }
  }
}