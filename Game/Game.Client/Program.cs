using Game.Client.Services;
using Game.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


var serverUrl = !builder.HostEnvironment.IsDevelopment() ? Environment.GetEnvironmentVariable("ServerUrl") : builder.HostEnvironment.BaseAddress;

builder.Services.AddSingleton<IQuestionService, QuestionService>();
builder.Services.AddSingleton<IGameClientService, GameClientService>();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serverUrl) });
//builder.Services.AddScoped(sp =>
//{
//  NavigationManager navigation = sp.GetRequiredService<NavigationManager>();
//  return new HttpClient { BaseAddress = new Uri(navigation.BaseUri) };
//});

builder.Services.AddHttpClient<IQuestionService, QuestionService>(client =>
{
  client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddHttpClient<IGameClientService, GameClientService>(client =>
{
  client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});


await builder.Build().RunAsync();
