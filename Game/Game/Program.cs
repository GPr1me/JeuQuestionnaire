using Game.App.Repos;
using Game.App.Services;
using Game.App.Services.Interfaces;
using Game.App.Validators;
using Game.Client.Services.Interfaces;
using Game.Components;
using Game.Dal;
using Game.Dal.Models;
using Game.Dal.Repos;
using Game.Services;
using Game.SignalR.Connector;
using Game.SignalR.Connector.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------------------------------------------------------
// Add services to the container.

builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

// DB
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

// Services
builder.Services.AddSingleton<IGameHubService, GameHubService>();
builder.Services.AddSingleton<IGameLinkService, GameLinkService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddScoped<IGameClientService, GameClientService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IHubClient, DummyHubClient>();

// Executors
builder.Services.AddScoped<IQuestionExecutor, QuestionExecutor>();

// Validators
builder.Services.AddSingleton<IQuestionValidator, QuestionValidator>();

// ------------------------------------------------------------------------------------------------------------
// Database

builder.Services.AddDbContext<GameContext>(options =>
{
  var gameContextOptions = builder.Configuration.GetRequiredSection("GameContextOptions").Get<GameContextOptions>()!;

  options.UseNpgsql(gameContextOptions.ConnectionString, builder => builder.MigrationsAssembly(gameContextOptions.AssemblyName));
  options.EnableSensitiveDataLogging();
  options.EnableDetailedErrors();
});

builder.Services.AddSignalR();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder =>
  {
    builder.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
  });
});

builder.Services.AddResponseCompression(opts =>
{
  opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      ["application/octet-stream"]);
});

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// run migration
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<GameContext>();
  db.Database.Migrate();
}

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Game.Client._Imports).Assembly);


app.UseWebSockets();

app.MapHub<GameHub>($"/{GameHub.HubUrl}");

app.MapControllers();

app.Run();
