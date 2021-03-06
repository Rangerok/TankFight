﻿using Docker.DotNet;
using ImageService.Services.Implementation;
using ImageService.Services.Interfaces;
using ImageService.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Runtime.InteropServices;

namespace ImageService
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      var dockerUrl = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? new Uri("npipe://./pipe/docker_engine")
        : new Uri("unix:/var/run/docker.sock");

      var dockerClient = new DockerClientConfiguration(dockerUrl).CreateClient();

      services
        .Configure<RunnersSettings>(this.Configuration.GetSection("Runners"));

      services
        .AddHostedService<DanglingImagesRemover>()
        .AddSingleton<IDockerClient>(dockerClient)
        .AddSingleton<ILanguageReader, LanguageReader>()
        .AddSingleton<IImageCreator, ImageCreator>()
        .AddSingleton<IImageRemover, ImageRemover>()
        .AddSingleton<ICodeSaver, CodeSaver>()
        .AddSingleton<ICodeArchiver, CodeArchiver>();

      services
        .AddCors();

      services
        .AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Image Service", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Image Service V1");
        c.RoutePrefix = string.Empty;
      });

      app.UseMvc();
    }
  }
}
