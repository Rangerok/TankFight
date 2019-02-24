using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TournamentService.HttpClients;
using TournamentService.Services.Interfaces;

namespace TournamentService.Services.Implementations
{
  public class ExpiredTestBotsRemover : BackgroundService
  {
    private readonly ILogger<ExpiredTestBotsRemover> logger;
    private readonly IImageClient imageClient;
    private readonly ITestBotsRepository testBotsRepository;

    private readonly TimeSpan clearDelay = TimeSpan.FromSeconds(60);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      this.logger.LogDebug("Начинаю очистку ботов.");

      while (!stoppingToken.IsCancellationRequested)
      {
        try
        {
          var expiredBots = await this.testBotsRepository.GetAllExpired();

          foreach (var expiredBot in expiredBots)
          {
            await this.imageClient.Delete(expiredBot.Tag);
            await this.testBotsRepository.Remove(expiredBot);
            this.logger.LogDebug($"Бот {expiredBot.Tag} удален.");
          }
        }
        catch (Exception ex)
        {
          this.logger.LogError(ex, "Ошибка при очистке ботов.");
        }

        await Task.Delay(this.clearDelay, stoppingToken);
      }

      this.logger.LogDebug("Заканчиваю очистку ботов.");
    }

    public ExpiredTestBotsRemover(ILogger<ExpiredTestBotsRemover> logger, 
      ITestBotsRepository testBotsRepository, 
      IImageClient imageClient)
    {
      this.logger = logger;
      this.testBotsRepository = testBotsRepository;
      this.imageClient = imageClient;
    }
  }
}