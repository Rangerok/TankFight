using Microsoft.AspNetCore.Authentication.Cookies;
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
      this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var db = new MongoClient(this.Configuration["Mongo:Connection"])
        .GetDatabase(this.Configuration["Mongo:Database"]);

      var fightClient = RestService.For<IFightClient>(this.Configuration["Locations:FightServerLocation"]);
      var imageClient = RestService.For<IImageClient>(this.Configuration["Locations:ImageServiceLocation"]);

      services
        .AddHostedService<ExpiredTestBotsRemover>()
        .AddSingleton(fightClient)
        .AddSingleton(imageClient)
        .AddSingleton<ITestBotsRepository, TestBotsRepository>()
        .AddSingleton<IUserRepository, UserRepository>()
        .AddSingleton<ISubmitService, SubmitService>()
        .AddSingleton(db.GetCollection<Bot>(this.Configuration["Mongo:TestBotsCollection"]))
        .AddSingleton(db.GetCollection<User>(this.Configuration["Mongo:UsersCollection"]));


      services.AddAuthentication(options =>
        {
          options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
          options.Events.OnRedirectToLogin += AuthenticationEventsHandlers.OnRedirectToLogin;
          options.Events.OnRedirectToAccessDenied += AuthenticationEventsHandlers.OnRedirectToAccessDenied;
          options.Events.OnSignedIn += AuthenticationEventsHandlers.OnSignedIn;
        })
        .AddGitHub(options =>
        {
          options.CallbackPath = "/auth/signin-github";
          options.ClientId = this.Configuration["Github:ClientId"];
          options.ClientSecret = this.Configuration["Github:ClientSecret"];
          options.Scope.Add("user:email");
        });

      services
        .Configure<ApiBehaviorOptions>(options =>
          {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ReturnErrorResponse;
          });

      services
        .AddCors()
        .AddMvc()
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

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvc();
    }
  }
}
