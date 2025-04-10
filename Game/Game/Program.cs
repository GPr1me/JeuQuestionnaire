using Game.App.Services;
using Game.App.Services.Interfaces;
using Game.Components;
using Game.Services;
using Game.SignalR.Connector;
using Game.SignalR.Connector.Services;
using Game.SignalR.Connector.Services.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IGameHubService, GameHubService>();
builder.Services.AddSingleton<IGameLinkService, GameLinkService>();

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

app.MapHub<GameHub>(GameHub.HubUrl);

app.Run();
