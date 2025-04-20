using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
if (builder.HostEnvironment.IsDevelopment())
{
  builder.Services.AddScoped(sp => new HttpClient
  {
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
  });
}

if (builder.HostEnvironment.IsProduction())
{
  builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Environment.GetEnvironmentVariable("ServerUrl")!) });
}

await builder.Build().RunAsync();
