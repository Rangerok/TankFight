﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using ImageService.Models;
using ImageService.Services.Interfaces;

namespace ImageService.Services.Implementation
{
  public class ImageCreator : IImageCreator
  {
    private const string ImageTagFormat = "tankfight:{0}-{1}";

    private readonly IDockerClient dockerClient;
    private readonly ICodeArchiver codeArchiver;
    private readonly ICodeSaver codeSaver;

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
          var stream = await this.dockerClient.Images.BuildImageFromDockerfileAsync(archiveStream, new ImageBuildParameters { Tags = new List<string> { imageTag } });
          await this.ReadToEnd(stream);
        }
      }
      finally
      {
        File.Delete(archive);
        Directory.Delete(solutionPath, true);
      }

      var result = await this.dockerClient.Images.SearchImagesAsync(new ImagesSearchParameters { Term = imageTag });

      if (result.Count == 0)
      {
        throw new Exception($"Не удалось создать образ с именем {imageTag}");
      }

      return new ImageInfo { Tag = imageTag };
    }

    private async Task ReadToEnd(Stream stream)
    {
      var buffer = new byte[128];
      var len = await stream.ReadAsync(buffer, 0, 128);
      while (len > 0)
      {
        len = await stream.ReadAsync(buffer, 0, 128);
      }
    }

    public ImageCreator(IDockerClient dockerClient, 
      ICodeArchiver codeArchiver, 
      ICodeSaver codeSaver)
    {
      this.dockerClient = dockerClient;
      this.codeArchiver = codeArchiver;
      this.codeSaver = codeSaver;
    }
  }
}