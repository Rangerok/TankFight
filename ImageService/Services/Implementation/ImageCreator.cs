using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using ImageService.Exceptions;
using ImageService.Models;
using ImageService.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ImageService.Services.Implementation
{
  public class ImageCreator : IImageCreator
  {
    private const string ImageTagFormat = "tankfight:{0}-{1}";

    private readonly ILogger<ImageCreator> logger;
    private readonly IDockerClient dockerClient;
    private readonly ICodeArchiver codeArchiver;
    private readonly ICodeSaver codeSaver;

    private readonly TimeSpan maxBuildTime = TimeSpan.FromSeconds(60);

    public async Task<ImageInfo> CreateImage(Language language, string code)
    {
      if (string.IsNullOrWhiteSpace(code))
      {
        throw new ArgumentException("Код пользователя пустой.", nameof(code));
      }

      var buildId = Guid.NewGuid().ToString();
      var solutionPath = await this.codeSaver.Save(code, language, buildId);
      var archive = this.codeArchiver.CreateArchive(solutionPath, buildId);

      var imageTag = string.Format(ImageTagFormat, buildId, language.Id.ToLower());

      try
      {
        using (var archiveStream = File.OpenRead(archive))
        {
          var cts = new CancellationTokenSource(this.maxBuildTime);

          var buildParams = new ImageBuildParameters
          {
            Tags = new List<string> {imageTag},
            Remove = true,
            ForceRemove = true
          };

          var stream = await this.dockerClient.Images.BuildImageFromDockerfileAsync(archiveStream, buildParams, cts.Token);

          await Task.WhenAny(this.ReadToEnd(stream, imageTag, cts.Token), Task.Delay(this.maxBuildTime));

          if (cts.IsCancellationRequested)
          {
            throw new BuildImageException($"Не удалось создать образ с именем {imageTag} за выделенное время");
          }

          return new ImageInfo { Tag = imageTag };
        }
      }
      finally
      {
        File.Delete(archive);
        Directory.Delete(solutionPath, true);
      }
    }

    private async Task ReadToEnd(Stream stream, string imageTag, CancellationToken cts)
    {
      var reader = new StreamReader(stream);

      while (!reader.EndOfStream && !cts.IsCancellationRequested)
      {
        var jsonMessage = JsonConvert.DeserializeObject<JSONMessage>(await reader.ReadLineAsync());

        this.logger.LogDebug("Событие сборки {json} образа {imageTag}", jsonMessage.Stream, imageTag);

        if (!string.IsNullOrWhiteSpace(jsonMessage.ErrorMessage))
        {
          this.logger.LogError("Ошибка сборки {error} образа {imageTag}", jsonMessage.ErrorMessage, imageTag);
          throw new BuildImageException($"Не удалось создать образ {imageTag}, ошибка {jsonMessage.ErrorMessage}");
        }
      }
    }

    public ImageCreator(IDockerClient dockerClient, 
      ICodeArchiver codeArchiver, 
      ICodeSaver codeSaver, 
      ILogger<ImageCreator> logger)
    {
      this.dockerClient = dockerClient;
      this.codeArchiver = codeArchiver;
      this.codeSaver = codeSaver;
      this.logger = logger;
    }
  }
}