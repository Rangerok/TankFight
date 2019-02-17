using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Refit;
using Swashbuckle.AspNetCore.Swagger;
using TournamentService.HttpClients;
using TournamentService.Models;
using TournamentService.Services.Implementations;
using TournamentService.Services.Interfaces;

namespace TournamentService
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddCors();

      var db = new MongoClient(this.Configuration["Mongo:Connection"])
        .GetDatabase(this.Configuration["Mongo:Database"]);

      services
        .AddSingleton(db.GetCollection<Bot>(this.Configuration["Mongo:TestBotsCollection"]))
        .AddSingleton(db.GetCollection<User>(this.Configuration["Mongo:UsersCollection"]));

      var fightClient = RestService.For<IFightClient>(this.Configuration["Locations:FightServerLocation"]);
      var imageClient = RestService.For<IImageClient>(this.Configuration["Locations:ImageServiceLocation"]);

      services
        .AddSingleton(fightClient)
        .AddSingleton(imageClient)
        .AddSingleton<ITestBotsRepository, TestBotsRepository>()
        .AddSingleton<ISubmitService, SubmitService>();

      services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Tournament Service", Version = "v1" });
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tournament Service API V1");
        c.RoutePrefix = string.Empty;
      });

      app.UseMvc();
    }
  }
}
