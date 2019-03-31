using System;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ImageService.Services.Implementation
{
  public class DanglingImagesRemover : IHostedService, IDisposable
  {
    private readonly TimeSpan removingPeriodInMinutes = TimeSpan.FromMinutes(5);
    private readonly ILogger logger;
    private readonly IDockerClient dockerClient;
    private Timer timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
      this.timer = new Timer(this.RemoveDangling, 
        null, 
        TimeSpan.Zero,
        this.removingPeriodInMinutes
        );

      return Task.CompletedTask;
    }

    private async void RemoveDangling(object state)
    {
      try
      {
        await this.dockerClient.Images.PruneImagesAsync();
        //Все, что ниже, не ожидается таймером.
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, "Ошибка при очистке промежуточных образов.");
      }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      this.timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      this.timer?.Dispose();
    }

    public DanglingImagesRemover(ILogger<DanglingImagesRemover> logger, 
      IDockerClient dockerClient)
    {
      this.logger = logger;
      this.dockerClient = dockerClient;
    }
  }
}