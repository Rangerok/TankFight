using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TournamentService.Exceptions;
using TournamentService.HttpClients;
using TournamentService.Models;
using TournamentService.Services.Interfaces;
using TournamentService.Settings;

namespace TournamentService.Services.Implementations
{
  public class SubmitService : ISubmitService
  {
    private readonly IFightClient fightClient;
    private readonly IImageClient imageClient;
    private readonly IUserRepository userRepository;
    private readonly ITestBotsRepository testBotsRepository;

    private readonly SubmitSettings submitSettings;

    public async Task Submit(string userName, NamedUserAnswer userAnswer)
    {
      if (string.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentNullException(nameof(userName));
      }

      if (userAnswer == null)
      {
        throw new ArgumentNullException(nameof(userAnswer));
      }

      await this.ValidateBotsCount(userName);

      ImageInfo imageInfo;
      try
      {
        imageInfo = await this.imageClient.Create(userAnswer);
      }
      catch (Exception ex)
      {
        throw new ImageNotCreatedException("Не удалось создать образ", ex);
      }

      var bot = new Bot
      {
        Name = userAnswer.Name,
        Tag = imageInfo.Tag,
        Code = userAnswer.Code,
        Language = userAnswer.Language,
        AddedAt = DateTime.UtcNow
      };

      await this.ValidateBotsCount(userName);
      await this.userRepository.AddBot(userName, bot);
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
        var battleInfo = await this.fightClient.StartNew(new []
        {
          imageInfo.Tag,
          this.submitSettings.TestImage
        });

        return battleInfo.BattleId;
      }
      catch (Exception ex)
      {
        throw new BattleNotStartedException("Не удалось запустить бой", ex);
      }
    }

    private async Task ValidateBotsCount(string userName)
    {
      var botsCount = await this.userRepository.GetBotsCount(userName);
      if (botsCount > this.submitSettings.MaxBotsCount)
      {
        throw new BotsCountExceededException($"Превышен лимит ботов {this.submitSettings.MaxBotsCount}: {userName} - {botsCount}.");
      }
    }

    public SubmitService(IFightClient fightClient,
      IImageClient imageClient,
      ITestBotsRepository testBotsRepository,
      IOptions<SubmitSettings> submitSettings,
      IUserRepository userRepository)
    {
      this.fightClient = fightClient;
      this.imageClient = imageClient;
      this.testBotsRepository = testBotsRepository;
      this.userRepository = userRepository;
      this.submitSettings = submitSettings.Value;
    }
  }
}