using System;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using ImageService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ImageService.Services.Implementation
{
  public class ImageRemover : IImageRemover
  {
    private readonly IDockerClient dockerClient;
    private readonly ILogger<ImageRemover> logger;

    public async Task RemoveImage(string imageTag)
    {
      if (string.IsNullOrWhiteSpace(imageTag))
      {
        throw new System.ArgumentException($"Недопустимый {nameof(imageTag)}", nameof(imageTag));
      }

      try
      {
        await this.dockerClient.Images.DeleteImageAsync(imageTag,
          new ImageDeleteParameters
          {
            Force = true,
            PruneChildren = true
          });
      }
      catch (DockerImageNotFoundException ex)
      {
        this.logger.LogWarning(ex, $"Образ с тэгом {imageTag} не найден.");
        throw;
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, $"Не удалось удалить образ {imageTag}");
        throw;
      }
    }

    public ImageRemover(IDockerClient dockerClient, 
      ILogger<ImageRemover> logger)
    {
      this.dockerClient = dockerClient;
      this.logger = logger;
    }
  }
}