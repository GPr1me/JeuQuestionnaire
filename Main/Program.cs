using Game.App.Services;
using Game.App.Services.Interfaces;
using Game.SignalR.Connector;
using Main.Components;
using Microsoft.OpenApi.Models;
using SignalR.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IGameService, GameService>();

builder.Services.AddHttpClient();
builder.Services.AddSingleton(sp => new HubClient(builder.Configuration.GetSection("SignalRHubConnection").Get<HubConnectionSettings>()!,
                                                     sp.GetRequiredService<ILogger<HubClient>>()));

builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//  app.UseExceptionHandler("/Error", createScopeForErrors: true);
//  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//  app.UseHsts();
//}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});


app.UseRouting();

app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.MapControllers();

app.MapHub<GameHub>(GameHub.HubUrl);

app.Run();

app.Run();
